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
      .when('/expenses/:page', {
        templateUrl: '/App/expenses/expenses-list.html',
        controller: 'ExpensesListCtrl',
        resolve: {
          currentUser: authCheckerProvider.require,
          expenses: ['Expenses', '$route', function(Expenses, $route) {
            return Expenses.query({page: $route.current.params.page}).$promise;
          }]
        }
      })
      .when('/expenses/tag/:tag', {
        templateUrl: '/App/expenses/expenses-list.html',
        controller: 'ExpensesListCtrl',
        resolve: {
          currentUser: authCheckerProvider.require,
          expenses: ['Expenses', '$route', function(Expenses, $route) {
            return Expenses.query({
              tag: $route.current.params.tag,
              page: $route.current.params.page
            }).$promise;
          }]
        }
      })
      .when('/expenses/tag/:tag/:page', {
        templateUrl: '/App/expenses/expenses-list.html',
        controller: 'ExpensesListCtrl',
        resolve: {
          currentUser: authCheckerProvider.require,
          expenses: ['Expenses', '$route', function(Expenses, $route) {
            return Expenses.query({
              tag: $route.current.params.tag,
              page: $route.current.params.page
            }).$promise;
          }]
        }
      })
      .when('/expenses/date/:date', {
        templateUrl: '/App/expenses/expenses-list.html',
        controller: 'ExpensesListCtrl',
        resolve: {
          currentUser: authCheckerProvider.require,
          expenses: ['Expenses', '$route', function(Expenses, $route) {
            return Expenses.query({
              date: $route.current.params.date,
              page: $route.current.params.page
            }).$promise;
          }]
        }
      })
      .when('/expenses/date/:date/:page', {
        templateUrl: '/App/expenses/expenses-list.html',
        controller: 'ExpensesListCtrl',
        resolve: {
          currentUser: authCheckerProvider.require,
          expenses: ['Expenses', '$route', function(Expenses, $route) {
            return Expenses.query({
              date: $route.current.params.date,
              page: $route.current.params.page
            }).$promise;
          }]
        }
      });
  }])
  .controller('ExpensesListCtrl', ['$scope', 'expenses', 'Expenses', '$route', '$location',
    function($scope, expenses, Expenses, $route, $location) {
      if(!$route.current.params.page) {
        $route.current.params.page = 1;
      }

      $scope.expenses = expenses;

      $scope.page = $route.current.params.page;

      $scope.$on('expenses-invalidate', function(e) {
        e.stopPropagation();
        $location.path('/expenses');
      });

      $scope.$on('expenses:editor:close', function() {
        $scope.editor = false;
      });

      $scope.edit = function($index) {
        $scope.editor = true;
        $scope.editorIndex = $index;
        $scope.$broadcast('expenses:editor:open', $scope.expenses[$index]);
      };

      $scope.delete = function($index) {
        var tmp = $scope.expenses[$index];
        $scope.expenses.splice($index, 1);
        tmp.$delete();
      };

      $scope.next = function() {
        $location.path('/expenses/' + (parseInt($route.current.params.page) + 1));
      };

      $scope.prev = function() {
        $location.path('/expenses/' + (parseInt($route.current.params.page) - 1));
      };
    }])
  .controller('ExpensesListEditCtrl', ['$scope', function($scope){
    $scope.$on('expenses:editor:open', function(e, expense) {
      $scope.expense = expense;
    });

    $scope.save = function() {
      $scope.expense.$update();
    };

    $scope.close = function() {
      $scope.$emit('expenses:editor:close');
    };
  }])
  .controller('ExpenseAddCtrl', ['$scope', 'Expenses', '$rootScope', function($scope, Expenses){
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
        $scope.$emit('expenses-invalidate');
      });
    };
  }]);
