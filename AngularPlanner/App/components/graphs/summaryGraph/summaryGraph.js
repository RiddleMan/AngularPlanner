'use strict';
/**
* graphs.incomesCosts Module
*/
angular.module('graphs.summary', ['highcharts-ng']).factory('SummariesData', [
  '$http',
  '$q',
  function ($http, $q) {
    return function (id) {
      var defer = $q.defer();
      $http.get('/api/summariesGraph/' + id).success(function (data) {
        defer.resolve(data);
      }).error(function (data) {
        defer.reject(data);
      });
      return defer.promise;
    };
  }
]).controller('SummaryGraphCtrl', [
  '$scope',
  'SummariesData',
  '$location',
  function ($scope, SummariesData, $location) {
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
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
              }]
          },
          tooltip: { valueSuffix: 'z\u0142' },
          legend: { enabled: false },
          credits: { enabled: false }
        },
        title: { text: $scope.summary.name },
        xAxis: { categories: [] },
        series: [],
        loading: true
      };
      SummariesData($scope.summary.id).then(function (data) {
        $scope.options.xAxis.categories = data.series;
        $scope.options.series.push({
          data: data.values.map(function (val) {
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
]).directive('summaryGraph', function () {
  return {
    scope: { summary: '=' },
    controller: 'SummaryGraphCtrl',
    restrict: 'EA',
    templateUrl: '/App/components/graphs/summaryGraph/summaryGraph.html'
  };
});