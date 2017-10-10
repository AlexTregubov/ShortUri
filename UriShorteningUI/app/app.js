var app = angular.module('CustomUrlShortener', ['ngCookies','ui.router']);

app.config(function($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/');

        $stateProvider

            .state('home', {
                url: '/',
                templateUrl: 'home.html'
            })

            .state('history', {
                url: '/history',
                templateUrl: 'urihistory.html'
            })

            .state('go', {
                url: '/go',
                templateUrl: 'redirect.html'
            });
    });