'use strict';
/**
* values Module
*
* Test for authentication
*/

//TODO: wypierdolić
angular.module('resources.values', ['auth.interceptor'])
  .factory('Values', ['$resource', 'authInterceptor', function($resource, authInterceptor){
    return $resource('/api/values/:id', null);
  }]);
