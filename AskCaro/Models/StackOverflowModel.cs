﻿using System;
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
        public int Similar { get; set; }
        public string Title { get; set; }
        public string HtmlDescription { get; set; }
        public string TextDescription { get; set; }
        public string LinkHref { get; set; }
        public string HtmlAnswers { get; set; }
        public string SiteClone { get; set; }
        public virtual List<AnswerModel> Answers { get; set; }

    }

    [Table("Answer")]
    public class AnswerModel
    {
        [Key]
        public Guid AnswerId { get; set; }
        public Int32 voteCount { get; set; }
        public string Htmldescription { get; set; }
        public string Textdescription { get; set; }

    }

    [Table("Conversations")]
    public class ConversationsModel
    {
        [Key]
        public Guid ConversationsId { get; set; }
        public string Question { get; set; }
        public string HtmlAnswers { get; set; }
        public DateTime CreaDate { get; set; }
        public virtual CategoriesModel Categories { get; set; }

    }

    [Table("Categories")]
    public class CategoriesModel
    {
        [Key]
        public Guid categorieId { get; set; }
        public string Name { get; set; }
        public DateTime CreaDate { get; set; }
    }
    [Table("Tags")]
    public class TagModel
    {
        [Key]
        public Guid TagId { get; set; }
        public string Name { get; set; }
        public string SiteClone { get; set; }
        public DateTime CreaDate { get; set; }
    }

}
