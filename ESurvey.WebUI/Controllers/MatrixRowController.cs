using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ESurvey.BL.Concrete;
using ESurvey.UIModels;
using ESurvey.UIModels.SurveyEditor;
using Microsoft.AspNet.Identity;

namespace ESurvey.WebUI.Controllers
{
    public class MatrixRowController:Controller
    {
        private MatrixRowCrudLogic _rowCrud;
        private SurveyAccessManager _surveyAccessManager;


        public SurveyAccessManager SurveyAccess
        {
            get { return _surveyAccessManager; }
        }

        public MatrixRowCrudLogic RowCrud
        {
            get { return _rowCrud; }
        }

        public MatrixRowController()
        {
            _rowCrud = new MatrixRowCrudLogic();
            _surveyAccessManager = new SurveyAccessManager();
        }

        [HttpGet]
        public async Task<JsonResult> All(int id)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToQuestion(userId, id))
            {
                var result = await RowCrud.FetchQuestionRows(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> Create(int id)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToQuestion(userId, id))
            {
                var result = await RowCrud.Create(id);
                return Json(result);
            }
            return Json(new Result("No Access"));
        }



        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToQuestion(userId, id))
            {
                var result = await RowCrud.Delete(id);
                return Json(result);
            }
            return Json(new Result("No Access"));
        }


        [HttpPost]
        public async Task<JsonResult> Rename(RenameRequestUiModel model)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToQuestion(userId, model.Id))
            {
                var result = await RowCrud.Rename(model);
                return Json(result);
            }
            return Json(new Result("No Access"));
        }








    }
}