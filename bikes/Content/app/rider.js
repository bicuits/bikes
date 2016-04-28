"use strict";

angular.module('bikesApp')

.controller('riderListController', ["$scope", "$state", "Rider", function (scope, state, Rider) {
    scope.riders = Rider.query();

    scope.add = function () {
        state.go("riderAdd", { id : 0} );
    };
}])

.controller('riderPwdController', ["$scope", "$state", "$stateParams", "RiderPwd",
    function (scope, state, stateParams, RiderPwd) {
        scope.rider = new RiderPwd();
        //scope.rider.pwd = "joe";

        scope.saveForm = function () {
            scope.rider.$setPwd(
                {id: stateParams.id},
                function () {
                    state.go("riderList");
                },
                function () {
                    alert("Could not save rider");
                }
            );
        };

        scope.cancel = function () {
            state.go("riderList");
        };
    }])


.controller('riderEditController', ["$scope", "$state", "$stateParams", "Rider", "BankBranch", "BankCustomer", "BankAccount", "chartColors",
    function (scope, state, stateParams, Rider, BankBranch, BankCustomer, BankAccount, chartColors) {

        scope.branches = BankBranch.query();
        scope.customers = [];
        scope.accounts = [];
        scope.chartColors = chartColors;

        if (stateParams.id == 0) {
            scope.rider = new Rider();
        } else {
            scope.rider = Rider.get({ id: stateParams.id });
        }

        scope.$watch("rider.bank_branch_id", function () {
            scope.customers = BankCustomer.query({ branchId: scope.rider.bank_branch_id});
        });

        scope.$watch("rider.bank_customer_id", function () {
            scope.accounts = BankAccount.query({ customerId: scope.rider.bank_customer_id });
        });

        scope.saveForm = function () {

            scope.rider.$save(
                function () {
                    state.go("riderList");
                },
                function () {
                    alert("Could not save rider");
                }
            );
        };

        scope.delete = function () {
            scope.rider.$delete(function () {
                state.go("riderList");
            });
        };

        scope.setPwd = function () {
            state.go("riderPwd", {id: stateParams.id});
        };

        scope.cancel = function () {
            state.go("riderList");
        };

    }]);
