var editorApp = angular.module("editorApp", []);



editorApp.controller('mainController',['$scope','$http','$templateCache', function($scope,$htpp,$templateCahe) {
    $scope.loading = true;
    $scope.surveyId = 0;
    $scope.hello = "Main";
    $scope.init = function (value) {
        $scope.surveyId = value;
    };
    }
]);

























//editorApp.factory('surveyDetailsService', [
//    '$http', function($http) {
//        var endpoint = '/Survey/';


//        this.get = function(surveyId, success, fail) {
//            $http({
//                method: 'GET',
//                url: endpoint + "Details/" + surveyId
//            }).then(function successCallback(response) {
//                var result = response.data;
//                alert(JSON.stringify(result));
//                if (success != null) {
//                    success();
//                }
//                alert("de"+JSON.stringify(result));
//                return result;
//            }, function errorCallback(response) {
//                alert("Error");
//                if (fail != null) {
//                    fail();
//                }
//                return null;
//            });
//        };


//        this.post = function (data, success, fail) {
//            $http({
//                method: 'Post',
//                url: endpoint + "Update/",
//                data: $scope.surveyDetailsEdited

//            }).then(function successCallback(response) {
//                var result = response.data;
//                alert("rets2ss" + JSON.stringify(result));
//                if (success != null) {
//                    success();
//                }
//                alert("retss" + JSON.stringify(result));
//                return result;
//            }, function errorCallback(response) {
//                if (fail != null) {
//                    fail();
//                }
//                alert("Error SurveyPost");
//            });
//        };
//    }
//]);

