'use strict';
/**
* resources.summaries Module
*
* ExpenseModel
*/
angular.module('resources.summaries', []).factory('Summaries', [
  '$resource',
  function ($resource) {
    return $resource('/api/summaries/:id', { id: '@id' }, {
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