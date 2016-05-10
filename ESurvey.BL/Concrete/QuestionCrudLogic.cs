using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.BL.Mappers;
using ESurvey.Common.Enums;
using ESurvey.DAL.Concrate;
using ESurvey.DAL.Concrete;
using ESurvey.Entity;
using ESurvey.UIModels;
using ESurvey.UIModels.SurveyEditor;

namespace ESurvey.BL.Concrete
{
    public class QuestionCrudLogic
    {

        //public async Task<Result> MoveQuestion(int surveyId, int from, int to)
        //{

        //}
        public async Task<int> GetQuestionType(int id)
        {
            using (var holder = new RepositoryHolder())
            {
                return (await holder.QuestionRepository.GetByIdAsync(id)).QuestionType;
            }
        }

        private async Task<Result> CreateMultiSelectQuestion(AddQuestionUiModel question)
        {
            using (var holder = new RepositoryHolder())
            {
                var entity = new AddQuestionMapper().UiToEntity(question);
                var number = await holder.QuestionRepository
                            .GetCountByAsync(q =>
                                q.SurveyId == entity.SurveyId &&
                                (q.Parent_Question == null || q.Parent_Question == 0));
                entity.Number = number + 1;
                holder.QuestionRepository.Insert(entity);
                await holder.SaveChangesAsync();
                return new Result();
            }
        }

        private async Task<Result> CreateMatrixQuestion(AddQuestionUiModel question)
        {
            using (var holder = new RepositoryHolder())
            {
                var entity = new AddQuestionMapper().UiToEntity(question);
                var number = await holder.QuestionRepository
                            .GetCountByAsync(q =>
                                q.SurveyId == entity.SurveyId &&
                                (q.Parent_Question == null || q.Parent_Question == 0));
                entity.Number = number + 1;
                entity.Is_matrix = true;

                holder.QuestionRepository.Insert(entity);
                await holder.SaveChangesAsync();
                return new Result();
            }
        }


        private async Task<Result> CreateOpenQuestion(AddQuestionUiModel question)
        {
            using (var holder = new RepositoryHolder())
            {
                var entity = new AddQuestionMapper().UiToEntity(question);
                var number = await holder.QuestionRepository
                            .GetCountByAsync(q =>
                                q.SurveyId == entity.SurveyId &&
                                (q.Parent_Question == null || q.Parent_Question == 0));
                entity.Number = number + 1;
                entity.OtherAnswer = true;
                holder.QuestionRepository.Insert(entity);
                
                
                await holder.SaveChangesAsync();
                var  answer = new QuestionAnswers()
                    {
                        QuestionId = entity.Id,
                        Title = "Other",
                        IsUserAnswer = true
                    };
                holder.AnswerRepository.Insert(answer);
                await holder.SaveChangesAsync();
                return new Result();
            }
        }

        public async Task<Result> CreateQuestion(AddQuestionUiModel question)
        {
            if (question.QuestionTypeId == (int)QuestionType.Matrix)
                return await CreateMatrixQuestion(question);
            if (question.QuestionTypeId == (int) QuestionType.MultiSelect)
                return await CreateMultiSelectQuestion(question);
            if (question.QuestionTypeId == (int) QuestionType.Open)
                return await CreateOpenQuestion(question);
            return new Result("Invalid Question Type");

        }


