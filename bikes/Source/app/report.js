angular.module('bikesApp')

.controller('reportController', ["$scope", "$state", "Report", function (scope, state, Report) {
    scope.routeReport = [];

    //scope.daySpanOptions = [
    //    { label: 'last 30 days', value: 30},
    //    { label: 'last 60 days', value: 60 },
    //    { label: 'last 120 days', value: 120 }
    //];
    //scope.daySpan = scope.daySpanOptions[1];

    //scope.onChange = function () {
    //    //getReports();
    //};

    function getReports() {
        Report.get(
            function (data) {
                scope.reports = data.reports;
                //if (scope.reports[0]) {
                //    scope.reports[0].daySpan = scope.daySpanOptions[1];
                //}
            },
            function () {
                alert("Failed to get report data.");
            }
        );
    }
    getReports();

}]);
