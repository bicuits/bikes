angular.module('bikesApp')

.controller('navController', ["$scope", "$state", "model", "currentYear", function (scope, state, model, currentYear) {
    scope.currentYear = currentYear.getYear();
    scope.years = ['2016', '2017'];

    scope.onChange = function () {
        currentYear.setYear(scope.currentYear);
        model.refresh();
    };
}]);