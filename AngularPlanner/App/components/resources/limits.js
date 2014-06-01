'use strict';
/**
* resources.limits Module
*
* limits resource
*/
angular.module('resources.limits', []).factory('Limits', [
  '$resource',
  function ($resource) {
    return $resource('/api/limits/:id', { id: '@id' }, {
      'get': {
        method: 'GET',
        cache: false
      },
      'save': {
        method: 'POST',
        cache: false
      },
      'query': {
        method: 'GET',
        isArray: true,
        cache: false
      },
      'remove': {
        method: 'DELETE',
        cache: false
      },
      'delete': {
        method: 'DELETE',
        cache: false
      },
      'update': {
        method: 'PUT',
        cache: false
      }
    });
  }
]);