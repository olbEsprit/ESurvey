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
    public class AnswerCrudLogic
    {
        public async Task<Result> CreateAnswer(AnswerUiModel answerUi)
        {
            using (var holder = new RepositoryHolder())
            {
                var answerEntity = new AnswerMapper().UiToEntity(answerUi);
                holder.AnswerRepository.Insert(answerEntity);
                await holder.SaveChangesAsync();
                return new Result();
            }
        }

        public async Task<Result> DeleteAnswer(int ansverId)
        {
            using (var holder = new RepositoryHolder())
            {
                var answer = holder.AnswerRepository.GetById(ansverId);
                holder.AnswerRepository.Remove(answer);
                await holder.SaveChangesAsync();
                return new Result();
            }
        }

        public async Task<DataResult<List<AnswerUiModel>>> GetQuestionAnswers(int questionId)
        {

            using (var holder = new RepositoryHolder())
            {
                var repository = holder.AnswerRepository;
                var answers = await repository.FetchByAsync(a => a.QuestionId == questionId);
                var mapper = new AnswerMapper();
                var answersUi = answers.Select(a => mapper.EntityToUi(a)).ToList();
                return new DataResult<List<AnswerUiModel>>(answersUi);
            }
        }

        public async Task<Result> UpdateAnswer(AnswerUiModel answerUi)
        {
            using (var holder = new RepositoryHolder())
            {
                AnswerMapper mapper = new AnswerMapper();
                var entity = mapper.UiToEntity(answerUi);
                holder.AnswerRepository.Update(entity);
                await holder.SaveChangesAsync();
                return new Result();
            }
        }

        public async Task<DataResult<AnswerUiModel>> GetAnswer(int ansverId)
        {
            using (var holder = new RepositoryHolder())
            {
                var answerEntity = await holder.AnswerRepository.GetByIdAsync(ansverId);
                var mapper = new AnswerMapper();
                var answer = mapper.EntityToUi(answerEntity);
                return new DataResult<AnswerUiModel>(answer);
            }
        } 
    }
}
