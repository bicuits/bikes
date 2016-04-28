angular.module('bikesApp')

.controller('routeListController', ["$scope", "$state", "Route", function (scope, state, Route) {
    scope.routes = Route.query();

    scope.add = function () {
        state.go("routeAdd", { id: 0 });
    };

}])

.controller('routeEditController', ["$scope", "$state", "$stateParams", "Route", function (scope, state, stateParams, Route) {

    if (stateParams.id == 0) {
        scope.route = new Route();
    } else {
        scope.route = Route.get({ id: stateParams.id });
    }

    scope.saveForm = function () {
        scope.route.$save(function () {
            state.go("routeList");
        });
    };

    scope.delete = function () {
        scope.route.$delete(function () {
            state.go("routeList");
        });
    };

    scope.cancel = function () {
        state.go("routeList");
    };

}]);