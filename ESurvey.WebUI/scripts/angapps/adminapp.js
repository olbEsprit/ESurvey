var adminapp = angular.module("adminApp", []);

adminapp.controller('profileContorller', ['$scope','$http','$templateCache', function ($scope,$http,$templateCache) {
    $scope.surveys = surveyst;
    $scope.hello = "Test";
    $scope.loading = false;
    $scope.refresh = function() {
        
        $templateCache.removeAll();
        
    };
    

    $scope.redirectToEdit = function(surveyId) {
        window.location.href = "/Editor/" + surveyId;
    };

    $scope.redirectToPublish = function (surveyId) {
        window.location.href = "/Publish/" + surveyId;
    };

    $scope.fetchSurveys = function() {

        $scope.loading = true;
        $http({
            method: 'GET',
            url: '/Survey/UserSurveys/'
        }).then(function successCallback(response) {
    
            var result = response.data;
            if (result.HadError) {
                $scope.loading = false;
                
                
            } else {
                $scope.loading = false;
                $scope.surveys = result.Data;
            }
        }, function errorCallback(response) {
            $scope.loading = false;
            alert("Error");
        });
    };

    $scope.fetchSurveys();

    $scope.deleteSurvey = function (survey) {
        if (confirm("You really want delete survey "+survey.Title+"?")) {
            $scope.loading = true;
            var surveyId = survey.Id;
            $http({
                method: 'POST',
                url: '/Survey/Delete/' + surveyId
            }).then(function successCallback(response) {
                var result = angular.fromJson(response);
                if (result.HadError) {
                    $scope.loading = false;
                    alert(result.ErrorMessage);
                } else {
                    $scope.removeSurveyFromListById(surveyId);
                    $scope.loading = false;
                }
            }, function errorCallback(response) {
                alert(["Error"]);
            });
        }
    };



    $scope.addSurveyToList = function (addedSurvey) {
        $scope.surveys.push(addedSurvey);
    };

    $scope.removeSurveyFromListById = function (surveyId) {
        $scope.surveys = $scope.surveys.filter(function(item) {
            return item.Id !== surveyId;
        });
    };
}]);



    var surveyst = [
        {
            Id: 1,
            Title: "SomeTitle"
        },
        {
            Id: 2,
            Title: "AnotherTitle"
        },
        {
            Id: 3,
            Title: "ThirdTitle"
        }
        
    ];