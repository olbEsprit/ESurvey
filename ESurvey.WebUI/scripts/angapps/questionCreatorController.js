angular.module("editorApp").controller('questionCreatorController', [
    '$scope', 'qListService', function ($scope, qListService) {

        $scope.questionTypes = [
            {
                Id : 1,
                Title : "MultiChoise"
            },
            {
                Id: 2,
                Title: "Open"
            },
            {
                Id: 3,
                Title: "Matrix"
            }
        ];

        $scope.createQuestion = function(typeId) {
            
            qListService.createQuestion(typeId);
        };

        

        
    }
]);


angular.module("editorApp").directive('createQuestionDirective', [function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: function (element, attrs) {
            return "/Editor/CreateQuestionTemplate/";
        }
    }
}]);
