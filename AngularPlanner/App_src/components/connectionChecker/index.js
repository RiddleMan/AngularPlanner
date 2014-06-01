'use strict';
/**
* connectionChecker Module
*
* Check if connection exists.
*/
angular.module('connectionChecker', [])
  .factory('connectionCheckerInterceptor', function(){
    return {
      'responseError': function(config) {
        if(config.status === 404) {
          angular.element('#connectionChecker').modal('show');
        }
        return config;
      }
    };
  })
  .config(function($httpProvider) {
    $httpProvider.interceptors.push('connectionCheckerInterceptor');
  });
