﻿using System;
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
            var logic = new QuestionCrudLogic();
            var R = logic.MoveQuestion(1006, 2, 6);
            R.Wait();
        }
    }
}


