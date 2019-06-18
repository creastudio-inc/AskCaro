
#pragma warning disable 649 // We don't care about unsused fields here, because they are mapped with the input file.

using Microsoft.ML.Data;

namespace AskCaro.MachineLearning.DataStructures
{
    public class QuestionsIssuePrediction
    {
        [ColumnName("PredictedLabel")]
        public string Answer { get; set; }
    }
}
