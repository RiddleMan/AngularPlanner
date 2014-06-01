'use strict';
/**
* progressbar Module
*
* Description
*/
angular.module('progressbar', []).constant('NProgress', NProgress).factory('progressbarInterceptor', [
  'NProgress',
  function (NProgress) {
    return {
      'request': function (config) {
        NProgress.start();
        return config;
      },
      'response': function (config) {
        NProgress.done();
        return config;
      },
      'responseError': function (config) {
        NProgress.done();
        return config;
      }
    };
  }
]).config([
  '$httpProvider',
  function ($httpProvider) {
    $httpProvider.interceptors.push('progressbarInterceptor');
  }
]);