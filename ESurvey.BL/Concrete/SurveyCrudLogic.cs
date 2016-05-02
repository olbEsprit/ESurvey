using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.BL.Mappers;
using ESurvey.DAL.Concrate;
using ESurvey.UIModels;
using ESurvey.UIModels.SurveyEditor;

namespace ESurvey.BL.Concrete
{
    public class SurveyCrudLogic
    {
        public async Task<DataResult<int>> CreateSurvey(SurveyUiModel surveyModel)
        {
            using (var holder = new RepositoryHolder())
            {
                var surveyEntity = new SurveyMapper().UiToEntity(surveyModel);
                holder.SurveyRepository.Insert(surveyEntity);
                await holder.SaveChangesAsync();
                return new DataResult<int>(surveyEntity.Id);
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

        public async Task<DataResult<List<SurveyUiModel>>> GetUserSurveys(string ownerId)
        {

            using (var holder = new RepositoryHolder())
            {
                var surveys = await holder.SurveyRepository.FetchByAsync(s => s.OwnerId == ownerId);
                var mapper = new SurveyMapper();
                var data = surveys.Select(s => mapper.EntityToUi(s)).ToList();
                var result = new DataResult<List<SurveyUiModel>>(data);
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

    }
}
