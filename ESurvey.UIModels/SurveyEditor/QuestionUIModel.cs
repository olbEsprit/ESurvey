using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESurvey.UIModels.SurveyEditor
{
    public class QuestionUiModel
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public bool IsMatrix { get; set; } = false;
        public bool IsHidden { get; set; } = false;
        public string Title { get; set; }
        public int? ParentQuestionId { get; set; } = null;

        public List<QuestionUiModel> Children { get; set; }

    }
}
