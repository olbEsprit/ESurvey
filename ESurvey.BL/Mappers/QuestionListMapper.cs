using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.Entity;
using ESurvey.UIModels.SurveyEditor;

namespace ESurvey.BL.Mappers
{
    public class QuestionListMapper
    {
        //public Questions UiToEntity(QuestionListUiModel question)
        //{
        //    return new Questions()
        //    {
        //        Id = question.Id,
        //        Title = question.Title,
        //        Number = question.Number

        //    };
        //}

        public QuestionListUiModel EntityToUi(Questions question)
        {
            return new QuestionListUiModel()
            {
                Id = question.Id,
                Title = question.Title,
                Number = question.Number

            };
        }
    }
}
