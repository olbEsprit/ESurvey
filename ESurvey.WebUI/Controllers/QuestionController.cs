using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ESurvey.BL.Concrete;
using ESurvey.UIModels;
using ESurvey.UIModels.SurveyEditor;
using Microsoft.AspNet.Identity;

namespace ESurvey.WebUI.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        private QuestionCrudLogic _questionCrud;
        private SurveyAccessManager _surveyAccessManager;
        

        public SurveyAccessManager SurveyAccess
        {
            get { return _surveyAccessManager; }
        }

        public QuestionCrudLogic QuestionCrud
        {
            get { return _questionCrud; }
        }

        public QuestionController()
        {
            _questionCrud = new QuestionCrudLogic();
            _surveyAccessManager = new SurveyAccessManager();
        }

        [System.Web.Mvc.HttpGet]
        public async Task<JsonResult> QuestionList(int id)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToSurvey(userId, id))
            {
                var result = await QuestionCrud.GetSurveyQuestionList(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToQuestion(userId, id))
            {
                var result = await QuestionCrud.DeleteQuestion(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
            }
        }



        [System.Web.Mvc.HttpPost]
        public async Task<JsonResult> Create(AddQuestionUiModel question)
        {
            var userId = User.Identity.GetUserId();
                if (await SurveyAccess.HasAccessToSurvey(userId, question.SurveyId))
                {
                    var result = await QuestionCrud.CreateQuestion(question);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
                }
        }

    }
}