using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ESurvey.BL.Concrete;
using ESurvey.Common.Enums;
using Microsoft.AspNet.Identity;

namespace ESurvey.WebUI.Controllers
{
    public class EditorController : Controller
    {

        private SurveyAccessManager _accessManager;

        public SurveyAccessManager AccessManager
        {
            get { return _accessManager; }
        }

        public EditorController()
        {
            _accessManager = new SurveyAccessManager();
        }


        // GET: Editor/Edit/5
        [HttpGet]
        public async Task<ActionResult> EditSurvey(int? id)
        {
            if(id == null)
                return RedirectToAction("UserProfile", "Home");

            var userId = User.Identity.GetUserId();

            if(!await AccessManager.HasAccessToSurvey(userId, id.Value))
                return RedirectToAction("UserProfile", "Home");

            ViewBag.Id = id;
            return View();
        }


        // /EditorController/EditQuestion/
        [HttpGet]
        public async Task<ActionResult> EditQuestion(int id)
        {
           
            var userId = User.Identity.GetUserId();
            if (!await AccessManager.HasAccessToQuestion(userId, id))
                return RedirectToAction("UserProfile", "Home");


            var res = await new QuestionCrudLogic().GetQuestionType(id);

            if (res == (int)QuestionType.MultiSelect)
            {
                return PartialView("_MultiSelectEditor");
            }
            if (res == (int)QuestionType.Open)
            {
                return PartialView("_OpenEditor");
            }
            if (res ==(int)QuestionType.Matrix)
            {
                return PartialView("_MatrixEditor");
            }

            return Json(res);

        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("UserProfile", "Home");
        }


        [HttpGet]
        public ActionResult SurveyDetailsForm(int? id)
        {
            return PartialView();
        }



        [HttpGet]
        public ActionResult QuestionListTemplate(int? id)
        {
            return PartialView("_QuestionListTemplate");
        }

        [HttpGet]
        public ActionResult CreateQuestionTemplate()
        {
            return PartialView("_CreateQuestionTemplate");
        }

    }
}
