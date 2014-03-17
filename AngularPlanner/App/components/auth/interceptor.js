'use strict';
/**
* auth.interceptor Module
*
* Module for handling authentication errors
*/
angular.module('auth.interceptor', []).factory('authInterceptor', ['$window', '$location', function($window, $location){
  return {
    'request': function(config) {
      config.headers = config.headers || {};
      if($window.localStorage.token) {
        var token = JSON.parse($window.localStorage.token);
        config.headers.Authorization = 'Bearer ' + token.access_token;
      }
      return config;
    },
    'responseError': function(config) {
      if(config.status === 401) {
        $location.path('/login');
      }
      return config;
    }
  };
}])
.config(['$httpProvider', function($httpProvider) {
  $httpProvider.interceptors.push('authInterceptor');
}]);
