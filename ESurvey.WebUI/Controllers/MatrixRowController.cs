using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESurvey.BL.Concrete;

namespace ESurvey.WebUI.Controllers
{
    public class MatrixRowController
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











    }
}