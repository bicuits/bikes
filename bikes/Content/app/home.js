angular.module('bikesApp')

.controller('homeController', ["$scope", "$state", "model", function (scope, state, model) {

    scope.data = model.data;

    scope.$watch("data.months", function () {

        if (model.data.months) {

            var ctx = $("#barYearSummary").get(0).getContext("2d");

            new Chart(ctx).Bar(model.data.yearSummaryChartData, {
                responsive: true,
                maintainAspectRatio: false,
                multiTooltipTemplate: "<%=datasetLabel%> <%=value%>"

            });

            var ctx2 = $("#barMonthSummary").get(0).getContext("2d");

            new Chart(ctx2).Bar(model.data.monthSummaryChartData, {
                responsive: true,
                maintainAspectRatio: false,
                multiTooltipTemplate: "<%=datasetLabel%> <%=value%>"
            });
        }
    });
}]);