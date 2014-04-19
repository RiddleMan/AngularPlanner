'use strict';
/**
* graphs.incomesCosts Module
*/
angular.module('graphs.summary', ['highcharts-ng'])
  .factory('SummariesData', function($http, $q){
    return function(id) {
      var defer = $q.defer();

      $http.get('/api/summariesGraph/' + id)
        .success(function(data) {
          defer.resolve(data);
        })
        .error(function(data) {
          defer.reject(data);
        });

      return defer.promise;
    };
  })
  .controller('SummaryGraphCtrl', function($scope, SummariesData, $location) {
    function openExpenses() {
      var date = this.category;
      $scope.$apply(function() {
        $location.path('/expenses/date/' + date);
      });
    }

    $scope.summaryScopes = [
      {name: 'Dziennie', value: 3},
      {name: 'Miesięcznie', value: 1},
      {name: 'Rocznie', value: 2}
    ];

    function refresh() {
      $scope.options.loading = true;

      SummariesData($scope.summary.id).then(function(data) {
        $scope.options.title.text = $scope.summary.name;
        $scope.options.xAxis.categories = data.series;
        $scope.options.series = [{
          data: data.values.map(function(val) {
            return {
              y: val,
              events: {
                click: openExpenses
              }
            };
          })
        }];

        $scope.options.loading = false;
      });
    }

    $scope.delete = function() {
      $scope.$emit('summary:delete', $scope.summary);
      $scope.summary.$delete();
    };

    $scope.editToggle = function() {
      $scope.editShow = !$scope.editShow;
    };

    $scope.save = function() {
      $scope.summary.scope = $scope.summary.scopeHidden.value;
      $scope.summary.$update(function() {
        refresh();
      });
    };


    (function init() {
      $scope.options = {
        options: {
          yAxis: {
            title: {
              text: 'kwota (zł)'
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
          credits: {
            enabled: false
          }
        },
        title: {
          text: ''
        },
        xAxis: {
          categories: []
        },
        series: [],
        loading: true
      };

      refresh();
    })();
  })
  .directive('summaryGraph', function(){
    return {
      scope: {
        summary: '='
      },
      controller: 'SummaryGraphCtrl',
      restrict: 'EA',
      templateUrl: '/App/components/graphs/summaryGraph/summaryGraph.html'
    };
  });
