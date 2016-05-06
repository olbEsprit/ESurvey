angular.module("editorApp").controller('questionListController', [
    '$scope', '$http', '$templateCache', function ($scope, $http, $templateCache) {

        $scope.questionList = {};

        $scope.loadQuestionList = function() {            
            $http({
                method: 'GET',
                url: '/Survey/Details/' + surveyId
            }).then(function successCallback(response) {
                $scope.loading = false;
                var result = response.Data;

                if (result.HadError) {
                    alert("Error: " + result.ErrorMessage);
                    $scope.refreshSurveyDetails();
                } else {
                    
                    $scope.surveyDetails = result.Data;

                    $scope.refreshSurveyDetails();
                }
            }, function errorCallback(response) {
                $.scope.loading = false;
                alert("Error Load Question List");
            });
        }


    }
]);