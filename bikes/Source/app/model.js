"use strict";

angular.module("bikeServices")

.factory("model", ["Model", "currentUser", "currentYear", function (Model, currentUser, currentYear) {

    var _getData = function (year) {

        return Model.get({ year: year }, function (data) {

            data.currentRider = new jinqJs()
                                .from(data.riders)
                                .where(function (r) { return r.id == currentUser.riderId; })
                                .select()[0];

            //convert dates from stringso to javascript date objects
            data.rides.forEach(function (ride) {
                ride.ride_date = new Date(ride.ride_date);
            });

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

            //create an array to hold labels for the weeks in the year
            var i;
            var weeksArray = [];
            var monthsArray = [];
            var weeksInYear = moment().weeksInYear();

            for (i = 1; i <= weeksInYear; i++) {
                weeksArray.push({ id: i, caption : i.toString() });
            }

            for (i = 0; i < 12; i++) {
                monthsArray.push({ id: i + 1, caption: moment([currentYear, i, 1]).format("MMM") });
            }

            //utility function for grouping and filtering data
            var _getCurrentRiderYearSummary = function (labels, filter) {
                return {
                    labels: new jinqJs()
                            .from(labels)
                            .select(function (label) { return label.caption; }),

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
                            .from(labels)
                            .select(function (label) {
                                return new jinqJs()
                                        .from(data.rides)
                                        .where(function (r) {
                                            return r.rider_id == rider.id && filter(r, label);
                                        })
                                        .sum("distance")
                                        .select(function (s) {
                                            return Math.floor(s);
                                        })[0];
                            })
                        })
                };
            };

            data.yearSummaryChartData = {

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

            data.currentRiderYearSummaryChartDataW = _getCurrentRiderYearSummary(weeksArray, function (ride, label) {
                return (moment(ride.ride_date).week() == label.id);
            });

            data.currentRiderYearSummaryChartDataM = _getCurrentRiderYearSummary(monthsArray, function (ride, label) {
                return (ride.month == label.id);
            });

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

        });
    };

    return {

        data: _getData(currentYear),

        refresh: function () {
            this.data = _getData(currentYear)
        }
    };
}]);