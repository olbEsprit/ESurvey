using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.DAL.Abstract;
using ESurvey.Entity;

namespace ESurvey.DAL.Concrete
{
    public class AnswerRepository:GenericBaseRepository<QuestionAnswers>,IAnswerRepository
    {
        public AnswerRepository(ESurveyEntities context):base(context)
        {
            
        }
    }
}
