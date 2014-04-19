'use strict';
/**
* auth.interceptor Module
*
* Module for handling authentication errors
*/
angular.module('auth.interceptor', []).factory('authInterceptor', function($window, $location){
  return {
    'request': function(config) {
      config.headers = config.headers || {};
      if($window.localStorage.token) {
        config.headers.Authorization = 'Bearer ' + $window.localStorage.token;
      }
      return config;
    },
    'response': function(config) {
      if(config.status === 401) {
        $location.path('/login');
      }
      return config;
    }
  };
})
.config(function($httpProvider) {
  $httpProvider.interceptors.push('authInterceptor');
});
