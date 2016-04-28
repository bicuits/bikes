angular.module('bikesApp')

.controller('bikeListController', ["$scope", "$state", "model", function (scope, state, model) {
    scope.data = model.data;

    scope.add = function () {
        state.go("bikeAdd", { id: 0} );
    };
}])

.controller('bikeEditController', ["$scope", "$state", "$stateParams", "model", "Bike", function (scope, state, stateParams, model, Bike) {

    if (stateParams.id == 0) {
        scope.Bike = new Bike();
    } else {
        scope.bike = Bike.get({ id: stateParams.id });
    }

    scope.saveForm = function () {
        scope.bike.$save(function () {
            model.refresh();
            state.go("bikeList");
        });
    };

    scope.delete = function () {
        scope.bike.$delete(function () {
            model.refresh();
            state.go("bikeList");
        });
    };

    scope.cancel = function () {
        state.go("bikeList");
    };
}]);
