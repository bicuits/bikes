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
    return resource(
        "/api/ride/:id",
        { id: "@id" },
        {
            trash: {
                method: 'POST',
                url: "/api/ride/:id/delete"
            }
        })
}])


.factory('RiderSummary', ["$resource", function (resource) {
    return resource("/api/rider/:riderId/RiderSummary/:month", { riderId: "@riderId", month: "@month" });
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

.factory("model", ["Model", function (Model) {

    var _getData = function () {

        return Model.get({ year: 2016 }, function (data) {

            //alert("before " + data.rides[data.rides.length - 1].ride_date);

            data.rides.forEach(function (ride) {
                ride.ride_date = new Date(ride.ride_date);
            });

            //alert("after " + data.rides[data.rides.length - 1].ride_date);


            data.yearSummaryChartData =
            {
                labels:
                    new jinqJs().from(data.months).select(function (m) { return m.caption }),
                datasets:
                    new jinqJs()
                    .from(data.riders)
                    .select(function (rider) {
                        return {
                            riderId: rider.id,
                            label: rider.name,
                            fillColor: rider.color_code,
                            data: new jinqJs()
                                .from(data.months)
                                .select(function (month) {
                                    return new jinqJs()
                                            .from(data.rides)
                                            .where(function (r) { return r.rider_id == rider.id && r.month == month.month })
                                            .sum("distance")
                                            .select()[0];
                                })
                        };
                    })
            };

            //data.debug = JSON.stringify(new jinqJs().from(data.riders).select(function (r) { return r.name; }), null, 2 );

            data.monthSummaryChartData =
            {
                labels:
                    [ moment().format("MMMM") ],
                datasets:
                    new jinqJs()
                    .from(data.riders)
                    .select(function (rider) {
                        return {
                            riderId: rider.id,
                            label: rider.name,
                            fillColor: rider.color_code,
                            data: new jinqJs()
                                        .from(data.rides)
                                        .where(function (r) { return r.rider_id == rider.id && r.month == moment().month() + 1 })
                                        .sum("distance")
                                        .select()
                            };
                    })
            };

            //data.debug = JSON.stringify( data.monthSummaryChartData , null, 2);

        });
    };

    return {

        data: _getData(),

        refresh: function () {
            this.data = _getData()
        }
    };
}]);