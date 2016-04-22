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
        private const string _alg = "HmacSHA256";
        private const string _salt = "rz8LuOtFBXphj9WQfvFh";

        public async Task<IEnumerable<Survey>> GetUserSurveys(string ownerId)
        {
            using (var holder = new RepositoryHolder())
            {
                return await holder.SurveyRepository.FetchByAsync(s => s.OwnerId == ownerId);
            }
        }


        public async Task<bool> HasAccess(string userId, int surveyId)
        {
            using (var holder = new RepositoryHolder())
            {
                var survey = await holder.SurveyRepository.GetByIdAsync(surveyId);
                return survey.OwnerId == userId;
            }
        }


        


        public static string GenerateToken(string username, string password, string ip, string userAgent, long ticks)
        {
            string hash = string.Join(":", new string[] { username, ip, userAgent, ticks.ToString() });
            string hashLeft = "";
            string hashRight = "";

            using (HMAC hmac = HMACSHA256.Create(_alg))
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
