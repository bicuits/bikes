"use strict";

angular.module('bikesApp')

.controller('userHomeController', ["$scope", "$state", "currentUser", "model",
    function (scope, state, currentUser, model) {

        scope.data = model.data;
        scope.currentUser = currentUser;

        scope.$watch("data.months", function () {

            scope.rider = new jinqJs()
                            .from(model.data.riders)
                            .where(function (r) { return r.id == currentUser.riderId })
                            .select()[0];

            if (model.data.months) {

                //clone the year chart data
                var chartData = JSON.parse(JSON.stringify(model.data.yearSummaryChartData));

                //select just the data for this rider
                chartData.datasets =
                    new jinqJs()
                    .from(chartData.datasets)
                    .where(function (d) { return d.riderId == scope.rider.id })
                    .select();

                //scope.jsonx = JSON.stringify( chartData, null, 2);

                var ctx = $("#barYearSummary").get(0).getContext("2d");
                new Chart(ctx).Bar(chartData, {
                    responsive: true,
                    maintainAspectRatio: false,
                    multiTooltipTemplate: "<%=datasetLabel%> <%=value%>"
                });

                scope.currentUserRides = new jinqJs()
                    .from(model.data.rides)
                    .where(function (r) { return r.rider_id == currentUser.riderId })
                    .select();
            }

            scope.summary = new jinqJs()
                            .from(model.data.riderSummary)
                            .where(function (rs) { return rs.riderId == scope.rider.id })
                            .select()[0];
        });

        scope.addRide = function () {
            state.go('rideAdd', { riderId: currentUser.riderId });
        };
    }]);