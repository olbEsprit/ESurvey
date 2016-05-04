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
            

        }

        [HttpPost]
        public async Task<Result> Update(SurveyUiModel model)
        {
            string userId = User.Identity.GetUserId();
            if (!await AccessManager.HasAccessToSurvey(userId, model.Id))
            {
                return new Result("You have no access");
            }
            else
            {
                return await CrudLogic.UpdateSurvey(model);
            }

        }

        [HttpPost]
        public async Task<Result> Delete(int id)
        {
            string userId = User.Identity.GetUserId();
            if (!await AccessManager.HasAccessToSurvey(userId, id))
            {
                return new Result("You have no access");
            }
            else
            {
                return await CrudLogic.DeleteSurvey(id);
            }
        }


        [Authorize]
        [HttpGet]
        public async Task<JsonResult> Details(int id)
        {

            string userId = User.Identity.GetUserId();
/*            
            if (!await AccessManager.HasAccessToSurvey(userId, id))
            {
                return new DataResult<SurveyUiModel>(new SurveyUiModel())
                {
                    HadError = true,
                    ErrorMessage = "HasNoAccess"
                };
            }
            
            else
            {
                return await CrudLogic.GetSurvey(id);
            }*/

            var data = new DataResult<SurveyUiModel>(
                new SurveyUiModel()
                {
                    Id = 5,
                    OwnerId = "teteres",
                    Title = "MYSYT"
                });
            var result = JsonConvert.SerializeObject(data);
            
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public void GetUserSurveys()
        {

        }

        
    }
}
