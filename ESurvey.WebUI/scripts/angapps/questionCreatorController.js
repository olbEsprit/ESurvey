angular.module("editorApp").controller('questionCreatorController', [
    '$scope', '$http', function ($scope, $http) {

        $scope.questionTypes = [
            {
                Id : 1,
                Title : "MultiChoise"
            },
            {
                Id: 2,
                Title: "Open"
            },
            {
                Id: 3,
                Title: "Matrix"
            }
        ];
        $scope.createQuestion = function(typeId) {
            var model = {
                SurveyId: $scope.surveyId,
                QuestionTypeId: typeId
            };
            sendQuestion(model);
        };

        

        function sendQuestion(addQuestionUiModel) {
            $http({
                method: 'POST',
                url: '/Question/Create',
                data: addQuestionUiModel
        }).then(function successCallback(response) {
                    var result = response.data;
                    if (result.HadError) {
                        alert("Error: " + result.ErrorMessage);
                    } else {
                        $scope.reloadQuestionList();
                    }
                }, function errorCallback(response) {
                    alert("Error Create Question");
                });
            };
    }
]);


angular.module("editorApp").directive('createQuestionDirective', [function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: function (element, attrs) {
            return "/Editor/CreateQuestionTemplate/";
        }
    }
}]);
