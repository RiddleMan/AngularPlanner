'use strict';
/**
* graphs.incomesCostsMonthScope Module
*/
angular.module('graphs.incomesCostsMonthScope', [
  'highcharts-ng',
  'resources'
]).factory('IncomesCostsMonthScopeData', [
  '$http',
  '$q',
  function ($http, $q) {
    var defer = $q.defer();
    $http.get('/api/IncomesCostsMonthScopeGraph').success(function (data) {
      defer.resolve(data);
    }).error(function (data) {
      defer.reject(data);
    });
    return defer.promise;
  }
]).controller('ExpenseDetailsCtrl', [
  '$scope',
  'Expenses',
  function ($scope, Expenses) {
    $scope.$on('incomesCostsExpenseDetails:open', function (e, id) {
      angular.element('.incomesCostsExpenseDetails').modal('show');
      $scope.expense = {};
      $scope.loading = true;
      Expenses.get({ id: id }, function (expense) {
        $scope.loading = false;
        $scope.expense = expense;
      });
    });
  }
]).controller('IncomesCostsMonthScopeCtrl', [
  '$scope',
  'IncomesCostsMonthScopeData',
  '$location',
  function ($scope, IncomesCostsMonthScopeData, $location) {
    function openExpenses() {
      var id = this.id;
      $scope.$broadcast('incomesCostsExpenseDetails:open', id);
    }
    (function init() {
      $scope.options = {
        options: {
          yAxis: {
            title: { text: 'bilans (z\u0142)' },
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
              }]
          },
          tooltip: { valueSuffix: 'z\u0142' },
          legend: { enabled: false },
          subtitle: { text: 'ostatni miesi\u0105c' },
          credits: { enabled: false }
        },
        title: { text: 'Przychody/Wydatki' },
        xAxis: {
          labels: { enabled: false },
          categories: []
        },
        series: [],
        loading: true
      };
      IncomesCostsMonthScopeData.then(function (data) {
        $scope.options.xAxis.categories = data.titles;
        $scope.options.series.push({
          name: 'Bilans',
          data: data.costs.map(function (val) {
            return {
              y: val.costAfterOp,
              id: val.id,
              events: { click: openExpenses }
            };
          })
        });
        $scope.options.loading = false;
      });
    }());
  }
]).directive('incomesCostsMonthScope', function () {
  return {
    scope: {},
    controller: 'IncomesCostsMonthScopeCtrl',
    restrict: 'EA',
    templateUrl: '/App/components/graphs/incomesCostsMonthScope/incomesCostsMonthScope.html'
  };
});