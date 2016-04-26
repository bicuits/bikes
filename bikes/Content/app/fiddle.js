angular.module('bikesApp')

.controller('fiddleController', ["$scope", "$state", "$stateParams", "RawData", function (scope, state, stateParams, RawData) {
    RawData.get({ year: 2016 }, function (data) {
        //alert("data = " + data.rides[0].rider_id);
        scope.rides = Enumerable
                        .from(data.rides)
                        .where(function (r) { return r.route == "Other" })
                        .toArray();
    });

}]);