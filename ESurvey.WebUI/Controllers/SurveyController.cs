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
using Newtonsoft.Json;

namespace ESurvey.WebUI.Controllers
{

    public class SurveyController : Controller
    {
        // GET: Survey
        private SurveyCrudLogic _surveyCrud;
        private SurveyAccessManager _accessManager;

        public SurveyAccessManager AccessManager
        {
            get { return _accessManager; }
        }

        public SurveyCrudLogic CrudLogic
        {
            get { return _surveyCrud; }
        }



        public SurveyController()
        {
            _surveyCrud = new SurveyCrudLogic();
            _accessManager = new SurveyAccessManager();
        }

        [HttpPost]
        public async Task<JsonResult> Create(SurveyListUi model)
        {
            var ownerId = User.Identity.GetUserId();
            var result = await CrudLogic.CreateSurvey(model, ownerId);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Update(SurveyUiModel model)
        {
            string userId = User.Identity.GetUserId();
            if (!await AccessManager.HasAccessToSurvey(userId, model.Id))
            {
                return Json(new Result("You have no access"));
            }
            else
            {
                var result = await CrudLogic.UpdateSurvey(model);
                return Json(result);
            }

        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            //return Json(new Result());
            if (!await AccessManager.HasAccessToSurvey(userId, id))
            {
                return Json(new Result("You have no access"));
            }
            else
            {
                return Json(await CrudLogic.DeleteSurvey(id));
            }
        }


        [Authorize]
        [HttpGet]
        public async Task<JsonResult> Details(int id)
        {

            string userId = User.Identity.GetUserId();
                       
            if (!await AccessManager.HasAccessToSurvey(userId, id))
            {
                var res =  new DataResult<SurveyUiModel>(new SurveyUiModel())
                {
                    HadError = true,
                    ErrorMessage = "HasNoAccess"
                };
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            
            else
            {
                return Json(await CrudLogic.GetSurvey(id), JsonRequestBehavior.AllowGet);
            }
            /*
            var data = new DataResult<SurveyUiModel>(
                new SurveyUiModel()
                {
                    Id = 5,
                    OwnerId = "teteres",
                    Title = "MYSYT"
                });
            var result = JsonConvert.SerializeObject(data);
            */
           // return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        [Authorize]
        public async Task<JsonResult> UserSurveys()
        {
            var id = User.Identity.GetUserId();
            var result = await CrudLogic.GetUserSurveys(id);
            /*
            var surveys = new List<SurveyListUi>()
            {
                new SurveyListUi()
                {
                    Id = 4,
                    Title = "Hello"
                },
                new SurveyListUi()
                {
                    Id = 5,
                    Title = "SURVE"
                }
            };
            var result = new DataResult<List<SurveyListUi>>(surveys);*/
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        

    }
}
