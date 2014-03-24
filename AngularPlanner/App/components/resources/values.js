'use strict';
/**
* values Module
*
* Test for authentication
*/

//TODO: wypierdolić
angular.module('resources.values', [])
  .factory('Values', ['$resource', function($resource){
    return $resource('/api/values/:id', null);
  }]);
