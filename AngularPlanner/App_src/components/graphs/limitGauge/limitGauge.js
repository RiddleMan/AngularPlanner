'use strict';
/**
* limitGauge Module
*
* Limit gauge
*/
angular.module('graphs.limitGauge', ['highcharts-ng'])
  .factory('LimitGaugeData', function($http, $q){
    return function(id) {
      var defer = $q.defer();

      $http.get('/api/limitsGraph/' + id)
        .success(function(data) {
          defer.resolve(data);
        })
        .error(function(data) {
          defer.reject(data);
        });

      return defer.promise;
    };
  })
  .controller('LimitGaugeCtrl', function($scope, LimitGaugeData, $filter){
    function buildSubtitle(from, to) {
      return 'Od: ' + $filter('date')(from) +
          ' Do: ' + $filter('date')(to);
    }

    $scope.options = {
      options: {
        chart: {
          type: 'gauge',
          plotBackgroundColor: null,
          plotBackgroundImage: null,
          plotBorderWidth: 0,
          plotShadow: false
        },
        credits: {
          enabled: false
        },
        pane: {
          startAngle: -150,
          endAngle: 150,
          background: [{
            backgroundColor: {
              linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
              stops: [
                [0, '#FFF'],
                [1, '#333']
              ]
            },
            borderWidth: 0,
            outerRadius: '109%'
          }, {
            backgroundColor: {
              linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
              stops: [
                [0, '#333'],
                [1, '#FFF']
              ]
            },
            borderWidth: 1,
            outerRadius: '107%'
          }, {
          }, {
            backgroundColor: '#DDD',
            borderWidth: 0,
            outerRadius: '105%',
            innerRadius: '103%'
          }]
        },
        yAxis: {
          min: 0,
          max: $scope.limit.amount*1.2,

          minorTickInterval: 'auto',
          minorTickWidth: 1,
          minorTickLength: 10,
          minorTickPosition: 'inside',
          minorTickColor: '#666',

          tickPixelInterval: 30,
          tickWidth: 2,
          tickPosition: 'inside',
          tickLength: 10,
          tickColor: '#666',
          labels: {
            step: 2,
            rotation: 'auto'
          },
          plotBands: [{
            from: 0,
            to: $scope.limit.amount*0.8,
            color: '#55BF3B' // green
          }, {
            from: $scope.limit.amount*0.8,
            to: $scope.limit.amount,
            color: '#DDDF0D' // yellow
          }, {
            from: $scope.limit.amount,
            to: $scope.limit.amount*1.2,
            color: '#DF5353' // red
          }]
        }
      },
      title: {
        text: $scope.limit.name
      },
      subtitle: {
        text: buildSubtitle($scope.limit.from, $scope.limit.to)
      },
      loading: true,
      series: [{
        name: 'Wartość',
        data: [],
        tooltip: {
          valueSuffix: ' zł'
        }
      }]
    };

    $scope.delete = function() {
      $scope.$emit('limit:delete', $scope.limit);
      $scope.limit.$delete();
    };

    $scope.editToggle = function() {
      $scope.editShow = !$scope.editShow;
    };

    $scope.save = function() {
      $scope.options.title = $scope.limit.name;
      $scope.options.subtitle = buildSubtitle($scope.limit.form, $scope.limit.to);
      $scope.limit.$update();
    };

    (function refresh() {
      new LimitGaugeData($scope.limit.id).then(function (data) {
        $scope.options.series[0].data[0] = data.value;
        $scope.options.loading = false;
      });
    })();
  })
  .directive('limitGauge', function(){
    return {
      scope: {
        limit: '='
      },
      controller: 'LimitGaugeCtrl',
      restrict: 'EA',
      templateUrl: '/App/components/graphs/limitGauge/limitGauge.html'
    };
  });
