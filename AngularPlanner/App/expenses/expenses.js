'use strict';
/**
* Expenses Module
*
* Module for expenses
*/
angular.module('expenses', ['auth', 'app', 'resources', 'tagsPicker'])
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
  .controller('ExpensesListCtrl', ['$scope', 'expenses', 'Expenses', '$route', 'currentUser',
    function($scope, expenses, Expenses, $route, currentUser) {
      $scope.$on('expenses-invalidate', function() {
        Expenses.query(function(expenses) {
          $scope.expenses = expenses;
        });
      });

      $scope.expenses = expenses;
    }])
  .controller('ExpenseAddCtrl', ['$scope', 'Expenses', '$rootScope', function($scope, Expenses, $rootScope){
    $scope.more = false;
    $scope.tagsList = [{id:1}, {id:2}];

    $scope.add = function() {
      var expense = new Expenses({
        dateOfExpense: $scope.expense.date,
        title: $scope.expense.title,
        cost: $scope.expense.cost,
        comment: $scope.expense.comment,
        tags: $scope.expense.tags
      });

      delete $scope.expense;

      $scope.form.$setPristine(true);

      expense.$save(function() {
        $rootScope.$broadcast('expenses-invalidate');
      });
    };
  }]);
