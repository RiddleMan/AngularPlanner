'use strict';
/**
* resources.limits Module
*
* limits resource
*/
angular.module('resources.limits', []).factory('Limits', [
  '$resource',
  function ($resource) {
    return $resource('/api/limits/:id', { id: '@id' }, { 'update': { method: 'PUT' } });
  }
]);