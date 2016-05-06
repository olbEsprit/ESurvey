using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.Entity;
using ESurvey.UIModels.SurveyEditor;

namespace ESurvey.BL.Mappers
{
    public class AddQuestionMapper
    {
        public Questions UiToEntity(AddQuestionUiModel questionUi)
        {
            return new Questions()
            {
                QuestionType = questionUi.QuestionTypeId,
                SurveyId = questionUi.SurveyId,
                Title = questionUi.Title
                
            };
        }

    }
}
