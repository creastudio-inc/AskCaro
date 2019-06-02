
#pragma warning disable 649 // We don't care about unsused fields here, because they are mapped with the input file.

using Microsoft.ML.Data;
using System;

namespace AskCaro.MachineLearning.DataStructures
{
    //The only purpose of this class is for peek data after transforming it with the pipeline

    public class ChatIssue
    {
        [LoadColumn(0)] 

        public string Question;

        [LoadColumn(1)]
        public string Answer; 

        [LoadColumn(2)]
        public string Tags;

        [LoadColumn(3)]
        public string More;

        [LoadColumn(4)]
        public DateTime Created;
    }



    public class QuestionsIssue
    {
        [LoadColumn(0)]
        public bool Label { get; set; }
        [LoadColumn(1)]
        public string LongDescription { get; set; }
        [LoadColumn(2)]
        public string answer { get; set; } 
    }
}
