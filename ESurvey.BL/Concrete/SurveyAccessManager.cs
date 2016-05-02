using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ESurvey.DAL.Concrate;
using ESurvey.Entity;
using ESurvey.UIModels;

namespace ESurvey.BL.Concrete
{
    
    public class SurveyAccessManager
    {
        private const string Alg = "HmacSHA256";
        private const string Salt = "rz8LuOtFBXphj9WQfvFh";

        
        public async Task<bool> HasAccessToSurvey(string userId, int surveyId)
        {
            using (var holder = new RepositoryHolder())
            {
                var survey = await holder.SurveyRepository.GetByIdAsync(surveyId);

                if (survey == null)
                    return false;

                return survey.OwnerId == userId;
            }
        }


        public async Task<bool> HasAccessToQuestion(string userId, int questionId)
        {
            using (var holder = new RepositoryHolder())
            {
                var question = await holder.QuestionRepository.GetByIdAsync(questionId);
                if (question == null)
                    return false;

                var survey = await holder.SurveyRepository.GetByIdAsync(question.SurveyId);
                return survey.OwnerId == userId;
            }
        }


        public async Task<bool> HasAccessToAnswer(string userId, int answerId)
        {
            using (var holder = new RepositoryHolder())
            {
                var answer = await holder.AnswerRepository.GetByIdAsync(answerId);
                if (answer == null)
                    return false;

                var question = await holder.QuestionRepository.GetByIdAsync(answer.QuestionId);
                

                var survey = await holder.SurveyRepository.GetByIdAsync(question.SurveyId);
                return survey.OwnerId == userId;
            }
        } 


        public async Task<string> GetUserIdFromToken(string token)
        {

            using (var holder = new RepositoryHolder())
            {
                var tokens = await holder.TokenRepository.FetchByAsync(t=>t.Value==token);
                var tok = tokens.FirstOrDefault();
                if (tok == null || tok.Expires<DateTime.Now)
                {
                    return null;
                }
                else
                {
                    return tok.UserId;
                }
            }
        }

        public async Task RegisterToken(string userId, string token, DateTime expires)
        {
            var Token = new UserToken()
            {
                UserId = userId,
                Value = token,
                Expires = expires
            };

            using (var holder = new RepositoryHolder())
            {
                holder.TokenRepository.Insert(Token);
                await holder.SaveChangesAsync();
            }


        } 

        public static string GenerateToken(string username, string password, string ip, string userAgent, long ticks)
        {
            string hash = string.Join(":", new string[] { username, ip, userAgent, ticks.ToString() });
            string hashLeft = "";
            string hashRight = "";

            using (HMAC hmac = HMACSHA256.Create(Alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(password);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
                hashLeft = Convert.ToBase64String(hmac.Hash);
                hashRight = string.Join(":", new string[] { username, ticks.ToString() });
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hashRight));
                hashRight = Convert.ToBase64String(hmac.Hash);
            }

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
        }

    }
    
}
