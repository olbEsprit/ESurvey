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
    public class QuestionCrudLogic
    {
        public async Task<Result> CreateQuestion(QuestionUiModel question)
        {

            using (var holder = new RepositoryHolder())
            {
                var entry = new QuestionMapper().UiToEntity(question);
                holder.QuestionRepository.Insert(entry);
                await holder.SaveChangesAsync();
                return new Result();
            }
        }
        /*
        public async void UpdateQuestion(AlterQuestionRequest request)
        {
            using (var holder = new RepositoryHolder())
            {
                var questionMapper = new QuestionMapper();
                var question = questionMapper.UiToEntity(request.Question);


                question.SurveyId = request.SurveyId;
                holder.QuestionRepository.Update(question);
                

                
                var aMapper = new AnswerMapper();
                foreach (var answer in aMapper.UiToEntity(request.Answers))
                {
                    answer.QuestionId = question.Id;
                    if (answer.Id != 0)
                    {
                        holder.AnswerRepository.Update(answer);
                    }
                    else
                    {
                        holder.AnswerRepository.Insert(answer);
                    }
                }

                var deleteId = request.DeletedAnswersId;
                holder.AnswerRepository.RemoveBy(a => deleteId.Contains(a.Id));

                await holder.SaveChangesAsync();
            }
        }
        */
        public async Task<Result> DeleteQuestion(int id)
        {
            using (var holder = new RepositoryHolder())
            {
                holder.QuestionRepository.RemoveBy(q => q.Id == id);
                await holder.SaveChangesAsync();
                return new Result();
            }
        }

        public async Task<DataResult<QuestionUiModel>> GetQuestion(int id)
        {
            using (var holder = new RepositoryHolder())
            {
                var questions = holder.QuestionRepository;
                var question = await questions.GetByIdAsync(id);
                //var answers = await holder.AnswerRepository.FetchByAsync(a => a.QuestionId == question.Id);
                var uiQuestion = new QuestionMapper().EntityToUi(question);
                
                return new DataResult<QuestionUiModel>(uiQuestion);

            }
        }

        public async Task<DataResult<List<QuestionUiModel>>> GetSurveyQuestions(int surveyId)
        {
            using (var holder = new RepositoryHolder())
            {
                var questions = await holder.QuestionRepository.FetchByAsync(q => q.SurveyId == surveyId);
                var mapper = new QuestionMapper();
                var data = questions.Select(q => mapper.EntityToUi(q)).ToList();
                return new DataResult<List<QuestionUiModel>>(data);
            }
        }

        public async Task<Result> UpdateQuestion(QuestionUiModel questionUi)
        {
            using (var holder = new RepositoryHolder())
            {
                var mapper = new QuestionMapper();
                var questionEntity = mapper.UiToEntity(questionUi);
                holder.QuestionRepository.Update(questionEntity);
                await holder.SaveChangesAsync();
                return new Result();
            }
        } 
    }
}
