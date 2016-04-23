using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.DAL.Abstract;
using ESurvey.Entity;

namespace ESurvey.DAL.Concrete
{
    public class TokenRepository:GenericBaseRepository<UserToken>,ITokenRepository
    {
        public TokenRepository(ESurveyEntities context):base(context)
        {
            
        }
    }
}
