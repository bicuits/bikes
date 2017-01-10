angular.module('bikesApp')

.controller('homeController', ["$scope", "$state", "model", "currentYear", function (scope, state, model, currentYear) {
    scope.model = model;
    scope.currentYear = currentYear.getYear();
}]);