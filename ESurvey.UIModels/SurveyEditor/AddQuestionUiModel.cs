using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESurvey.UIModels.SurveyEditor
{
    public class AddQuestionUiModel
    {
        public int SurveyId { get; set; }
        public int QuestionTypeId { get; set; }
        public string Title { get; set; } = "Untitled";
    }
}
