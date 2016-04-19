//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Questions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Questions()
        {
            this.AnsweredQuestions = new HashSet<AnsweredQuestions>();
            this.QuestionAnswers = new HashSet<QuestionAnswers>();
            this.Questions1 = new HashSet<Questions>();
        }
    
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string Title { get; set; }
        public int QuestionType { get; set; }
        public Nullable<bool> Is_hidden { get; set; }
        public Nullable<bool> Is_matrix { get; set; }
        public int Parent_Question { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnsweredQuestions> AnsweredQuestions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionAnswers> QuestionAnswers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Questions> Questions1 { get; set; }
        public virtual Questions Questions2 { get; set; }
        public virtual QuestionTypes QuestionTypes { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
