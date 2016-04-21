angular.module('bikesApp')

.controller('rideListController', ["$scope", "$state", "Ride", function (scope, state, Ride) {
    scope.rides = Ride.query();

    scope.add = function () {
        state.go("rideAdd", { id: 0 });
    };
}])

.controller('rideEditController', ["$scope", "$state", "$stateParams", "Ride", function (scope, state, stateParams, Ride) {

    if (stateParams.id == 0) {
        scope.Ride = new Ride();
    } else {
        scope.ride = Ride.get({ id: stateParams.id });
    }

    scope.saveForm = function () {
        scope.ride.$save(function () {
            state.go("rideList");
        });
    };

    scope.delete = function () {
        scope.ride.$delete(function () {
            state.go("rideList");
        });
    };

    scope.cancel = function () {
        state.go("rideList");
    };
}]);
