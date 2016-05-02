using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.Entity;
using ESurvey.UIModels.SurveyEditor;

namespace ESurvey.BL.Mappers
{
    public class SurveyMapper
    {
        public Survey UiToEntity(SurveyUiModel model)
        {
            return new Survey()
            {
                Id = model.Id,
                OwnerId = model.OwnerId,
                Title = model.Title
            };
        }

        public SurveyUiModel EntityToUi(Survey entity)
        {
            return new SurveyUiModel()
            {
                Id = entity.Id,
                OwnerId = entity.OwnerId,
                Title = entity.Title
            };
        }



        
    }
}
