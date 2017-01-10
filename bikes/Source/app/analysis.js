angular.module('bikesApp')

.controller('analysisController', ["$scope", "$state", "uiGridConstants", "model", function (scope, state, uiGridConstants, model) {

    scope.model = model;

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
                footerCellTemplate: "<div>{{col.getAggregationValue() | number : 0}}</div>",
                enableFiltering: false
            },
            {
                name: 'reward',
                field: 'reward',
                type: 'number',
                aggregationType: uiGridConstants.aggregationTypes.sum,
                footerCellTemplate: "<div>{{col.getAggregationValue() | number : 2}}</div>",
                enableFiltering: false
            },
            {
                name: 'date',
                field: 'ride_date',
                type: 'date',
                cellFilter: 'date',
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
        data: "model.data.rides"
    };

}]);

