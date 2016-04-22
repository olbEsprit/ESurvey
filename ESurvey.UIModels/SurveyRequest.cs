using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.UIModels.SurveyEditor;

namespace ESurvey.UIModels
{
    public class SurveyRequest:Request
    {
       public SurveyUIModel Survey { get; set; }
    }
}
