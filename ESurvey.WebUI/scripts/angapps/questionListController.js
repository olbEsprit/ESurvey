angular.module("editorApp").controller('questionListController', [
    '$scope', '$http', '$templateCache', function ($scope, $http, $templateCache) {

        $scope.questionList = {};

        $scope.init = function (){
            loadQuestionList($scope.surveyID);
        };

        $scope.loadQuestionList = function(surveyId) {            
            $http({
                method: 'GET',
                url: '/Question/QuestionList/' + surveyId
            }).then(function successCallback(response) {
                var result = response.data;
                if (result.HadError) {
                    alert("Error: " + result.ErrorMessage);
                } else {
                    $scope.questionList = result.Data;
                }
            }, function errorCallback(response) {
                alert("Error Load Question List");
            });
        }
    }
]);


angular.module("editorApp").controller('questionListController', [
    '$scope', '$http', function ($scope, $http) {

        

        $scope.init = function () {
            loadQuestionList($scope.surveyId);
            //alert("Loda");
        };

        function loadQuestionList(surveyId) {
            $http({
                method: 'GET',
                url: '/Question/QuestionList/' + surveyId
            }).then(function successCallback(response) {
                var result = response.data;
                if (result.HadError) {
                    alert("Error: " + result.ErrorMessage);
                } else {
                    $scope.questionList = result.Data;
                }
            }, function errorCallback(response) {
                alert("Error Load Question List");
            });
        }
    }
]);

angular.module("editorApp").directive('questionListDir', [ function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: function (element, attrs) {
            return "/Editor/QuestionListTemplate/";
        }
    }
}]);