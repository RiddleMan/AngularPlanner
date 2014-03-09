'use strict';

/**
* app Module
*
* init app module
*/
angular.module('app', ['ngSanitize', 'ngResource', 'ngCookies', 'ngRoute'])
  .config(['$routeProvider',function($routeProvider) {
    $routeProvider
      .when('');
  }]);
