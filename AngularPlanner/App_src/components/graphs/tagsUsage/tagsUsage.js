'use strict';
/**
* tagsUsage Module
*
* Tags usage graph
*/
angular.module('graphs.tagsUsage', ['highcharts-ng'])
  .factory('TagsUsageData', function($http, $q){
    return function() {
        var defer = $q.defer();

        $http.get('/api/tagsUsageGraph')
          .success(function(data) {
            defer.resolve(data);
          })
          .error(function(data) {
            defer.reject(data);
          });

        return defer.promise;
      };
  })
  .controller('TagsUsageCtrl', function($scope, TagsUsageData, $location){
    function openExpensesList () {
      var tag = this.name === 'Bez taga' ? 'notag' : this.name;
      $scope.$apply(function() {
        $location.path('/expenses/tag/' + tag);
      });
    }

    (function init() {
      //Graph options
      $scope.options = {
        options: {
          chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
          },
          title: {
            text: 'Statystyki korzystania z tagowania'
          },
          credits: {
            enabled: false
          },
          plotOptions: {
            pie: {
              allowPointSelect: true,
              cursor: 'pointer',
              dataLabels: {
                enabled: true,
                color: '#000000',
                connectorColor: '#000000',
                formatter: function() {
                  if(this.key && this.key.length >= 15) {
                    return this.key.substr(0, 12) + '...';
                  }
                  return this.key;
                }
              }
            }
          },
        },
        loading: true,
        series: [{
          type: 'pie',
          name: 'Tagowanie',
          data: []
        }]
      };

      TagsUsageData().then(function(data) {
        $scope.expensesList = false;
        $scope.options.loading = false;

        $scope.options.series[0].data = data.usage.map(function(val) {
          return {
            y: val,
            events: {
              click: openExpensesList
            }
          };
        });
        data.tags.forEach(function(tag, index) {
          $scope.options.series[0].data[index].name = tag;
        });
      });
    })();
  })
  .directive('tagsUsage', function(){
    return {
      scope: {},
      controller: 'TagsUsageCtrl',
      restrict: 'EA',
      templateUrl: '/App/components/graphs/tagsUsage/tagsUsage.html'
    };
  });
