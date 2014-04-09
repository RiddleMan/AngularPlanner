'use strict';
/**
* app Module
*
* init app module
*/
angular.module('app', [
  'ngSanitize',
  'ngResource',
  'ngCookies',
  'ngRoute',
  'http',
  'login',
  'register',
  'index',
  'expenses',
  'navbar',
  'nav',
  'simulations',
  'statistics',
  'limits',
  'progressbar',
  'connectionChecker',
  'summaries'
]);
angular.module('app').config([
  '$routeProvider',
  '$locationProvider',
  function ($routeProvider, $locationProvider) {
    $locationProvider.html5Mode(true);
    $routeProvider.otherwise({ redirectTo: '/statistics' });
  }
]);