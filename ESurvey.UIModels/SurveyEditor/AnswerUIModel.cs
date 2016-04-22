using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESurvey.UIModels.SurveyEditor
{
    public class AnswerUIModel
    {
        public string Title { get; set; }
        public bool IsHidden { get; set; }
        public int Id { get; set; }
        public int QuestionId { get; set; }

    }
}
