'use strict';
/**
* resources.limits Module
*
* limits resource
*/
angular.module('resources.limits', [])
  .factory('Limits', function($resource){
    return $resource('/api/limits/:id',
      {id: '@id'},
      {
        'update': {method: 'PUT'}
      });
  });
