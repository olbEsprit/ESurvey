var editorApp = angular.module("editorApp", ["ngRoute", "xeditable"])
    .config(function($routeProvider) {
        $routeProvider.when('/EditQuestion/:QuestionId*', {
            templateUrl: function (urlattr) {
                return '/Editor/EditQuestion/' + urlattr.QuestionId;
            },
            controller: 'questionEditorController'
        });
        $routeProvider.when('/answer',
        {
            templateUrl: '/Home/Test',
            controller: 'testCtrl'
        });
    });




editorApp.service('qListService', ["$http",function ($http) {

    var questions = [];
    var surveyId;
    var onModelChangeSubscribers = [];
    var me = this;

    this.setSurveyId = function (id) {
        surveyId = id;
    };

    this.getSurveyId = function () {
        return surveyId;
    };
    
    this.getQList = function () {
        return questions;
    };

    function onModelChange() {
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

    this.loadQuestionList = function (surveyId) {
        return $http.get("/Question/QuestionList/"+surveyId).then(function (data) {
            return data.data;
        });
    };

    this.UpdateQuestionInList = function (model) {
        exchangeQuestionInList(model);
        onModelChange();
    };

    this.loadSingleQuestion = function(questionId) {
        return $http.get("/Question/ListModel/" + questionId).then(
            function (data) {
                if (data.HadError) {
                    alert(data.ErrorMessage);
                } else {
                    exchangeQuestionInList(data.Data);
                }
            },
            function(data) {
                alert("Failed To Load Question Link")
            }
        );
    };

    this.reloadQuestionList = function() {
        this.loadQuestionList(surveyId).then(function(res) {
            if (res.HadError) {
                alert(res.ErrorMessage);
            } else {
                questions = res.Data;
                onModelChange();
            }
        });
    };

    this.setQuestionNumber = function(questionId, targetNumber) {
        var endpoint = "/Question/SetPosition";
        var model = {
            Id:questionId,
            Position:targetNumber
        };

        $http({
            method: 'POST',
            data: model,
            url: endpoint
        }).then(function successCallback(response) {
            var result = response.data;
            if (result.HadError) {
                alert("Error: " + result.ErrorMessage);
            } else {
                me.reloadQuestionList();
            }
        }, function errorCallback(response) {
            alert("Error Load Question List");
        });
    };

    
    

    this.deleteQuestionById = function(questionId) {
        sendDeleteRequest(questionId).then(function(res) {
            if (res.HadError) {
                alert(res.ErrorMessage);
            } else {
                removeQuestionFromListById(questionId);
                onModelChange();
            }
        });
    };

    this.createQuestion = function (typeId) {
        var model = {
            SurveyId: surveyId,
            QuestionTypeId: typeId
        };
        sendCreateRequest(model).then(function(res) {


            if (res.HadError) {
                alert(res.ErrorMessage);
            } else {
                me.reloadQuestionList();
            }
        });
    };

    this.renameQuestion = function (questionId, newName) {
        //alert(questionId);
        var endpoint = "/Question/Rename";
        var model = {
            Id: questionId,
            Name: newName
        };
        $http({
            method: 'POST',
            data:model,
                    url: endpoint
                }).then(function successCallback(response) {
                    var result = response.data;
                    if (result.HadError) {
                        alert("Error: " + result.ErrorMessage);
                    } else {
                        me.reloadQuestionList();
                    }
                }, function errorCallback(response) {
                    alert("Error Load Question List");
                });
        //$.post(endpoint,model).then(
        //    function successCallback(data) {
        //        var result = data.data;
        //        if (result.HadError) {
        //            alert(result.ErrorMessage);
        //        } else {
        //            me.reloadQuestionList();
        //        }
        //    },
        //    function errorCallback() {
        //        alert("Failed Rename");
        //    });
    };

    this.changeQuestionPosition = function(request) {
        
    }


    /*
    this.updateSingleQuestion = function(item) {
        var index = getIndexById(item.Id);
        questions[index] = item;
    };
    */

    function getIndexById(id) {
        questions.forEach(function(item, index) {
            if (item.Id == id) {
                return index;
            } else {
                return null;
            }
        }); 

    };

    

    function removeQuestionFromListById(id) {
        questions = questions.filter(function (item) {
            return item.Id !== id;
        });
    };

    function exchangeQuestionInList(model) {
        questions.forEach(function(item, inxed, arr) {
            if (item.Id == model.Id) {
                arr[inxed] = model;
            }
        });
    };
   
    function sendDeleteRequest(iD) {
       return  $http({
            method: 'Post',
            url: '/Question/Delete/' + iD
        }).then(function successCallback(response) {
            var result = response.data;
           return result;
       }, function errorCallback(response) {
            alert("Error Delete Question");
        });
    };


    function sendCreateRequest(addQuestionUiModel) {
       return  $http({
            method: 'POST',
            url: '/Question/Create',
            data: addQuestionUiModel
        }).then(function successCallback(response) {
            var result = response.data;
            return result;
        }, function errorCallback(response) {
            alert("Error Create Question");
        });
    };


}]);




editorApp.service('qEditorService', ["$http", function ($http) {


    var me = this;

    var questionId = 0;
    var questionType = 0;

    this.GetQuestionId = function() {
        return questionId;
    };

    this.SetQuestionId = function (newId) {
        questionId = newId;
    };

    this.setQuestionType = function (newType) {
        questionType = newId;
    };
    this.setQuestionType = function () {
        return questionType;
    };


    var QuestionDetails = {};

    

    this.ReloadQuestionDetails = function() {
        LoadQuestionDetails().then(
            function (data) {
                if (data.HadError) {
                    alert(data.ErrorMessage);
                } else {
                    QuestionDetails = data.Data;
                }

            });
    };

    this.toggleOtherAnswerOption = function(val) {
        var model = {
            QuestionId: questionId,
            Toggle: val
        };
        return $http({
            method: 'POST',
            url: '/Question/ToggleOtherAnswerOption/',
            data: model
        }).then(function successCallback(response) {
            var result = response.data;
            return result;
        }, function errorCallback(response) {
            alert("Error Toggle Other Question");
        });
    };

    


    this.LoadQuestionDetails = function() {
        return FetchQuestionDetails(questionId);
    }



    function FetchQuestionDetails (id) {
                return $http({
                    method: 'GET',
                    url: '/Question/Details/' + id
                }).then(function successCallback(response) {
                    var result = response.data;
                    return result;
                }, function errorCallback(response) {
                    alert("Error Fetch Question");
                });
    };

    

    this.UpdateQuestion = function (model) {
        return $http({
            method: 'POST',
            url: '/Question/Update/',
            data: model
        }).then(function successCallback(response) {
            var result = response.data;
            return result;
        }, function errorCallback(response) {
            alert("Error Yodate Question");
            alert(response.Message);
        });
    };

}]);




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


editorApp.service('multiAnswerService', [
    "$http", function($http) {


        this.FetchAnswers = function(id) {
            return $http.get("/Answer/All/" + id);
        };


        this.CreateAnswer = function(id) {
            return $http.post("/Answer/Create/" + id);

        };

        this.DeleteAnswer = function (id) {
            return $http.post("/Answer/Delete/" + id);

        };


        this.RenameAnswer = function(model) {
            return $http.post("/Answer/Rename", model);
        };

        this.ChangeNumber = function(model) {
            return $http.post("/Answer/ChangeNumber/", model);
        };


        this.Hide = function (model) {
            return $http.post("/Answer/Hide/", model);
        };
    }
]);

editorApp.service('matrixRowService', [
    "$http", function ($http) {


        this.FetchRows = function (id) {
            return $http.get("/Row/All/" + id);
        };


        this.CreateRow = function (id) {
            return $http.post("/Answer/Create/" + id);

        };

        this.DeleteRow = function (id) {
            return $http.post("/Answer/Delete/" + id);

        };


        this.RenameRow = function (model) {
            return $http.post("/Answer/Rename", model);
        };

        this.ChangeNumber = function (model) {
            return $http.post("/Answer/ChangeNumber/", model);
        };


        this.Hide = function (model) {
            return $http.post("/Answer/Hide/", model);
        };
    }
]);




editorApp.controller('multiAnswerController', ['$scope', 'multiAnswerService', '$q', function ($scope, multiAnswerService, $q) {

        $scope.answers = [];    
        $scope.loadAnswers = function () {

        multiAnswerService.FetchAnswers($scope.QuestionId)
            .then(function (result) {
                if (result.data.HadError) {
                    alert(result.data.ErrorMessage);
                } else {
                    $scope.answers = result.data.Data;
                }
            }, function(data) { alert("FetchAnswrsError") });
    };

        $scope.CreateAnswer = function() {
            multiAnswerService.CreateAnswer($scope.QuestionId)
            .then(function (result) {
                if (result.data.HadError) {
                    alert(result.data.ErrorMessage);
                } else {
                    $scope.answers.push(result.data.Data);
                }
            }, function (data) { alert("Create AnswerError") });
        };


        $scope.DeleteAnswer = function (id) {
            multiAnswerService.DeleteAnswer(id)
            .then(function (result) {
                if (result.data.HadError) {
                    alert(result.data.ErrorMessage);
                } else {
                    deleteById(id);
                }
            }, function (data) { alert("Delete AnswerError") });
        };

        $scope.Rename = function (data) {
            var d = $q.defer();
            var model = {
                Id:data.Id,
                Name:data.Title
            };
            multiAnswerService.RenameAnswer(model).then(function(responce) {
                var res = responce.data;
                if (res.HadError) {
                  alert(res.ErrorMessage);  
                } else {
                    
                }

            }, function() {});
        };


        function deleteById(id) {
            $scope.answers = $scope.answers.filter(function (val) {
                return val.Id !== id;
            });

        };



        $scope.init = function (id) {
            $scope.QuestionId = id;
            $scope.loadAnswers();
        };


    }
]);




editorApp.service('qEditorService', ["$http", function ($http) {


    var me = this;

    var questionId = 0;
    var questionType = 0;

    this.GetQuestionId = function() {
        return questionId;
    };

    this.SetQuestionId = function (newId) {
        questionId = newId;
    };

    this.setQuestionType = function (newType) {
        questionType = newId;
    };
    this.setQuestionType = function () {
        return questionType;
    };


    var QuestionDetails = {};

    

    this.ReloadQuestionDetails = function() {
        LoadQuestionDetails().then(
            function (data) {
                if (data.HadError) {
                    alert(data.ErrorMessage);
                } else {
                    QuestionDetails = data.Data;
                }

            });
    };

    this.toggleOtherAnswerOption = function(val) {
        var model = {
            QuestionId: questionId,
            Toggle: val
        };
        return $http({
            method: 'POST',
            url: '/Question/ToggleOtherAnswerOption/',
            data: model
        }).then(function successCallback(response) {
            var result = response.data;
            return result;
        }, function errorCallback(response) {
            alert("Error Toggle Other Question");
        });
    };

    


    this.LoadQuestionDetails = function() {
        return FetchQuestionDetails(questionId);
    }



    function FetchQuestionDetails (id) {
                return $http({
                    method: 'GET',
                    url: '/Question/Details/' + id
                }).then(function successCallback(response) {
                    var result = response.data;
                    return result;
                }, function errorCallback(response) {
                    alert("Error Fetch Question");
                });
    };

    

    this.UpdateQuestion = function (model) {
        return $http({
            method: 'POST',
            url: '/Question/Update/',
            data: model
        }).then(function successCallback(response) {
            var result = response.data;
            return result;
        }, function errorCallback(response) {
            alert("Error Yodate Question");
            alert(response.Message);
        });
    };

}]);