        public async Task<Result> ToggleOtherAnswerOption(ToggleOtherAnswerRequest request)
        {
            using (var holder = new RepositoryHolder())
            {

                var ans = await holder.QuestionRepository.GetByIdAsync(request.QuestionId);
                ans.OtherAnswer = request.Toggle;
                holder.QuestionRepository.Update(ans);

                if (request.Toggle == false)
                {
                    holder.AnswerRepository.RemoveBy(a => a.QuestionId == request.QuestionId && a.IsUserAnswer == true);
                }
                else
                {
                    var answer = new QuestionAnswers()
                    {
                        QuestionId = request.QuestionId,
                        IsUserAnswer = true,
                        Title = "Other"
                     };

                    holder.AnswerRepository.Insert(answer);
                }
                await holder.SaveChangesAsync();

            }

            return new Result();
        }
        /*
        public async void UpdateQuestion(AlterQuestionRequest requestUiModel)
        {
            using (var holder = new RepositoryHolder())
            {
                var questionMapper = new QuestionMapper();
                var question = questionMapper.UiToEntity(requestUiModel.Question);


                question.SurveyId = requestUiModel.SurveyId;
                holder.QuestionRepository.Update(question);
                

                
                var aMapper = new AnswerMapper();
                foreach (var answer in aMapper.UiToEntity(requestUiModel.Answers))
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

                var deleteId = requestUiModel.DeletedAnswersId;
                holder.AnswerRepository.RemoveBy(a => deleteId.Contains(a.Id));

                await holder.SaveChangesAsync();
            }
        }
        */
        public async Task<Result> DeleteQuestion(int id)
        {
            using (var holder = new RepositoryHolder())
            {
                var quest = await holder.QuestionRepository.GetByIdAsync(id);
                var number = quest.Number;
                var quests = await holder.QuestionRepository
                    .FetchByAsync(q => q.Number > number && (q.Parent_Question == null || q.Parent_Question==0));

                quests.ForEach(q=>q.Number--);
                holder.QuestionRepository.RemoveBy(q=>q.Parent_Question == id);
                holder.QuestionRepository.Update(quests);
                holder.QuestionRepository.Remove(quest);
                
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

        public async Task<DataResult<List<QuestionListUiModel>>> GetSurveyQuestionList(int surveyId)
        {
            using (var holder = new RepositoryHolder())
            {
                var questions = await holder.QuestionRepository
                    .FetchByAsync(q => q.SurveyId == surveyId && (q.Parent_Question==null||q.Parent_Question==0));
                var mapper = new QuestionListMapper();
                var data = questions.Select(q => mapper.EntityToUi(q)).ToList();
                return new DataResult<List<QuestionListUiModel>>(data);
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

        public async Task<DataResult<QuestionListUiModel>> RenameQuestion(RenameRequestUiModel requestRequestUi)
        {
            using (var holder = new RepositoryHolder ())
            {
                var repository = holder.QuestionRepository;
                var entiy = await repository.GetByIdAsync(requestRequestUi.Id);
                entiy.Title = requestRequestUi.Name;
                await holder.SaveChangesAsync();
                var mapper = new QuestionListMapper();
                var result = mapper.EntityToUi(entiy);
                return new DataResult<QuestionListUiModel>(result);
            }
        }

        public async Task<int> GetQuestionCount(int surveyId)
        {
            using (var holder = new RepositoryHolder())
            {
               var result = await holder.QuestionRepository
                    .FetchByAsync(q => q.SurveyId == surveyId && q.Parent_Question == null);
                return result.Count();
            }
        } 

        public async Task<Result> ChangeQuestionPostion(ChangePostitionRequestUiModel requestUiModel)
        {
            using (var holder = new RepositoryHolder())
            {
                var repository = holder.QuestionRepository;
                var entity = await repository.GetByIdAsync(requestUiModel.Id);
                var maxnumber = await GetQuestionCount(entity.SurveyId);
                int to = requestUiModel.Position;
                int from = entity.Number;
                int surveyId = entity.SurveyId;

                if (from == to)
                    return new Result();
                if (to <= 0 || to > maxnumber)
                    return new Result("Invalid Position");

                List<Questions> questions;

                if (from > to)
                {


                    questions = await repository
                        .FetchByAsync(q =>
                            (q.SurveyId == surveyId && q.Parent_Question == null) &&
                            (q.Number <= from && q.Number >= to));

                    var tQuestion = questions.First(q => q.Number == from);
                    questions.ForEach(q => q.Number++);
                    tQuestion.Number = to;
                }
                else
                {


                    questions = await repository
                        .FetchByAsync(q =>
                            (q.SurveyId == surveyId && q.Parent_Question == null) &&
                            (q.Number >= from && q.Number <= to));

                    var tQuestion = questions.First(q => q.Number == from);
                    questions.ForEach(q => q.Number--);
                    tQuestion.Number = to;


                }

                repository.Update(questions);
                await repository.SaveContextAsync();
                return new Result();

            }
        }
    }
}
