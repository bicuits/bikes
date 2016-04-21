"use strict";

angular

.module("bikeServices", ["ngResource"])

.factory('Bike', ["$resource", function (resource) {
    return resource("/api/bike/:id", {id: "@id"});
}])

.factory('Rider', ["$resource", function (resource) {
    return resource("/api/rider/:id", { id: "@id" });
}])

.factory('Route', ["$resource", function (resource) {
    return resource("/api/route/:id", { id: "@id" });
}])

.factory('Ride', ["$resource", function (resource) {
    return resource("/api/ride/:id", { id: "@id" });
}])

//.factory('Model', ["$resource", function (resource) {
//    return resource("/api/lookup").get();
//}])

.factory('chartColors', ["$resource", function (resource) {
    return resource("/api/chartcolor").query();
}])

.factory('BankBranch', ["$resource", function (resource) {
    return resource("/api/bank/branch");
}])

.factory('BankCustomer', ["$resource", function (resource) {
    return resource("/api/bank/branch/:branchId/customer", { branchId: "branchId"});
}])

.factory('BankAccount', ["$resource", function (resource) {
    return resource("/api/bank/customer/:customerId/account", { customerId: "customerId" });
}]);