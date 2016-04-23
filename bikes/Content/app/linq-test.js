angular.module('bikesApp')

.controller('linqTestController', ["$scope", "$state", "uiGridConstants", "RawData", function (scope, state, uiGridConstants, RawData) {

    scope.gridOptions = {
        enableSorting: true,
        enableFiltering: true,
        showGridFooter: true,
        showColumnFooter: true,
        columnDefs: [
            {
                name: 'rider',
                field: 'rider'
            },
            {
                name: 'bike',
                field: 'bike'
            },
            {
                name: 'route',
                field: 'route'
            },
            {
                name: 'distance',
                field: 'distance',
                type: 'number',
                aggregationType: uiGridConstants.aggregationTypes.sum,
                enableFiltering: false
            },
            {
                name: 'reward',
                field: 'reward',
                typee: 'number',
                aggregationType: uiGridConstants.aggregationTypes.sum,
                enableFiltering: false
            },
            {
                name: 'date',
                field: 'ride_date',
                enableFiltering: false
                //filters: [
                //{
                //    condition: uiGridConstants.filter.GREATER_THAN,
                //    placeholder: 'greater than'
                //},
                //{
                //    condition: uiGridConstants.filter.LESS_THAN,
                //    placeholder: 'less than'
                //}]
            }
        ],
        data: []
    };

    scope.data = RawData.get({ year: 2016 }, function () {
        scope.gridOptions.data = scope.data.rides;
    });

}]);

