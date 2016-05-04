using ESurvey.Entity;
using ESurvey.UIModels;

namespace ESurvey.BL.Mappers
{
    public class SurveyListMapper
    {
        public SurveyListUi EntityToUi(Survey surveyEntity)
        {
           return new SurveyListUi()
                    {
                        Id = surveyEntity.Id,
                        Title = surveyEntity.Title
                    };
        }


        public Survey UiToEntity(SurveyListUi surveyModel)
        {
            return new Survey()
            {
                Id = surveyModel.Id,
                Title = surveyModel.Title
            };
        }


    }
}
