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

    
    public class SurveyEditor
    {

        

        public SurveyAccessManager AccessManager { get; set; }
        
        public async void CreateSurvey(SurveyUIModel surveyModel)
        {
            using (var holder = new RepositoryHolder())
            {
                var survey = new Survey
                {
                    Title = surveyModel.Title,
                    OwnerId = surveyModel.OwnerId
                };

                holder.SurveyRepository.Insert(survey);
                await holder.SaveChangesAsync();
            }
        }

        public async void DeleteSurvey(int id)
        {
            using (var holder = new RepositoryHolder())
            {
                holder.SurveyRepository.RemoveBy(s => s.Id == id);
                await holder.SaveChangesAsync();
            }
        }

        

        public async void CreateQuestion(QuestionUIModel question)
        {

            using (var holder = new RepositoryHolder())
            {                
                var entry = new QuestionMapper().UIToEntity(question);
                holder.QuestionRepository.Insert(entry);
                await holder.SaveChangesAsync();
            }
        }

        public async void UpdateQuestion(AlterQuestionRequest request)
        {
            using (var holder = new RepositoryHolder())
            {
                var questionMapper = new QuestionMapper();
                var question = questionMapper.UIToEntity(request.Question);


                question.SurveyId = request.SurveyId;
                holder.QuestionRepository.Update(question);
                

                
                var aMapper = new AnswerMapper();
                foreach (var answer in aMapper.UIToEntity(request.Answers))
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

                var deleteId = request.DeletedAnswersID;
                holder.AnswerRepository.RemoveBy(a => deleteId.Contains(a.Id));

                await holder.SaveChangesAsync();
            }
        }

        public async void DeleteQuestion(int id)
        {
            using (var holder = new RepositoryHolder())
            {
                holder.QuestionRepository.RemoveBy(q => q.Id == id);
                await holder.SaveChangesAsync();
            }
        }



        public async Task<QuestionUIModel>  GetQuestion(int id)
        {
            using (var holder = new RepositoryHolder())
            {
                var questions = holder.QuestionRepository;
                var question = await questions.GetByIdAsync(id);

                //var subquestions = await questions.FetchByAsync(q => q.Parent_Question == question.Id);
                var answers = await holder.AnswerRepository.FetchByAsync(a => a.QuestionId == question.Id);

                var UIQuestion = new QuestionMapper().EntityToUI(question);
                
                var aMapper = new AnswerMapper();

                UIQuestion.Answers = aMapper.EntityToUI(answers);

                return UIQuestion;

            }
        }

        public async Task<IEnumerable<Survey>> GetUserSurveys(string ownerId)
        {
            using (var holder = new RepositoryHolder())
            {
                return await holder.SurveyRepository.FetchByAsync(s => s.OwnerId == ownerId);
            }
        }



    }

}
