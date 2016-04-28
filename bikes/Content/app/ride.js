angular.module('bikesApp')

.controller('rideListController', ["$scope", "$state", "Ride", function (scope, state, Ride) {
    //scope.riders = Rider.query();
    scope.rides = Ride.query();

    scope.add = function () {
        state.go("rideAdd");
    };

}])

.controller('rideAddController', ["$scope", "$state", "$stateParams", "Ride", "Rider", "Route", "Bike",
    function (scope, state, stateParams, Ride, Rider, Route, Bike) {

        scope.routeCaption = function (route) {
            if (route.id == 1) {
                return route.name;
            } else {
                return route.name + ' (' + route.distance + ' miles)';
            }
        };

        var goBack = function () {
            if (stateParams.riderId) {
                state.go("userHome");
            } else {
                state.go("rideList");
            }
        };

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
        scope.bikes  = Bike.query();
        scope.riders = Rider.query();

        scope.ride = new Ride();
        scope.ride.distance = 0;
        scope.ride.payable = false;
        scope.ride.bonus = 0;
        scope.ride.rider_id = stateParams.riderId ? parseInt(stateParams.riderId, 10) : 0;
        //scope.ride.rider_id = 2;
        scope.ride.route_id = 1;
        scope.ride.bike_id = 1;

        scope.calculateReward = function () {
            var distance;
            var reward;

            if (!scope.ride.payable) {
                reward = 0;
            } else {

                //find the rider
                var rider = getRider(scope.ride.rider_id);
                if (!rider) {
                    reward = scope.ride.bonus;
                } else {

                    //see if there is a route selected
                    if (scope.ride.routeId > 1) {
                        var route = getRoute(scope.ride.route_id);
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
                    goBack();
                },
                function () {
                    alert("Failed to save new ride.");
                    goBack();
                }
            );
        };

        scope.cancel = function () {
            goBack();
        };
    }
])

.controller('rideEditController', ["$scope", "$state", "$stateParams", "Ride", function (scope, state, stateParams, Ride) {

    scope.ride = Ride.get({ id: stateParams.id });

    var goBack = function () {
        if (stateParams.riderId) {
            state.go("userHome");
        } else {
            state.go("rideList");
        }
    };

    scope.saveForm = function () {
        Ride.notes
        scope.ride.$save(function () {
            goBack();
        });
    };

    scope.delete = function () {
        scope.ride.$delete(function () {
            goBack();
        });
    };

    scope.cancel = function () {
        goBack();
    };
}]);
