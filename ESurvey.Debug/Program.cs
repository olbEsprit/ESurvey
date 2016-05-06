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
            var log = new QuestionCrudLogic();
            try
            {
                var r = log.DeleteQuestion(4);
                r.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                
            }
        }
    }
}


