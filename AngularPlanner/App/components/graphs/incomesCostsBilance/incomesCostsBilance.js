'use strict';
/**
* graphs.incomesCosts Module
*/
angular.module('graphs.incomesCostsBilance', ['highcharts-ng'])
  .factory('IncomesCostsBilanceData', ['$http', '$q', function($http, $q){
    var defer = $q.defer();

    $http.get('/api/incomesCostsBilanceGraph')
      .success(function(data) {
        defer.resolve(data);
      })
      .error(function(data) {
        defer.reject(data);
      });

    return defer.promise;
  }])
  .controller('incomesCostsBilanceCtrl', ['$scope', 'IncomesCostsBilanceData', '$location', function($scope, IncomesCostsBilanceData, $location) {
    function openExpenses() {
      var date = this.category;
      $scope.$apply(function() {
        $location.path('/expenses/date/' + date);
      });
    }

    (function init() {
      $scope.options = {
        options: {
          yAxis: {
            title: {
              text: 'bilans (zł)'
            },
            plotLines: [{
              value: 0,
              width: 1,
              color: '#808080',
            }]
          },
          tooltip: {
            valueSuffix: 'zł'
          },
          legend: {
            enabled: false
          },
          subtitle: {
            text: 'ostatni rok'
          },
          credits: {
            enabled: false
          }
        },
        title: {
          text: 'Bilans przychodów i wydatków'
        },
        xAxis: {
          categories: []
        },
        series: [],
        loading: true
      };

      IncomesCostsBilanceData.then(function(data) {
        $scope.options.xAxis.categories = data.dates;
        $scope.options.series.push({
          name: 'Bilans',
          data: data.bilances.map(function(val) {
            return {
              y: val,
              events: {
                click: openExpenses
              }
            };
          })
        });

        $scope.options.loading = false;
      });
    })();
  }])
  .directive('incomesCostsBilance', function(){
    return {
      scope: {},
      controller: 'incomesCostsBilanceCtrl',
      restrict: 'EA',
      templateUrl: '/App/components/graphs/incomesCostsBilance/incomesCostsBilance.html'
    };
  });
