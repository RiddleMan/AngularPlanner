'use strict';
/**
* resources.tags Module
*
* tags resource
*/
angular.module('resources.tags', [])
  .factory('Tags', function($resource){
    return $resource('/api/tags/:id',
      {id: '@id'},
      { 'get':    {method:'GET' , cache: false},
        'save':   {method:'POST' , cache: false},
        'query':  {method:'GET', isArray:true , cache: false},
        'remove': {method:'DELETE' , cache: false},
        'delete': {method:'DELETE' , cache: false},
        'update': { method: 'PUT' , cache: false}
    });
  });
