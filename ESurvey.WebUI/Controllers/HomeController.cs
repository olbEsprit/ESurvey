using System;
using System.Web.Mvc;
using ESurvey.WebUI.Models;
using Microsoft.AspNet.Identity;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {

        

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Test()
        {
            ViewBag.Test = DateTime.Now.ToString();
            return PartialView();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult UserProfile()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateSurveyForm()
        {
            return PartialView("_CreateSurveyForm",new CreateSurveyFormModel());
        }

        
    }
}
