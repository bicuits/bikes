// app.js
"use strict";

angular

.module('bikesApp', ['ui.router', "bikeServices"])

.config(function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/home');

    $stateProvider

        // HOME STATES AND NESTED VIEWS ========================================
        .state('home', {
            url: '/home',
            templateUrl: '/content/app/html/home.html'
        })

        // nested list with custom controller
        .state('home.list', {
            url: '/list',
            templateUrl: '/content/app/html/home-list.html',
            controller: function ($scope) {
                $scope.dogs = ['Bernese', 'Husky', 'Goldendoodle'];
            }
        })

        // nested list with just some random string data
        .state('home.paragraph', {
            url: '/paragraph',
            template: 'I could sure use a drink right now.'
        })

        // RIDE PAGE =================================

        .state('rideList', {
            url: '/ride',
            templateUrl: '/content/app/html/ride-list.html',
            controller: 'rideListController'
        })

        .state('rideAdd', {
            url: '/ride/add',
            templateUrl: '/content/app/html/ride-add.html',
            controller: 'rideAddController'
        })

        .state('rideEdit', {
            url: '/ride/:id/edit',
            templateUrl: '/content/app/html/ride-edit.html',
            controller: 'rideEditController'
        })

        // ROUTE PAGE =================================

        .state('routeList', {
            url: '/route',
            templateUrl: '/content/app/html/route-list.html',
            controller: 'routeListController'
        })

        .state('routeAdd', {
            url: '/route/:id/add',
            templateUrl: '/content/app/html/route-edit.html',
            controller: 'routeEditController'
        })

        .state('routeEdit', {
            url: '/route/:id/edit',
            templateUrl: '/content/app/html/route-edit.html',
            controller: 'routeEditController'
        })

        // RIDER PAGE =================================

        .state('riderList', {
            url: '/rider',
            templateUrl: '/content/app/html/rider-list.html',
            controller: 'riderListController'
        })

        .state('riderAdd', {
            url: '/rider/:id/add',
            templateUrl: '/content/app/html/rider-edit.html',
            controller: 'riderEditController'
        })

        .state('riderEdit', {
            url: '/rider/:id/edit',
            templateUrl: '/content/app/html/rider-edit.html',
            controller: 'riderEditController'
        })
    
        // BIKE PAGE =================================

        .state('bikeList', {
            url: '/bike',
            templateUrl: '/content/app/html/bike-list.html',
            controller: 'bikeListController'
        })

        .state('bikeAdd', {
            url: '/bike/:id/add',
            templateUrl: '/content/app/html/bike-edit.html',
            controller: 'bikeEditController'
        })
        .state('bikeEdit', {
            url: '/bike/:id/edit',
            templateUrl: '/content/app/html/bike-edit.html',
            controller: 'bikeEditController'
        });
});
