
#pragma warning disable 649 // We don't care about unsused fields here, because they are mapped with the input file.

using Microsoft.ML.Data;

namespace AskCaro.MachineLearning.DataStructures
{

    public class ChatIssuePrediction
    {
        public string Question;
        [ColumnName("PredictedLabel")]
        public string Answer;
        public string Tags;
        public string More;

    }

    public class QuestionsIssuePrediction
    {

        public float Probability { get; set; }
        public float Score { get; set; }

        [ColumnName("PredictedLabel")]
        public string answer { get; set; }
    }
}
