using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.DAL.Abstract;
using ESurvey.Entity;

namespace ESurvey.DAL.Concrete
{
    public class UserRepository:GenericBaseRepository<AspNetUsers>, IUserRepository
    {
        public UserRepository(ESurveyEntities context):base(context)
        {
            
        }
    }
}
