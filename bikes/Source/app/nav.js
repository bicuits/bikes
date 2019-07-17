angular.module('bikesApp')

    .controller('navController', ["$scope", "$state", "model", "currentYear", function (scope, state, model, currentYear) {
        scope.year = currentYear.year;
        scope.years = [];

        var thisYear = new Date().getFullYear();

        for (var i = 2016; i <= thisYear && i < 2026; i++) {
            scope.years.push(i.toString());
        }

        scope.onChange = function () {
            currentYear.setYear(scope.year);
            model.refresh();
        };
    }
]);