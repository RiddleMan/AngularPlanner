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
  'register'
]);

angular.module('app')
  .config(['$routeProvider', '$locationProvider',
    function($routeProvider, $locationProvider) {
      $locationProvider.html5Mode(true);

      $routeProvider
        .otherwise({redirectTo: '/'});
    }]);

angular.module('app')
  .constant('TEMPLATE_PREFIX', '/App');
