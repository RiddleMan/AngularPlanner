'use strict';
/**
* resources.summaries Module
*
* ExpenseModel
*/
angular.module('resources.summaries', [])
  .factory('Summaries', function($resource) {
    return $resource('/api/summaries/:id',
      {id: '@id'},
      {
        'update': { method: 'PUT' }
      });
  });
