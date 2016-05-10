//angular.module("editorApp").controller('questionListController', [
//    '$scope', '$http', '$templateCache', function ($scope, $http, $templateCache) {

//        $scope.questionList = {};

//        $scope.init = function (){
//            loadQuestionList($scope.surveyID);
//        };


//        $scope.loadQuestionList = function(surveyId) {            
//            $http({
//                method: 'GET',
//                url: '/Question/QuestionList/' + surveyId
//            }).then(function successCallback(response) {
//                var result = response.data;
//                if (result.HadError) {
//                    alert("Error: " + result.ErrorMessage);
//                } else {
//                    $scope.questionList = result.Data;
//                }
//            }, function errorCallback(response) {
//                alert("Error Load Question List");
//            });
//        }
//    }
//]);


angular.module("editorApp").controller('questionListController', [
    '$scope', 'qListService', function ($scope, qListService) {


        $scope.questionList = [];

        $scope.init = function () {
            qListService.setSurveyId($scope.surveyId);
            qListService.subscribeOnModelChange(onModelChange);
            qListService.reloadQuestionList();
            
            
        };

        function onModelChange() {
            $scope.questionList = qListService.getQList();
            CalculateNumbers();
        }

        $scope.pendingDelete = function (id) {
            if (confirm("You Really Wanna Delete?")) {
                sendDeleteRequest(id);
            }
        };

        function sendDeleteRequest(id) {
            qListService.deleteQuestionById(id);
        }



        $scope.renameQuestion = function (q,$data) {
            qListService.renameQuestion(q.Id, $data);
            return false;
        };

        $scope.setQuestionNumber = function(q) {
            qListService.setQuestionNumber(q.Id, q.Number);
            return false;
        };

        $scope.numbers = [];

        function CalculateNumbers (){
            var res = [];
            for (var i = 1; i <= $scope.questionList.length; i++) {
                res.push({num:i, value:i});
            }
            $scope.numbers = res;
        };

        function getById(id) {
            $scope.questionList.forEach(function(item, index) {
                 if (item.Id == id) {
                     return item;
                 } else {
                     return null;
                 }
                });
        }

        
        //function loadQuestionList(surveyId) {
        //    $http({
        //        method: 'GET',
        //        url: '/Question/QuestionList/' + surveyId
        //    }).then(function successCallback(response) {
        //        var result = response.data;
        //        if (result.HadError) {
        //            alert("Error: " + result.ErrorMessage);
        //        } else {
        //            $scope.questionList = result.Data;
        //        }
        //    }, function errorCallback(response) {
        //        alert("Error Load Question List");
        //    });
        //};


        $scope.reloadQuestionList = function() {
            loadQuestionList($scope.surveyId);
        }

        
        
        //function sendDeleteRequest (iD) {
        //        $http({
        //            method: 'Post',
        //            url: '/Question/Delete/' + iD
        //        }).then(function successCallback(response) {
        //            var result = response.data;
        //            if (result.HadError) {
        //                alert("Error: " + result.ErrorMessage);
        //            } else {
        //                loadQuestionList($scope.surveyId);
        //            }
        //        }, function errorCallback(response) {
        //            alert("Error Delete Question");
        //        });
        //    };

    }]);

angular.module("editorApp").directive('questionListDir', [ function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: function (element, attrs) {
            return "/Editor/QuestionListTemplate/";
        }
    }
}]);