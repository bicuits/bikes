"use strict";

angular

.module("bikeServices", ["ngResource"])

.factory('Bike', ["$resource", function (resource) {
    return resource(
        "/api/bike/:id",
        { id: "@id" },
        {
            trash: {
                method: 'POST',
                url: "/api/bike/:id/delete"
            }
        });
}])

.factory('Rider', ["$resource", function (resource) {
    return resource(
        "/api/rider/:id",
        { id: "@id" },
        {
            trash: {
                method: 'POST',
                url: "/api/rider/:id/delete"
            }
        });
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
    return resource(
        "/api/route/:id",
        { id: "@id" },
        {
            trash: {
                method: 'POST',
                url: "/api/route/:id/delete"
            }
        });
}])

.factory('Ride', ["$resource", function (resource) {
    return resource(
        "/api/ride/:id",
        { id: "@id" },
        {
            trash: {
                method: 'POST',
                url: "/api/ride/:id/delete"
            }
        });
}])

.factory('Payment', ["$resource", function (resource) {
    return resource("/api/payment");
}])

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

.factory('Report', ["$resource", function (resource) {
    return resource("/api/report/:id", { id: "@id" });
}])


.factory('Admin', ["$resource", function (resource) {
    return resource(
        "/api/admin/",
        null,
        {
            refresh : {
                method: 'POST',
                url: "/api/admin/routes/update"
            }
        });
}]);

