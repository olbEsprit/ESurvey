﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ESurvey.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ESurveyEntities : DbContext
    {
        public ESurveyEntities()
            : base("name=ESurveyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AnsweredQuestions> AnsweredQuestions { get; set; }
        public virtual DbSet<AnsweredQuestionsOptions> AnsweredQuestionsOptions { get; set; }
        public virtual DbSet<AnswerTypes> AnswerTypes { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<QuestionAnswers> QuestionAnswers { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<QuestionTypes> QuestionTypes { get; set; }
        public virtual DbSet<Survey> Survey { get; set; }
        public virtual DbSet<SurveySessions> SurveySessions { get; set; }
        public virtual DbSet<Voters> Voters { get; set; }
    }
}
