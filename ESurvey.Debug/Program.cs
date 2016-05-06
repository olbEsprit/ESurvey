using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESurvey.BL.Concrete;
using ESurvey.DAL.Concrate;
using ESurvey.Entity;
using ESurvey.UIModels;
using ESurvey.WebUI.Controllers;

namespace ESurvey.Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            var ligic  = new QuestionCrudLogic();
            var T = ligic.GetSurveyQuestionList(1006);
            T.Wait();
            var res = T.Result;
            Console.ReadKey();
        }
    }
}


