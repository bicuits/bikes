angular.module('bikesApp')

.controller('homeController', ["$scope", "$state", "model", function (scope, state, model) {

    scope.data = model.data;

    scope.$watch("data.yearSummary", function () {

        if (model.data.yearSummary) {

            var ctx = $("#barYearSummary").get(0).getContext("2d");

            new Chart(ctx).Bar(scope.data.yearSummary.chartData, {
                responsive: true,
                maintainAspectRatio: false,
                multiTooltipTemplate: "<%=datasetLabel%> <%=value%>"

            });

            var ctx2 = $("#barMonthSummary").get(0).getContext("2d");

            new Chart(ctx2).Bar(model.data.monthSummary, {
                responsive: true,
                maintainAspectRatio: false,
                multiTooltipTemplate: "<%=datasetLabel%> <%=value%>"
            });
        }
    });

    //scope.yearSummary = YearSummary.get({ riderId: 0, year: 2016 }, function () {
    //    var ctx = $("#barYearSummary").get(0).getContext("2d");
    //    new Chart(ctx).Bar(scope.yearSummary.chartData, {
    //        responsive: true,
    //        maintainAspectRatio: false,
    //        multiTooltipTemplate: "<%=datasetLabel%> <%=value%>"
    //    });
    //});

    //scope.riderSummary = RiderSummary.get({ riderId: 0, month: 0 }, function () {
    //    var ctx = $("#barRiderDistance").get(0).getContext("2d");
    //    new Chart(ctx).Bar(scope.riderSummary, {
    //        responsive: true,
    //        maintainAspectRatio: false,
    //        multiTooltipTemplate: "<%=datasetLabel%> <%=value%>"
    //    });
    //});

}]);