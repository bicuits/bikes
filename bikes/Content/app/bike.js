angular.module('bikesApp')

.controller('bikeListController', ["$scope", "$state", "Bike", function (scope, state, Bike) {
    scope.bikes = Bike.query();

    scope.add = function () {
        state.go("bikeAdd", { id: 0} );
    };
}])

.controller('bikeEditController', ["$scope", "$state", "$stateParams", "Bike", function (scope, state, stateParams, Bike) {

    if (stateParams.id == 0) {
        scope.Bike = new Bike();
    } else {
        scope.bike = Bike.get({ id: stateParams.id });
    }

    scope.saveForm = function () {
        scope.bike.$save(function () {
            state.go("bikeList");
        });
    };

    scope.delete = function () {
        scope.bike.$delete(function () {
            state.go("bikeList");
        });
    };

    scope.cancel = function () {
        state.go("bikeList");
    };
}]);
