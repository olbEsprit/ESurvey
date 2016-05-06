using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.DAL.Abstract;
using ESurvey.Entity;

namespace ESurvey.DAL.Concrete
{
    public class QuestionRepository : GenericBaseRepository<Questions>, IQuestionRepository
    {
        public QuestionRepository(ESurveyEntities context):base(context)
        {
            
        }

        public override void Update(Questions item)
        {
            base.Update(item);
            Context.Entry(item).Property(i => i.Is_matrix).IsModified = false;
            Context.Entry(item).Property(i => i.Parent_Question).IsModified = false;
            Context.Entry(item).Property(i => i.SurveyId).IsModified = false;
            Context.Entry(item).Property(i => i.QuestionType).IsModified = false;
        }
    }
}
