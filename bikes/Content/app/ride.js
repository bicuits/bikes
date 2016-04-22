angular.module('bikesApp')

.controller('rideListController', ["$scope", "$state", "Ride", function (scope, state, Ride) {
    //scope.riders = Rider.query();
    scope.rides = Ride.query();

    scope.add = function () {
        state.go("rideAdd");
    };

}])

.controller('rideAddController', ["$scope", "$state", "Ride", "Rider", "Route", "Bike", function (scope, state, Ride, Rider, Route, Bike) {

    var getRider = function (id) {
        var i;
        for (i = 0; i < scope.riders.length; i++) {
            if (scope.riders[i].id === id) {
                return scope.riders[i];
            }
        }
    }

    var getRoute = function (id) {
        var i;
        for (i = 0; i < scope.routes.length; i++) {
            if (scope.routes[i].id === id) {
                return scope.routes[i];
            }
        }
    }

    scope.routes = Route.query();
    scope.riders = Rider.query();
    scope.bikes  = Bike.query();

    scope.ride = new Ride();
    scope.ride.distance = 0;
    scope.ride.payable = false;
    scope.ride.bonus = 0;
    scope.ride.riderId = 0;
    scope.ride.routeId = 1;
    scope.ride.bikeId = 1;

    scope.calculateReward = function () {
        var distance;
        var reward;

        if (!scope.ride.payable) {
            reward = 0;
        } else {

            //find the rider
            var rider = getRider(scope.ride.riderId);
            if (!rider) {
                reward = scope.ride.bonus;
            } else {

                //see if there is a route selected
                if (scope.ride.routeId > 1) {
                    var route = getRoute(scope.ride.routeId);
                    distance = route.distance;

                    if (scope.ride.returnRide) {
                        distance = distance * 2;
                    }
                } else {
                    distance = scope.ride.distance;
                }

                reward = scope.ride.bonus + (distance * rider.rate) / 100;
            }
        }
        return reward.toFixed(2);
    };

    scope.saveForm = function () {
        scope.ride.$save(
            function () {
                state.go("rideList");
            },
            function () {
                alert("Failed to save new ride.");
                state.go("rideList");
            }
        );
    };

    scope.cancel = function () {
        state.go("rideList");
    };
}])

.controller('rideEditController', ["$scope", "$state", "$stateParams", "Ride", function (scope, state, stateParams, Ride) {

    scope.ride = Ride.get({ id: stateParams.id });

    scope.saveForm = function () {
        Ride.notes
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
