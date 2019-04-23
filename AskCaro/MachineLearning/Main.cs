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
        private static string AppPath => @"C:\Users\AhmedOumezzine\source\repos\creastudio-inc\AskCaro\AskCaro\MachineLearning\";

        private static string BaseDatasetsRelativePath = $@"{AppPath}/Data";
        // private static string DataSetRelativePath = $"{BaseDatasetsRelativePath}/corefx-issues-train.tsv";
        private static string DataSetRelativePath = $"{BaseDatasetsRelativePath}/MyDataTable.csv";
        private static string DataSetLocation = GetAbsolutePath(DataSetRelativePath);

        private static string BaseModelsRelativePath = $@"{AppPath}/MLModels";
        private static string ModelRelativePath = $"{BaseModelsRelativePath}/GitHubLabelerModel.zip";
        public static string ModelPath = GetAbsolutePath(ModelRelativePath);
        public enum MyTrainerStrategy : int { SdcaMultiClassTrainer = 1, OVAAveragedPerceptronTrainer = 2 };

        //public static async Task Main()
        //{
        //    //  DataTable table = AskCaro_console.CsvFile.DataTableExtensions.GetTable();
        //    //  table.WriteToCsvFile(DataSetLocation);
        //    //  TrainModel(DataSetLocation, DataSetLocation, MyTrainerStrategy.SdcaMultiClassTrainer);
        //    ChatIssue issue = new ChatIssue() { Question = "Thank you!", Answer = "" };
        //    var test = BuildModel(ModelPath, issue);
        //}
        public static void TrainModel(string DataSetLocation, string ModelPath, MyTrainerStrategy selectedStrategy)
        {
            Microsoft.ML.MLContext mlContext = new MLContext(seed: 1);
            var trainingDataView = mlContext.Data.LoadFromTextFile<ChatIssue>(DataSetLocation, hasHeader: true, separatorChar: '\t', allowSparse: false);
            var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: DefaultColumnNames.Label, inputColumnName: nameof(ChatIssue.Answer))
                            .Append(mlContext.Transforms.Text.FeaturizeText(outputColumnName: "TitleFeaturized", inputColumnName: nameof(ChatIssue.Question)))
                          .Append(mlContext.Transforms.Text.FeaturizeText(outputColumnName: "DescriptionFeaturized", inputColumnName: nameof(ChatIssue.More)))
                            .Append(mlContext.Transforms.Concatenate(outputColumnName: DefaultColumnNames.Features, "TitleFeaturized", "DescriptionFeaturized"))
                            .AppendCacheCheckpoint(mlContext);
            // Use in-memory cache for small/medium datasets to lower training time. 
            // Do NOT use it (remove .AppendCacheCheckpoint()) when handling very large datasets.

            IEstimator<ITransformer> trainer = null;
            switch (selectedStrategy)
            {
                case MyTrainerStrategy.SdcaMultiClassTrainer:
                    trainer = mlContext.MulticlassClassification.Trainers.StochasticDualCoordinateAscent(DefaultColumnNames.Label,
                                                                                                         DefaultColumnNames.Features);
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
                default:
                    break;
            }

            var trainingPipeline = dataProcessPipeline.Append(trainer).Append(mlContext.Transforms.Conversion.MapKeyToValue(DefaultColumnNames.PredictedLabel));

            var trainedModel = trainingPipeline.Fit(trainingDataView);


            using (var fs = new FileStream(ModelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
                mlContext.Model.Save(trainedModel, fs);

        }

        public static ChatIssuePrediction BuildModel(string ModelPath, ChatIssue issue)
        {
            Microsoft.ML.MLContext mlContext = new MLContext(seed: 1);
            ITransformer trainedModel;
            using (FileStream stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                trainedModel = mlContext.Model.Load(stream);
            }
            var predEngine = trainedModel.CreatePredictionEngine<ChatIssue, ChatIssuePrediction>(mlContext);
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
