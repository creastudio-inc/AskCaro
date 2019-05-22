using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AskCaro.Models
{
    [Table("Question")]
    public class QuestionModel
    {
        [Key]
        public Guid QuestionId { get; set; }

        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string LinkHref { get; set; }
        public virtual List<TagModel> Tags { get; set; }
        public virtual List<AnswerModel> Answers { get; set; }

    }
    [Table("Tag")]
    public class TagModel
    {
        [Key]
        public Guid TagId { get; set; }
        public string Title { get; set; }
 
    }
    [Table("Answer")]
    public class AnswerModel
    {
        [Key]
        public Guid AnswerId { get; set; }
        public string Description { get; set; }
 
    }
}
