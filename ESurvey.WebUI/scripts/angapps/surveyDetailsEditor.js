

angular.module("editorApp").directive('surveyDetailsEditor', ['$http', function ($http) {
    return {
        restrict: 'E',
        replace: true,
        scope: {
            surveyDetails: "=",
            loading: "=",
            surveyId:"="
        },
        templateUrl: function (element, attrs) {
            return "/Editor/SurveyDetailsForm/";
        },
        link: function (scope, element, attrs) {

            scope.refreshSurveyDetails = function () {
                scope.surveyDetailsEdited = angular.copy(scope.surveyDetails);
            };

            scope.updateSurveyDetails = function () {
                scope.surveyDetails = angular.copy(scope.surveyDetailsEdited);
            };
            //BeginLoadSueveyDetails
            scope.loadSurveyDetails = function (surveyId) {
                $http({
                    method: 'GET',
                    url: '/Survey/Details/' + surveyId
                }).then(function successCallback(response) {
                    scope.loading = false;
                    var result = response.data;

                    if (result.HadError) {
                        alert("Error: " + result.ErrorMessage);
                        scope.refreshSurveyDetails();
                    } else {
                        alert(JSON.stringify(result));
                        scope.surveyDetails = result.Data;
                        
                        scope.refreshSurveyDetails();
                    }
                }, function errorCallback(response) {
                    scope.loading = false;
                    alert("Error");
                });
            };
            //EndLoadSurveyDetails

            //BeginSendSueveyDetails
            scope.sendSurveyDetails = function () {
                alert(JSON.stringify(scope.surveyDetailsEdited));
                alert(JSON.stringify(scope.surveyDetails));
                $http({
                    method: 'Post',
                    url: '/Survey/Update',
                    data: scope.surveyDetailsEdited
                    
                }).then(function successCallback(response) {
                    scope.loading = false;
                    var result = response.data;

                    if (result.HadError) {
                        alert("Error: " + result.ErrorMessage);
                    } else {
                        scope.updateSurveyDetails();
                    }
                }, function errorCallback(response) {
                    scope.loading = false;
                    alert("Error");
                });
            };
            //EndSendSurveyDetails

           

            alert(scope.surveyId);
            scope.loadSurveyDetails(scope.surveyId);
            

        }//endlink
        




        //,
        //controller: function ($scope) {
        //    console.info("enter directive controller");
        //    $scope.gallery = [];

        //    console.log($scope.src);

        //    $http({ method: 'GET', url: $scope.src }).then(function (result) {
        //        console.log(result);
        //    }, function (result) {
        //        alert("Error: No data returned");
        //    });
        //}
    }
}]);