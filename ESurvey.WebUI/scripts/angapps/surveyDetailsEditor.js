angular.module("editorApp").controller('editorController', ['$scope', '$http', '$templateCache', function ($scope, $http, $templateCache) {

    $scope.hello = "Editor";
    $scope.surveyDetailsEdited = null;
    $scope.surveyDetais = null;
    $scope.init = function () {
        $scope.loadSurveyDetails($scope.surveyId);
    };



    $scope.refreshSurveyDetails = function () {
        
        $scope.surveyDetailsEdited = angular.copy($scope.surveyDetails);
    };
    $scope.updateSurveyDetails = function () {
        $scope.surveyDetails = angular.copy($scope.surveyDetailsEdited);
    };
    //BeginLoadSueveyDetails
    $scope.loadSurveyDetails = function (surveyId) {
        $http({
            method: 'GET',
            url: '/Survey/Details/' + surveyId
        }).then(function successCallback(response) {
            $scope.loading = false;
            var result = response.data;

            if (result.HadError) {
                alert("Error: " + result.ErrorMessage);
                $scope.refreshSurveyDetails();
            } else {
                alert(JSON.stringify(result));
                $scope.surveyDetails = result.Data;

                $scope.refreshSurveyDetails();
            }
        }, function errorCallback(response) {
            $.scope.loading = false;
            alert("Error");
        });
    };
//    EndLoadSurveyDetails
  //  BeginSendSueveyDetails
    $scope.sendSurveyDetails = function () {
      //  alert(JSON.stringify($scope.surveyDetailsEdited));
        //alert(JSON.stringify($scope.surveyDetails));
        $http({
            method: 'POST',
            url: '/Survey/Update',
            data: $scope.surveyDetailsEdited

        }).then(function successCallback(response) {
            $scope.loading = false;
            var result = response.data;

            if (result.HadError) {
                alert("Error: " + result.ErrorMessage);
            } else {
                $scope.updateSurveyDetails();
            }
        }, function errorCallback(response) {
            $scope.loading = false;
            alert("Error");
        });
    };
    //EndSendSurveyDetails
}]);

angular.module("editorApp").directive('surveyDetailsEditor', ['$http', function ($http) {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: function(element, attrs) {
            return "/Editor/SurveyDetailsForm/";
        }
    }
}]);