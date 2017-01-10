"use strict";

angular.module('bikesApp')

.controller('userHomeController', ["$scope", "$state", "currentUser", "model",
    function (scope, state, currentUser, model) {

        scope.currentUser = currentUser;
        scope.showMonthly = true;

        model.data.$promise.then(function () {
            scope.model = model;

            scope.chartData = function () {
                if (scope.showMonthly) {
                    return model.data.currentRiderYearSummaryChartDataM;
                } else {
                    return model.data.currentRiderYearSummaryChartDataW;
                }
            };

            //scope.chartData = model.data.currentRiderYearSummaryChartDataM;

            scope.toggleChart = function () {
                scope.showMonthly = !scope.showMonthly;
            };
        });

        scope.addRide = function () {
            state.go('rideAdd', { riderId: currentUser.riderId });
        };

    }]);