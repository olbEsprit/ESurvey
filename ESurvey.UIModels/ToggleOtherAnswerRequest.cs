using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESurvey.UIModels
{
    public class ToggleOtherAnswerRequest
    {
        public int QuestionId { get; set; }
        public bool Toggle { get; set; }
    }
}
