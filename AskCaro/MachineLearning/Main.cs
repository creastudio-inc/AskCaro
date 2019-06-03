using System;
using System.Threading.Tasks;
using System.IO;
// Requires following NuGet packages
// NuGet package -> Microsoft.Extensions.Configuration
// NuGet package -> Microsoft.Extensions.Configuration.Json
using Microsoft.ML;
using AskCaro.MachineLearning.DataStructures;
using Microsoft.ML.Data;
using static Microsoft.ML.TrainCatalogBase;
using System.Data;
using AskCaro_console.CsvFile;
using static Microsoft.ML.DataOperationsCatalog;

namespace AskCaro.MachineLearning
{
    public static class Program
    {
        //public static string AppPath = @"C:\Users\ahmed\source\repos\AskCaro\AskCaro\MachineLearning\";
        public static string AppPath = @"D:\home\site\wwwroot\MachineLearning\";

        private static string BaseDatasetsRelativePath = $@"{AppPath}Data";
        // private static string DataSetRelativePath = $"{BaseDatasetsRelativePath}/corefx-issues-train.tsv";
        private static string DataSetRelativePath = $"{BaseDatasetsRelativePath}/Questiontrain.csv";
        private static string DataSetLocation = GetAbsolutePath(DataSetRelativePath);

        private static string BaseModelsRelativePath = $@"{AppPath}\MLModels";
        private static string ModelRelativePath = $"{BaseModelsRelativePath}/GitHubLabelerModel.zip";
        public static string ModelPath = GetAbsolutePath(ModelRelativePath);
        public enum MyTrainerStrategy : int { SdcaMultiClassTrainer = 1, OVAAveragedPerceptronTrainer = 2 , FastTree =3};
 
        public static void train()
        {
            AskCaro.MachineLearning.Program.TrainModel(AskCaro.MachineLearning.Program.DataSetLocation, AskCaro.MachineLearning.Program.ModelPath, MyTrainerStrategy.SdcaMultiClassTrainer);
        }

        public static void TrainModel(string DataSetLocation, string ModelPath, MyTrainerStrategy selectedStrategy)
        {
            Microsoft.ML.MLContext mlContext = new MLContext(seed: 1);
            var trainingDataView = mlContext.Data.LoadFromTextFile<QuestionsIssue>(DataSetLocation, hasHeader: true, separatorChar: '\t', allowSparse: false);

            TrainTestData trainTestSplit = mlContext.Data.TrainTestSplit(trainingDataView, testFraction: 0.2);
            IDataView trainingData = trainTestSplit.TrainSet;
            IDataView testData = trainTestSplit.TestSet;

            var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "Label", inputColumnName: nameof(QuestionsIssue.Answer))
                         .Append(mlContext.Transforms.Text.FeaturizeText(outputColumnName: "DescriptionFeaturized", inputColumnName: nameof(QuestionsIssue.Question)))
                           .Append(mlContext.Transforms.Concatenate(outputColumnName: "Features", "DescriptionFeaturized"))
                           .AppendCacheCheckpoint(mlContext);

            IEstimator<ITransformer> trainer = null;
            switch (selectedStrategy)
            {
                case MyTrainerStrategy.SdcaMultiClassTrainer:
                    trainer = mlContext.MulticlassClassification.Trainers.SdcaNonCalibrated("Label","Features");
                    break;
                default:
                    break;
            }

            var trainingPipeline = dataProcessPipeline.Append(trainer).Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var trainedModel = trainingPipeline.Fit(trainingDataView);
             

            // STEP 5: Evaluate the model and show accuracy stats
            var predictions = trainedModel.Transform(testData);
            var metrics = mlContext.MulticlassClassification.Evaluate(data: predictions, labelColumnName: "Label", scoreColumnName: "Score");

            // STEP 6: Save/persist the trained model to a .ZIP file
            mlContext.Model.Save(trainedModel, trainingData.Schema, ModelPath);

        }

        public static QuestionsIssuePrediction BuildModel(string ModelPath, QuestionsIssue issue)
        {
            Microsoft.ML.MLContext mlContext = new MLContext(seed: 1);
            //Define DataViewSchema for data preparation pipeline and trained model
            DataViewSchema modelSchema;

            // Load trained model
            ITransformer trainedModel = mlContext.Model.Load(ModelPath, out modelSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<QuestionsIssue, QuestionsIssuePrediction>(trainedModel);
            var prediction = predEngine.Predict(issue);
            return prediction;
        }


        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }
    }
}
