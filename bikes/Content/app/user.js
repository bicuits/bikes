"use strict";

angular.module('bikesApp')

.controller('userHomeController', ["$scope", "$state", "currentUser", "YearSummary", "Ride",
    function (scope, state, currentUser, YearSummary, Ride) {

        Ride.query(function (data) {
            scope.rides = Enumerable
                            .from(data)
                            .where( function (r) {return r.rider_id == currentUser.riderId} )
                            .toArray();
        });


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