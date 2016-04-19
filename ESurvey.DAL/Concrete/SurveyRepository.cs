using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.DAL.Abstract;
using ESurvey.Entity;

namespace ESurvey.DAL.Concrete
{
    public class SurveyRepository : GenericBaseRepository<Survey>, ISurveyRepository
    {
        public SurveyRepository(ESurveyEntities context): base(context)
        {
                
        }
    }
}
