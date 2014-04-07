'use strict';
/**
* resources.tags Module
*
* tags resource
*/
angular.module('resources.tags', [])
  .factory('Tags', function($resource){
    return $resource('/api/tags/:id', {id: '@id'},
      {
        'update': { method: 'PUT'}
      });
  });
