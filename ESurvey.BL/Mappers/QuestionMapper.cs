using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.Entity;
using ESurvey.UIModels.SurveyEditor;

namespace ESurvey.BL.Mappers
{
    public class QuestionMapper
    {
        public Questions UiToEntity(QuestionUiModel question)
        {
            return new Questions()
            {
                Id = question.Id,
                SurveyId = question.SurveyId,
                Title = question.Title,
                Is_matrix = question.IsMatrix,
                Is_hidden = question.IsHidden,
                RequireAnswer = question.RequriedAnswer,
                AllowMultipleAnswers = question.AllowMultiple,
                OtherAnswer = question.OtherAnswer,
                Number = question.Number,
                QuestionType = question.Type

                

            };
        }

        public QuestionUiModel EntityToUi(Questions question)
        {
            return new QuestionUiModel()
            {
                Id = question.Id,
                SurveyId = question.SurveyId,
                Title = question.Title,
                IsMatrix = question.Is_matrix,
                IsHidden = question.Is_hidden,
                RequriedAnswer = question.RequireAnswer,
                AllowMultiple = question.AllowMultipleAnswers,
                OtherAnswer = question.OtherAnswer,
                Number = question.Number,
                Type = question.QuestionType
            };
        }

    }
}
