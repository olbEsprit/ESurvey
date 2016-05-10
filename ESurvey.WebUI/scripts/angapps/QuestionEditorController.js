angular.module("editorApp").controller('questionEditorController', [
    '$scope', 'qListService', 'qEditorService', '$routeParams',
    function ($scope, qListService, qEditorService, $routeParams) {


        var Types = [
            "None",
            "Muliselect",
            "Open",
            "Matrix"
        ];

        $scope.QuestionId = $routeParams.QuestionId;

        $scope.QuestionDetails = {};

        $scope.EditedDetails = {};

        $scope.TName = "Noname";

        $scope.init = function() {
            qEditorService.SetQuestionId($routeParams.QuestionId);
            $scope.LoadDetails();
            $scope.TName = Types[$scope.QuestionDetails.Type];
        };


        $scope.LoadDetails = function() {
            qEditorService.LoadQuestionDetails().then(
                function(result) {
                    if (result.HadError) {
                        alert(result.ErrorMessage);
                    } else {
                        $scope.QuestionDetails = result.Data;            
                        cancelEditedDetails();
                    }
                }
            );
        };


        $scope.SendDetails = function () {
            qEditorService.UpdateQuestion($scope.EditedDetails).then(
                function (result) {
                    if (result.HadError) {
                        alert(result.ErrorMessage);
                    } else {
                        saveEditedDetails();
                    }
                }
            );
        };

        $scope.toggleOtherAnswer = function () {
            
            var val = $scope.EditedDetails.OtherAnswer;
        
            qEditorService.toggleOtherAnswerOption(val).then(
                function(data) {
                    if (data.HadError) {
                        alert(data.ErrorMessage);
                        refreshSurveyDetails();
                    } else {
                        saveEditedDetails();
                    }
                }
            );

        };


        function saveEditedDetails () {
            $scope.QuestionDetails = angular.copy($scope.EditedDetails);
            var model = {
                Id: $scope.EditedDetails.Id,
                Title: $scope.EditedDetails.Title,
                Number: $scope.EditedDetails.Number
            };
            qListService.UpdateQuestionInList(model);
        };

        function cancelEditedDetails() {
            $scope.EditedDetails = angular.copy($scope.QuestionDetails);
        };

        

        $scope.hello = "Editor";

        $scope.init();

    }]);
