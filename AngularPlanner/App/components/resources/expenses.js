'use strict';
/**
* resources.expenses Module
*
* ExpenseModel
*/
angular.module('resources.expenses', []).factory('Expenses', [
  '$resource',
  function ($resource) {
    return $resource('/api/expenses/:id', { id: '@id' }, { 'update': { method: 'PUT' } });
  }
]);