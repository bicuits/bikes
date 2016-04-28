"use strict";

angular.module('bikesApp')

.controller('userHomeController', ["$scope", "$state", "currentUser", "YearSummary", "model",
    function (scope, state, currentUser, YearSummary, model) {

        scope.selectRides = function () {
            return Enumerable
                    .from(scope.data.rides)
                    .where(function (r) { return r.rider_id == currentUser.riderId })
                    .orderByDescending(function (r) {
                        return r.ride_date.replace(/(\d+)\/(\d+)\/(\d+)/, "$3/$2/$1");
                    })
                    .toArray();
        };


        scope.data = model.data;

        //scope.$watch("model.data", function () {
        //    scope.rides = selectRides(model.data.rides, currentUser.riderId);
        //});


        YearSummary.get({ riderId: currentUser.riderId, year: 2016 }, function (data) {

            scope.currentUser = currentUser;

            scope.summary = data.riderSummary[0];

            var ctx = $("#barYearSummary").get(0).getContext("2d");
            new Chart(ctx).Bar(data.chartData, {
                responsive: true,
                maintainAspectRatio: false,
                multiTooltipTemplate: "<%=datasetLabel%> <%=value%>"
            });
        });

        scope.addRide = function () {
            state.go('rideAdd', {riderId: currentUser.riderId});
        };
    }]);