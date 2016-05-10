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


    //    private SurveyAccessManager _accessManager;

        //public SurveyAccessManager AccessManager
        //{
        //    get { return _accessManager; }
        //}

        //public SurveyEditor(SurveyAccessManager manager)
        //{
        //    _accessManager = manager;
        //}


        

        //public async Task<SurveyUiModel> GetSurveyData(int surveyId)
        //{
        //    using (var holder = new RepositoryHolder())
        //    {
        //       // holder.
        //    }
        //}







        public async Task<QuestionUiModel> GetQuestionAgregate(int id)
        {
            using (var holder = new RepositoryHolder())
            {
                var questions = holder.QuestionRepository;
                var question = await questions.GetByIdAsync(id);
                var qMapper = new QuestionMapper();
                var uiQuestion = qMapper.EntityToUi(question);

                if (question.Is_matrix)
                {
                    var children = await questions.FetchByAsync(q => q.Parent_Question == question.Id);
                   
                }


                var answers = await holder.AnswerRepository.FetchByAsync(a => a.QuestionId == question.Id);
                var aMapper = new AnswerMapper();

                



                return uiQuestion;

            }
        }








        public async Task<List<AnswerUiModel>> GetQuestionAnswers(int questionId)
        {
            using (var holder = new RepositoryHolder())
            {
                var answers = await holder.AnswerRepository.FetchByAsync(a => a.QuestionId == questionId);
                var mapper = new AnswerMapper();
                return answers.Select(a => mapper.EntityToUi(a)).ToList();
            }
        }
        

        public async Task<List<QuestionUiModel>> GetSurveyMajorQuestions(int surveyId)
        {
            using (var holder = new RepositoryHolder())
            {
                var questions = await holder.QuestionRepository
                    .FetchByAsync(
                    q=> q.SurveyId == surveyId &&
                    q.Parent_Question==null);

                var mapper = new QuestionMapper();
                return questions.Select(s => mapper.EntityToUi(s)).ToList();
            }
        } 

        

    }

}
