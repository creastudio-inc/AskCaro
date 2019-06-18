using Microsoft.ML.Data;
using System;

namespace AskCaro.MachineLearning.DataStructures
{

    public class QuestionsIssue
    {

        [LoadColumn(0)]
        public string Question { get; set; }
        [LoadColumn(1)]
        public string Answer { get; set; } 
    }
}
