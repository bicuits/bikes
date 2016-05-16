"use strict";

angular.module('bikesApp')

.controller('userHomeController', ["$scope", "$state", "currentUser", "model",
    function (scope, state, currentUser, model) {

        scope.currentUser = currentUser;
        scope.showMonthly = true;

        model.data.$promise.then(function () {
            scope.data = model.data;
            scope.chartData = model.data.currentRiderYearSummaryChartDataM;

            scope.toggleChart = function () {
                if (scope.showMonthly) {
                    scope.chartData = model.data.currentRiderYearSummaryChartDataW;
                    scope.showMonthly = false;
                } else {
                    scope.chartData = model.data.currentRiderYearSummaryChartDataM;
                    scope.showMonthly = true;
                }
            };
        });

        scope.addRide = function () {
            state.go('rideAdd', { riderId: currentUser.riderId });
        };

    }]);