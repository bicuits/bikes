"use strict";

angular.module('bikesApp')

.controller('userHomeController', ["$scope", "$state", "currentUser", "model",
    function (scope, state, currentUser, model) {

        scope.data = model.data;
        scope.currentUser = currentUser;

        scope.addRide = function () {
            state.go('rideAdd', { riderId: currentUser.riderId });
        };
    }]);