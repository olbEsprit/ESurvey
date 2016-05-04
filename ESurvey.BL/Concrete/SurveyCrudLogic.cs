using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.BL.Mappers;
using ESurvey.DAL.Concrate;
using ESurvey.Entity;
using ESurvey.UIModels;
using ESurvey.UIModels.SurveyEditor;

namespace ESurvey.BL.Concrete
{
    public class SurveyCrudLogic
    {
        public async Task<DataResult<SurveyListUi>> CreateSurvey(SurveyListUi surveyModel, string ownerId)
        {
            using (var holder = new RepositoryHolder())
            {

                var surveyEntity = new SurveyListMapper().UiToEntity(surveyModel);
                surveyEntity.OwnerId = ownerId;
                
                holder.SurveyRepository.Insert(surveyEntity);
                await holder.SaveChangesAsync();

                var data = new SurveyListMapper().EntityToUi(surveyEntity);
                return new DataResult<SurveyListUi>(data);
            }
        }

        public async Task<Result> DeleteSurvey(int surveyId)
        {
            using (var holder = new RepositoryHolder())
            {
                var survey = holder.SurveyRepository.GetById(surveyId);
                holder.SurveyRepository.Remove(survey);
                await holder.SaveChangesAsync();
                return new Result();
            }
        }

        public async Task<DataResult<List<SurveyListUi>>> GetUserSurveys(string ownerId)
        {

            using (var holder = new RepositoryHolder())
            {
                var surveys = await holder.SurveyRepository.FetchByAsync(s => s.OwnerId == ownerId);
                var mapper = new SurveyListMapper();
                var data = surveys.Select(s => mapper.EntityToUi(s)).ToList();
                var result = new DataResult<List<SurveyListUi>>(data);
                return result;
            }
        }

        public async Task<Result> UpdateSurvey(SurveyUiModel surveyModel)
        {
            using (var holder = new RepositoryHolder())
            {
                SurveyMapper mapper = new SurveyMapper();
                var surveyEntity = mapper.UiToEntity(surveyModel);
                holder.SurveyRepository.Update(surveyEntity);
                await holder.SaveChangesAsync();
                return new Result();
            }
        }

        public async Task<DataResult<SurveyUiModel>> GetSurvey(int surveyId)
        {
            using (var holder = new RepositoryHolder())
            {
                var mapper = new SurveyMapper();
                var surveyEntity = await holder.SurveyRepository.GetByIdAsync(surveyId);

                if (surveyEntity == null)
                    return new DataResult<SurveyUiModel>("Survey don't exist");

                var surveyModel = mapper.EntityToUi(surveyEntity);
                return new DataResult<SurveyUiModel>(surveyModel);
            }
        } 

    }
}
