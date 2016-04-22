angular.module('bikesApp')

.controller('paymentListController', ["$scope", "$state", "Payment", function (scope, state, Payment) {
    scope.payments = Payment.query();

    scope.pay = function () {
        Payment.save();
        state.go("home");
    };
}]);