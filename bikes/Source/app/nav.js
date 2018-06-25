angular.module('bikesApp')

.controller('navController', ["$scope", "$state", "model", "currentYear", function (scope, state, model, currentYear) {
    scope.year = currentYear.year;
    scope.years = ['2016', '2017', '2018'];

    scope.onChange = function () {
        currentYear.setYear(scope.year);
        model.refresh();
    };
}]);