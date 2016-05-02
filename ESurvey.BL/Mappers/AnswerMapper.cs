using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.Entity;
using ESurvey.UIModels.SurveyEditor;

namespace ESurvey.BL.Mappers
{
    public class AnswerMapper
    {
        public AnswerUiModel EntityToUi(QuestionAnswers answer)
        {
            return new AnswerUiModel()
            {
                Id = answer.Id,
                Title = answer.Title,
                QuestionId = answer.QuestionId,
                IsHidden = answer.Is_hidden
            };
        }

       public QuestionAnswers UiToEntity(AnswerUiModel answer)
        {
            return new QuestionAnswers()
            {
                Id = answer.Id,
                Title = answer.Title,
                QuestionId = answer.QuestionId,
                Is_hidden = answer.IsHidden
            };
        }

        public List<QuestionAnswers> UiToEntity(List<AnswerUiModel> answers)
        {
            return answers.Select(UiToEntity).ToList();
        }

        public List<AnswerUiModel> EntityToUi(List<QuestionAnswers> answers)
        {
            return answers.Select(a => EntityToUi(a)).ToList();
        }


    }
}
