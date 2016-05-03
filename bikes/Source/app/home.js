angular.module('bikesApp')

.controller('homeController', ["$scope", "$state", "model", function (scope, state, model) {

    scope.data = model.data;

}]);