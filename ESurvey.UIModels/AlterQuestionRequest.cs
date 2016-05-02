using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.UIModels.SurveyEditor;

namespace ESurvey.UIModels
{
    public class AlterQuestionRequest:Request
    {
        public QuestionUiModel Question { get; set; }
        public List<AnswerUiModel> Answers { get; set; }
        public List<int> DeletedAnswersId { get; set; } 
    }
}
