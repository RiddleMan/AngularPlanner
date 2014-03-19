'use strict';
/**
* Expenses Module
*
* Module for expenses
*/
angular.module('expenses', ['auth', 'app', 'resources'])
  .config(['$routeProvider', 'authCheckerProvider', function($routeProvider, authCheckerProvider) {
    $routeProvider
      .when('/expenses/add', {
        templateUrl: '/App/expenses/expenses-add.html',
        controller: 'ExpensesAddCtrl',
        resolve: {
          currentUser: authCheckerProvider.require
        }
      });
  }])
  .controller('ExpensesAddCtrl', ['$scope', 'Expenses', '$route', 'currentUser', function($scope, Expenses, $route, currentUser) {
    Expenses.query(function(list) {
      console.log(list);
    });
  }]);
