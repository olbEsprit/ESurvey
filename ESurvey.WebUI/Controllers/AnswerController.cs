using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ESurvey.BL.Concrete;
using ESurvey.UIModels;
using ESurvey.UIModels.SurveyEditor;
using Microsoft.AspNet.Identity;

namespace ESurvey.WebUI.Controllers
{
    public class AnswerController : Controller
    {
        private AnswerCrudLogic _answerCrud;
        private SurveyAccessManager _surveyAccessManager;


        public SurveyAccessManager SurveyAccess
        {
            get { return _surveyAccessManager; }
        }

        public AnswerCrudLogic AnswerCrud
        {
            get { return _answerCrud; }
        }

        public AnswerController()
        {
            _answerCrud = new AnswerCrudLogic();
            _surveyAccessManager = new SurveyAccessManager();
        }

        // GET: Answer/Details/5
        [HttpGet]
        public async Task<JsonResult> All(int id)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToQuestion(userId, id))
            {
                var result = await AnswerCrud.GetQuestionAnswers(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
        }

       

        // POST: Answer/Create
        [HttpPost]
        public async Task<JsonResult> Create(int id)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToQuestion(userId, id))
            {
                var result = await AnswerCrud.CreateAnswer(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> Rename(RenameRequestUiModel model)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToAnswer(userId, model.Id))
            {
                var result = await AnswerCrud.Rename(model);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
        }

        // POST: Answer/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToAnswer(userId, id))
            {
                var result = await AnswerCrud.DeleteAnswer(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
        }
    }
}
