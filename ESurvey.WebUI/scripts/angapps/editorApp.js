var editorApp = angular.module("editorApp", []);

editorApp.controller('editorController', ['$scope', '$http', '$templateCache', function ($scope, $http, $templateCache) {

        
    $scope.loading = true;
    $scope.surveyId = 0;
               
        $scope.hello = "Test";
        

    }]);


