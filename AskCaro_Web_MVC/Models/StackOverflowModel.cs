using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AskCaro_Web_MVC.Models
{
    [Table("Question")]
    public class QuestionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid Id { get; set; }
        public int Similar { get; set; }
        public string Title { get; set; }
        public string HtmlDescription { get; set; }
        public string TextDescription { get; set; }
        public string LinkHref { get; set; }
        public string HtmlAnswers { get; set; }
        public string Tag { get; set; }
        public virtual List<AnswerModel> Answers { get; set; }

    }

    [Table("Answer")]
    public class AnswerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid Id { get; set; }
        public Int32 voteCount { get; set; }
        public string Htmldescription { get; set; }
        public string Textdescription { get; set; }
        public Guid QuestionId { get; set; }
        public virtual QuestionModel Question { get; set; }

    }

    [Table("Conversations")]
    public class ConversationsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string HtmlAnswers { get; set; }
        public DateTime CreaDate { get; set; }
        public virtual CategoriesModel Categories { get; set; }

    }

    [Table("Categories")]
    public class CategoriesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid categorieId { get; set; }
        public string Name { get; set; }
        public DateTime CreaDate { get; set; }
    }
    [Table("Tags")]
    public class TagModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid TagId { get; set; }
        public string Name { get; set; }
        public string SiteClone { get; set; }
        public DateTime CreaDate { get; set; }
    }

}
