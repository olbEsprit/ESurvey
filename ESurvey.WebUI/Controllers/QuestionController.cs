﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ESurvey.BL.Concrete;
using ESurvey.UIModels;
using Microsoft.AspNet.Identity;

namespace ESurvey.WebUI.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        private QuestionCrudLogic _questionCrud;
        private SurveyAccessManager _accessManager;

        public SurveyAccessManager AccessManager
        {
            get { return _accessManager; }
        }

        public QuestionCrudLogic QuestionCrud
        {
            get { return _questionCrud; }
        }

        public QuestionController()
        {
            _questionCrud = new QuestionCrudLogic();
            _accessManager = new SurveyAccessManager();
        }

        [HttpGet]
        public async Task<JsonResult> QuestionList(int id)
        {
            var userId = User.Identity.GetUserId();
            if (await AccessManager.HasAccessToSurvey(userId, id))
            {
                var result = QuestionCrud.GetSurveyQuestions(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
            }
        }
    }
}