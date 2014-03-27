'use strict';
/**
* tagsUsage Module
*
* Tags usage graph
*/
angular.module('graphs.tagsUsage', ['highcharts-ng'])
  .factory('TagsUsageData', ['$http', '$q', function($http, $q){
    var defer = $q.defer();

    $http.get('/api/tagsGraphs/usingStatistics')
      .success(function(data) {
        defer.resolve(data);
      })
      .error(function(data) {
        defer.reject(data);
      });

    return defer.promise;
  }])
  .controller('TagsUsageCtrl', ['$scope', 'TagsUsageData', function($scope, TagsUsageData){
    (function init() {
      //Graph options
      $scope.options = {
        options: {
          chart: {
            type: 'column',
            margin: [ 50, 50, 100, 80]
          },
          credits: {
            enabled: false
          },
          legend: {
            enabled: false
          },
          yAxis: {
            min: 0,
            title: {
              text: 'Ilośc wydatków'
            }
          }
        },
        loading: true,
        title: {
          text: 'Statystyki korzystania z tagowania'
        },
        xAxis: {
          categories: [],
          labels: {
            rotation: -45,
            align: 'right',
            style: {
              fontSize: '13px',
              fontFamily: 'Verdana, sans-serif'
            }
          }
        },
        series: [{
          name: 'Wykorzystanie',
          data: []
        }]
      };

      TagsUsageData.then(function(data) {
        $scope.options.loading = false;

        $scope.options.xAxis.categories = data.tags;
        $scope.options.series[0].data = data.usage;
      });
    })();
  }])
  .directive('tagsUsage', function(){
    return {
      scope: {},
      controller: 'TagsUsageCtrl',
      restrict: 'EA',
      templateUrl: '/App/components/graphs/tagsUsage/tagsUsage.html'
    };
  });
