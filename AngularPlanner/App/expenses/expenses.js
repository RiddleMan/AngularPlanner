'use strict';
/**
* Expenses Module
*
* Module for expenses
*/
angular.module('expenses', ['auth', 'app', 'resources'])
  .config(['$routeProvider', 'authCheckerProvider', function($routeProvider, authCheckerProvider) {
    $routeProvider
      .when('/expenses/edit', {
        templateUrl: '/App/expenses/expenses-edit.html',
        controller: 'ExpensesEditCtrl',
        resolve: {
          currentUser: authCheckerProvider.require
        }
      })
      .when('/expenses', {
        templateUrl: '/App/expenses/expenses-list.html',
        controller: 'ExpensesListCtrl',
        resolve: {
          currentUser: authCheckerProvider.require,
          expenses: ['Expenses', function(Expenses) {
            return Expenses.query().$promise;
          }]
        }
      })
      .when('/expenses/list', {
        templateUrl: '/App/expenses/expenses-list.html',
        controller: 'ExpensesListCtrl',
        resolve: {
          currentUser: authCheckerProvider.require,
          expenses: ['Expenses', function(Expenses) {
            return Expenses.query().$promise;
          }]
        }
      });
  }])
  .controller('ExpensesListCtrl', ['$scope', 'expenses', '$route', 'currentUser',
    function($scope, expenses, $route, currentUser) {
      $scope.expenses = expenses;
    }]);
