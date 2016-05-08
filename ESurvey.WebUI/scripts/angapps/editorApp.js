var editorApp = angular.module("editorApp", ["ngRoute", "xeditable"])
    .config(function($routeProvider) {
        $routeProvider.when('/EditQuestion/:questionId',
        {
            template: '<H4>Imma superstar</H4>',
            controller: 'testCtrl'
        });
        $routeProvider.when('/answer',
        {
            templateUrl: '/Home/Test',
            controller: 'testCtrl'
        });
    });




editorApp.service('qListService', function () {

    var questions = [];
    var surveyId;

    var onModelChangeSubscribers = [];

    this.setSurveyId = function (id) {
        surveyId = id;
    };

    this.getSurveyId = function () {
        return surveyId;
    };
    
    this.getQList = function () {
        return questions;
    };

    this.onModelChange = function() {
        onModelChangeSubscribers.forEach(function(item, i, arr) {
            item();
        });
    };

    this.subscribeOnModelChange = function(callback) {
        onModelChangeSubscribers.push(callback);
    };


    this.setQuestionList = function(data) {
        questions = data;
        onModelChange();
    };


    this.reloadQuestionList = function() {
        var res = loadQuestionList(surveyId);
        if (res.HadError) {
            alert(res.ErrorMessage);
        } else {
            questions = res.Data;
            onModelChange();
        }
    };


    

    function loadQuestionList(surveyId) {
        $http({
            method: 'GET',
            url: '/Question/QuestionList/' + surveyId
        }).then(function successCallback(response) {
            var result = response.data;
            return result;
        }, function errorCallback(response) {
            alert("Error Load Question List");
        });
    };



});




editorApp.run(function (editableOptions) {
    editableOptions.theme = 'bs3'; // bootstrap3 theme. Can be also 'bs2', 'default'
});

editorApp.controller('testCtrl', ['$scope', '$http', '$templateCache', '$location', function ($scope, $htpp, $templateCache, $location) {
    $scope.hello = "TestCtrl";
    $scope.$on('$routeChangeStart', function (event, next, current) {
        if (typeof (current) !== 'undefined') {
            $templateCache.remove(next.templateUrl);
        }
    });

    }
]);

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

