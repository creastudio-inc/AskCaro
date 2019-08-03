﻿// <auto-generated />
using System;
using AskCaro.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AskCaro.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AskCaro.Models.AnswerModel", b =>
                {
                    b.Property<Guid>("AnswerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Htmldescription");

                    b.Property<Guid?>("QuestionModelQuestionId");

                    b.Property<string>("Textdescription");

                    b.Property<int>("voteCount");

                    b.HasKey("AnswerId");

                    b.HasIndex("QuestionModelQuestionId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("AskCaro.Models.CategoriesModel", b =>
                {
                    b.Property<Guid>("categorieId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreaDate");

                    b.Property<string>("Name");

                    b.HasKey("categorieId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AskCaro.Models.ConversationsModel", b =>
                {
                    b.Property<Guid>("ConversationsId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CategoriescategorieId");

                    b.Property<DateTime>("CreaDate");

                    b.Property<string>("HtmlAnswers");

                    b.Property<string>("Question");

                    b.HasKey("ConversationsId");

                    b.HasIndex("CategoriescategorieId");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("AskCaro.Models.QuestionModel", b =>
                {
                    b.Property<Guid>("QuestionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HtmlAnswers");

                    b.Property<string>("HtmlDescription");

                    b.Property<string>("LinkHref");

                    b.Property<int>("Similar");

                    b.Property<string>("SiteClone");

                    b.Property<string>("TextDescription");

                    b.Property<string>("Title");

                    b.HasKey("QuestionId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("AskCaro.Models.TagModel", b =>
                {
                    b.Property<Guid>("TagId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreaDate");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("PaginationActuel");

                    b.Property<int>("PaginationFound");

                    b.Property<string>("SiteClone");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("AskCaro.Models.TagQuestionModel", b =>
                {
                    b.Property<Guid>("TagQuestionId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("QuestionId");

                    b.Property<Guid>("TagId");

                    b.HasKey("TagQuestionId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("TagId");

                    b.ToTable("TagsQuestions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AskCaro.Models.AnswerModel", b =>
                {
                    b.HasOne("AskCaro.Models.QuestionModel")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionModelQuestionId");
                });

            modelBuilder.Entity("AskCaro.Models.ConversationsModel", b =>
                {
                    b.HasOne("AskCaro.Models.CategoriesModel", "Categories")
                        .WithMany()
                        .HasForeignKey("CategoriescategorieId");
                });

            modelBuilder.Entity("AskCaro.Models.TagQuestionModel", b =>
                {
                    b.HasOne("AskCaro.Models.QuestionModel", "QuestionModel")
                        .WithMany("TagQuestionModels")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AskCaro.Models.TagModel", "TagModel")
                        .WithMany("TagQuestionModels")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
