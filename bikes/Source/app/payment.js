angular.module('bikesApp')

.controller('paymentListController', ["$scope", "$state", "model", "Payment", function (scope, state, model, Payment) {
    scope.data = model.data;

    scope.pay = function () {
        Payment.save();
        model.refresh();
        //state.go("home");
    };
}]);