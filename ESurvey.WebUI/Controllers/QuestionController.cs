using System;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<JsonResult> QuestionList(int id)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToSurvey(userId, id))
            {
                var result = await QuestionCrud.GetSurveyQuestionList(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                if (await SurveyAccess.HasAccessToQuestion(userId, id))
                {
                    var result = await QuestionCrud.DeleteQuestion(id);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                var y = e.Message;
                return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
            }
            
            return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Rename(RenameRequestUiModel request)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToQuestion(userId, request.Id))
            {
                var result = await QuestionCrud.RenameQuestion(request);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SetPosition(ChangePostitionRequestUiModel requestUiModel)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToQuestion(userId, requestUiModel.Id))
            {
                var result = await QuestionCrud.ChangeQuestionPostion(requestUiModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> Create(AddQuestionUiModel question)
        {
            var userId = User.Identity.GetUserId();
                if (await SurveyAccess.HasAccessToSurvey(userId, question.SurveyId))
                {
                    var result = await QuestionCrud.CreateQuestion(question);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> Details(int id)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToQuestion(userId, id))
            {
                var res = await QuestionCrud.GetQuestion(id);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            return Json(new Result("No Access"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Update(QuestionUiModel model)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToQuestion(userId, model.Id))
            {
                var res = await QuestionCrud.UpdateQuestion(model);
                return Json(res);
            }
            return Json(new Result("No Access"));
        }


       

        [HttpPost]
        public async Task<JsonResult> ToggleOtherAnswerOption(ToggleOtherAnswerRequest model)
        {
            var userId = User.Identity.GetUserId();
            if (await SurveyAccess.HasAccessToQuestion(userId, model.QuestionId))
            {
                var res = await QuestionCrud.ToggleOtherAnswerOption(model);
                return Json(res);
            }
            return Json(new Result("No Access"));
        }

    }
}