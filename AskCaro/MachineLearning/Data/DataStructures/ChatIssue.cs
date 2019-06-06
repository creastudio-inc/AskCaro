using Microsoft.ML.Data;
using System;

namespace AskCaro.MachineLearning.DataStructures
{

    public class QuestionsIssue
    {
        [LoadColumn(0)]
        public Int32 Similar { get; set; }
        [LoadColumn(1)]
        public string Question { get; set; }
        [LoadColumn(2)]
        public string Answer { get; set; } 
    }
}
