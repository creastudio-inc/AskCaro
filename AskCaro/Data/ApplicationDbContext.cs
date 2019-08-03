using System;
using System.Collections.Generic;
using System.Text;
using AskCaro.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AskCaro.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<AnswerModel> Answers { get; set; }
        public DbSet<ConversationsModel> Conversations { get; set; }
        public DbSet<CategoriesModel> Categories { get; set; }
        public DbSet<TagModel> Tags { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
