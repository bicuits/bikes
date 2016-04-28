"use strict";

angular

.module("bikeServices", ["ngResource"])

.factory('Bike', ["$resource", function (resource) {
    return resource("/api/bike/:id", {id: "@id"});
}])

.factory('Rider', ["$resource", function (resource) {
    return resource("/api/rider/:id", { id: "@id" });
}])

.factory('RiderPwd', ["$resource", function (resource) {
    return resource(
        "/api/rider/:id/pwd",
        { id: "@id" },
        {
            setPwd: {
                method: 'POST'
            }
        }
    );
}])

.factory('Route', ["$resource", function (resource) {
    return resource("/api/route/:id", { id: "@id" });
}])

.factory('Ride', ["$resource", function (resource) {
    return resource("/api/ride/:id", { id: "@id" })
}])

.factory('YearSummary', ["$resource", function (resource) {
    return resource("/api/rider/:riderId/YearSummary/:year", { riderId: "@riderId", year: "@year" });
}])

.factory('RiderSummary', ["$resource", function (resource) {
    return resource("/api/rider/:riderId/RiderSummary/:month", { riderId: "@riderId", month: "@month" });
}])

.factory('Payment', ["$resource", function (resource) {
    return resource("/api/payment");
}])

//.factory('chartColors', ["$resource", function (resource) {
//    return resource("/api/chartcolor").query();
//}])

.factory('BankBranch', ["$resource", function (resource) {
    return resource("/api/bank/branch");
}])

.factory('BankCustomer', ["$resource", function (resource) {
    return resource("/api/bank/branch/:branchId/customer", { branchId: "branchId"});
}])

.factory('BankAccount', ["$resource", function (resource) {
    return resource("/api/bank/customer/:customerId/account", { customerId: "customerId" });
}])

.factory('Model', ["$resource", function (resource) {
    return resource("/api/model/:year", { year: "@year" });
}])

.factory("model", ["Model", function (Model) {

    var getData = function () {
        return Model.get({ year: 2016 });
    };

    return {

        data: getData(),

        refresh: function () {
            this.data = getData()
        }
    };
}]);