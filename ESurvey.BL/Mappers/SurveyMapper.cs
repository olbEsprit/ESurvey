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
        public Survey UIToEntity(SurveyUIModel model)
        {
            return new Survey()
            {
                Id = model.Id,
                OwnerId = model.OwnerId,
                Title = model.Title
            };
        }

        public SurveyUIModel EntityToUI(Survey entity)
        {
            return new SurveyUIModel()
            {
                Id = entity.Id,
                OwnerId = entity.OwnerId,
                Title = entity.Title
            };
        }
    }
}
