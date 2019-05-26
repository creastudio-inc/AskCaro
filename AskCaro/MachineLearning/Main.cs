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

namespace AskCaro.MachineLearning
{
    public static class Program
    {
        private static string AppPath => @"C:\Users\ahmed\source\repos\AskCaro\AskCaro\MachineLearning\";

        private static string BaseDatasetsRelativePath = $@"{AppPath}Data";
        // private static string DataSetRelativePath = $"{BaseDatasetsRelativePath}/corefx-issues-train.tsv";
        private static string DataSetRelativePath = $"{BaseDatasetsRelativePath}/people.csv";
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
            var trainingDataView = mlContext.Data.LoadFromTextFile<QuestionsIssue>(DataSetLocation, hasHeader: true, separatorChar: ',', allowSparse: false);
            var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: DefaultColumnNames.Label, inputColumnName: nameof(QuestionsIssue.answer))
                          .Append(mlContext.Transforms.Text.FeaturizeText(outputColumnName: "TitleFeaturized", inputColumnName: nameof(QuestionsIssue.Title)))
                          .Append(mlContext.Transforms.Text.FeaturizeText(outputColumnName: "DescriptionFeaturized", inputColumnName: nameof(QuestionsIssue.LongDescription)))
                            .Append(mlContext.Transforms.Concatenate(outputColumnName: DefaultColumnNames.Features, "TitleFeaturized", "DescriptionFeaturized"))
                            .AppendCacheCheckpoint(mlContext);
            // Use in-memory cache for small/medium datasets to lower training time. 
            // Do NOT use it (remove .AppendCacheCheckpoint()) when handling very large datasets.

            IEstimator<ITransformer> trainer = null;
            switch (selectedStrategy)
            {
                case MyTrainerStrategy.SdcaMultiClassTrainer:
                    trainer = mlContext.MulticlassClassification.Trainers.StochasticDualCoordinateAscent(DefaultColumnNames.Label,DefaultColumnNames.Features);
                    break;
                case MyTrainerStrategy.OVAAveragedPerceptronTrainer:
                    {
                        // Create a binary classification trainer.
                        var averagedPerceptronBinaryTrainer = mlContext.BinaryClassification.Trainers.AveragedPerceptron(DefaultColumnNames.Label,
                                                                                                                         DefaultColumnNames.Features, numIterations: 10);
                        // Compose an OVA (One-Versus-All) trainer with the BinaryTrainer.
                        // In this strategy, a binary classification algorithm is used to train one classifier for each class, "
                        // which distinguishes that class from all other classes. Prediction is then performed by running these binary classifiers, "
                        // and choosing the prediction with the highest confidence score.
                        trainer = mlContext.MulticlassClassification.Trainers.OneVersusAll(averagedPerceptronBinaryTrainer);

                        break;
                    }
                case MyTrainerStrategy.FastTree:
                    trainer = mlContext.BinaryClassification.Trainers.FastTree();
                    break;
                default:
                    break;
            }

            var trainingPipeline = dataProcessPipeline.Append(trainer).Append(mlContext.Transforms.Conversion.MapKeyToValue(DefaultColumnNames.PredictedLabel));

            var trainedModel = trainingPipeline.Fit(trainingDataView);


            using (var fs = new FileStream(ModelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
                mlContext.Model.Save(trainedModel, fs);

        }

        public static QuestionsIssuePrediction BuildModel(string ModelPath, QuestionsIssue issue)
        {
            Microsoft.ML.MLContext mlContext = new MLContext(seed: 1);
            ITransformer trainedModel;
            using (FileStream stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                trainedModel = mlContext.Model.Load(stream);
            }
            var predEngine = trainedModel.CreatePredictionEngine<QuestionsIssue, QuestionsIssuePrediction>(mlContext);
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
