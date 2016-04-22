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
        public AnswerUIModel EntityToUI(QuestionAnswers answer)
        {
            return new AnswerUIModel()
            {
                Id = answer.Id,
                Title = answer.Title,
                QuestionId = answer.QuestionId,
                IsHidden = answer.Is_hidden
            };
        }

       public QuestionAnswers UIToEntity(AnswerUIModel answer)
        {
            return new QuestionAnswers()
            {
                Id = answer.Id,
                Title = answer.Title,
                QuestionId = answer.QuestionId,
                Is_hidden = answer.IsHidden
            };
        }

        public List<QuestionAnswers> UIToEntity(List<AnswerUIModel> answers)
        {
            return answers.Select(a => UIToEntity(a)).ToList();
        }

        public List<AnswerUIModel> EntityToUI(List<QuestionAnswers> answers)
        {
            return answers.Select(a => EntityToUI(a)).ToList();
        }


    }
}
