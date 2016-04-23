using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.DAL.Abstract;

namespace ESurvey.DAL.Abstract
{
    public interface IRepositoryHolder : IDisposable
    {

        IAnsweredQuestionsOptionsRepository AnsweredQuestionsOptionsRepository { get; }
        IAnsweredQuestionsRepository AnsweredQuestionsRepository { get; }
        IAnswerRepository AnswerRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        ISurveyRepository SurveyRepository { get; }
        ISurveySessionRepository SurveySessionRepository {get;}
        IUserRepository UserRepository { get; }
        IVoterRepository VoterRepository { get; }
        ITokenRepository TokenRepository { get; }
        
        Task SaveChangesAsync();
        void SaveChanges();

    }
}
