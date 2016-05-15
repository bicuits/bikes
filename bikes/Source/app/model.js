"use strict";

angular.module("bikeServices")

.factory("model", ["Model", "currentUser", function (Model, currentUser) {

    var _getData = function () {

        return Model.get({ year: 2016 }, function (data) {

            //create an array to hold labels for the weeks in the year
            var weeksArray = [];
            var weeksInYear = moment().weeksInYear();

            for (var i = 1; i <= weeksInYear; i++) {
                weeksArray.push(i);
            }

            data.currentRider = new jinqJs()
                                .from(data.riders)
                                .where(function (r) { return r.id == currentUser.riderId; })
                                .select()[0];

            //alert("before " + data.rides[data.rides.length - 1].ride_date);

            data.rides.forEach(function (ride) {
                ride.ride_date = new Date(ride.ride_date);
            });

            //alert("after " + data.rides[data.rides.length - 1].ride_date);

            data.chartColors = new jinqJs()
                    .from(data.riders)
                    .select(function (rider) {
                        return {
                            fillColor: rider.color_code,
                            strokeColor: rider.color_code,
                            highlightFill: rider.color_code,
                            highlightStroke: rider.color_code
                        };
                    });

            data.yearSummaryChartData =
            {
                labels: new jinqJs()
                    .from(data.months)
                    .select(function (m) { return m.caption }),

                series: new jinqJs()
                    .from(data.riders)
                    .select(function (rider) { return rider.name }),

                colors: data.chartColors,

                data: new jinqJs()
                    .from(data.riders)
                    .select(function (rider) {
                        return new jinqJs()
                        .from(data.months)
                        .select(function (month) {
                            return new jinqJs()
                                    .from(data.rides)
                                    .where(function (r) { return r.rider_id == rider.id && r.month == month.month })
                                    .sum("distance")
                                    .select(function (s) {
                                        return Math.floor(s);
                                    })[0];
                        })
                    })
            };

            data.currentRiderYearSummaryChartData =
            {
                labels: new jinqJs()
                        .from(weeksArray)
                        .select(function (w) { return w.toString(); }),

                series: [data.currentRider.name],

                colors: [{
                    fillColor: data.currentRider.color_code,
                    strokeColor: data.currentRider.color_code,
                    highlightFill: data.currentRider.color_code,
                    highlightStroke: data.currentRider.color_code
                }],

                data: new jinqJs()
                    .from(data.riders)
                    .where(function (r) { return r.id == data.currentRider.id })
                    .select(function (rider) {
                        return new jinqJs()
                        .from(weeksArray)
                        .select(function (week) {
                            return new jinqJs()
                                    .from(data.rides)
                                    .where(function (r) {
                                        return r.rider_id == rider.id &&
                                                moment(r.ride_date).week() == week;
                                    })
                                    .sum("distance")
                                    .select(function (s) {
                                        return Math.floor(s);
                                    })[0];
                        })
                    })
            };

            data.monthSummaryChartData =
            {
                labels:
                    [moment().format("MMMM")],

                series: new jinqJs()
                    .from(data.riders)
                    .select(function (rider) { return rider.name; }),

                colors: data.chartColors,

                data:
                    new jinqJs()
                    .from(data.riders)
                    .select(function (rider) {
                        return new jinqJs()
                            .from(data.rides)
                            .where(function (r) { return r.rider_id == rider.id && r.month == moment().month() + 1; })
                            .sum("distance")
                            .select();
                    })
            };

            data.currentRiderRides = new jinqJs()
                .from(data.rides)
                .where(function (r) { return r.rider_id == currentUser.riderId })
                .select();

            data.currentRiderSummary = new jinqJs()
                .from(data.riderSummary)
                .where(function (rs) { return rs.riderId == currentUser.riderId; })
                .select()[0];

            //data.debug = JSON.stringify(data.riderSummary, null, 2);


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