using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ESurvey.UIModels.SurveyEditor
{
    public class QuestionUiModel
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public bool IsMatrix { get; set; } = false;
        public bool IsHidden { get; set; } = false;
        public string Title { get; set; }
        public bool AllowMultiple { get; set; }
        public bool RequriedAnswer { get; set; }
        public bool OtherAnswer { get; set; }
        public int Number { get; set; }
        public int Type { get; set; }
    }
}
