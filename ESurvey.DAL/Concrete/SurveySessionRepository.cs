using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.DAL.Abstract;
using ESurvey.Entity;

namespace ESurvey.DAL.Concrete
{
    public class SurveySessionRepository : GenericBaseRepository<SurveySessions>, ISurveySessionRepository
    {
        public SurveySessionRepository(ESurveyEntities context) : base(context)
        {
            
        }
    }
}
