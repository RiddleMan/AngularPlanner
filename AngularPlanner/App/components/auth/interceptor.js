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
      if($window.sessionStorage.token) {
        config.headers.Authorization = 'Bearer ' + $window.sessionStorage.token;
      }
      return config;
    },
    'response': function(config) {
      if(config.status === 401) {
        $location.path('/login/' + encodeURI($location.path()));
      }
      return config;
    }
  };
}])
.config(['$httpProvider', function($httpProvider) {
  $httpProvider.interceptors.push('authInterceptor');
}]);
