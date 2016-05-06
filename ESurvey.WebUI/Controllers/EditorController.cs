using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESurvey.WebUI.Controllers
{
    public class EditorController : Controller
    {



        // GET: Editor/Edit/5
        [HttpGet]
        public ActionResult EditSurvey(int id)
        {
            ViewBag.Id = id;
            return View();
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

    }
}
