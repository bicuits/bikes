"use strict";

angular.module('bikesApp')

.controller('riderListController', ["$scope", "$state", "model", function (scope, state, model) {
    scope.data = model.data;

    scope.add = function () {
        state.go("riderAdd", { id : 0} );
    };
}])

.controller('riderPwdController', ["$scope", "$state", "$stateParams", "RiderPwd",
    function (scope, state, stateParams, RiderPwd) {
        scope.rider = new RiderPwd();

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


.controller('riderEditController', ["$scope", "$state", "$stateParams", "model", "Rider", "BankBranch", "BankCustomer", "BankAccount", "chartColors",
    function (scope, state, stateParams, model, Rider, BankBranch, BankCustomer, BankAccount, chartColors) {

        scope.branches = BankBranch.query();
        scope.customers = [];
        scope.accounts = [];
        scope.model = model;

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
                    model.refresh();
                    state.go("riderList");
                },
                function () {
                    alert("Could not save rider");
                }
            );
        };

        scope.delete = function () {
            scope.rider.$delete(function () {
                model.refresh();
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
