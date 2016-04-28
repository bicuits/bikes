angular.module('bikesApp')

.controller('homeController', ["$scope", "$state", "YearSummary", "RiderSummary", function (scope, state, YearSummary, RiderSummary) {

    scope.yearSummary = YearSummary.get({ riderId: 0, year: 2016 }, function () {
        var ctx = $("#barYearSummary").get(0).getContext("2d");
        new Chart(ctx).Bar(scope.yearSummary.chartData, {
            responsive: true,
            maintainAspectRatio: false,
            multiTooltipTemplate: "<%=datasetLabel%> <%=value%>"
        });
    });

    scope.riderSummary = RiderSummary.get({ riderId: 0, month: 0 }, function () {
        var ctx = $("#barRiderDistance").get(0).getContext("2d");
        new Chart(ctx).Bar(scope.riderSummary, {
            responsive: true,
            maintainAspectRatio: false,
            multiTooltipTemplate: "<%=datasetLabel%> <%=value%>"
        });
    });

}]);