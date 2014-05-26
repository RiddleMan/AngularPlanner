'use strict';
/**
* resources.expenses Module
*
* ExpenseModel
*/
angular.module('resources.expenses', [])
  .factory('Expenses', function($resource) {
    return $resource('/api/expenses/:id',
      {id: '@id'},
      { 'get':    {method:'GET' , cache: false},
        'save':   {method:'POST' , cache: false},
        'query':  {method:'GET', isArray:true , cache: false},
        'remove': {method:'DELETE' , cache: false},
        'delete': {method:'DELETE' , cache: false},
        'update': { method: 'PUT' , cache: false}
    });
  });
