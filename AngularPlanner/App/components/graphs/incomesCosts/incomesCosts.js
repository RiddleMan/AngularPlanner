'use strict';
/**
* graphs.incomesCosts Module
*/
angular.module('graphs.incomesCosts', ['highcharts-ng']).factory('IncomesCostsData', [
  '$http',
  '$q',
  function ($http, $q) {
    return function () {
      var defer = $q.defer();
      $http.get('/api/IncomesCostsGraph').success(function (data) {
        defer.resolve(data);
      }).error(function (data) {
        defer.reject(data);
      });
      return defer.promise;
    };
  }
]).controller('IncomesCostsCtrl', [
  '$scope',
  'IncomesCostsData',
  '$location',
  function ($scope, IncomesCostsData, $location) {
    function openExpenses() {
      var date = this.category;
      $scope.$apply(function () {
        $location.path('/expenses/date/' + date);
      });
    }
    (function init() {
      $scope.options = {
        options: {
          yAxis: {
            title: { text: 'kwota (z\u0142)' },
            min: 0,
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
              }]
          },
          tooltip: { valueSuffix: 'z\u0142' },
          legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle',
            borderWidth: 0
          },
          subtitle: {
            text: 'ostatni rok',
            x: -20
          },
          credits: { enabled: false }
        },
        title: {
          text: 'Przychody/Wydatki',
          x: -20
        },
        xAxis: { categories: [] },
        series: [],
        loading: true
      };
      IncomesCostsData().then(function (data) {
        $scope.options.xAxis.categories = data.dates;
        $scope.options.series.push({
          name: 'Wydatki',
          data: data.costs.map(function (val) {
            return {
              y: val,
              events: { click: openExpenses }
            };
          })
        });
        $scope.options.series.push({
          name: 'Przychody',
          data: data.incomes.map(function (val) {
            return {
              y: val,
              events: { click: openExpenses }
            };
          })
        });
        $scope.options.loading = false;
      });
    }());
  }
]).directive('incomesCosts', function () {
  return {
    scope: {},
    controller: 'IncomesCostsCtrl',
    restrict: 'EA',
    templateUrl: '/App/components/graphs/incomesCosts/incomesCosts.html'
  };
});