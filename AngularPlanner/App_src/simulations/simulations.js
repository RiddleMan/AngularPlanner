'use strict';

/**
* simulations Module
*
* Module for simulations
*/
angular.module('simulations', ['highcharts-ng', 'auth'])
  .config(function($routeProvider, authCheckerProvider) {
    $routeProvider
      .when('/simulations', {
        templateUrl: '/App/simulations/simulations.html',
        controller: 'SimulationsCtrl',
        resolve: {
          simulation: function(SimulationsData) {
            return SimulationsData();
          },
          currentUser: authCheckerProvider.require
        }
      });
  })
  .factory('SimulationsData', function($http, $q){
    return function(scope) {
      if(!scope) {
        scope = 2;
      }

      var defer = $q.defer();

      $http({
          method: 'GET',
          url: '/api/simulations',
          params: {
            scope: scope
          }
        })
        .success(function(data) {
          defer.resolve(data);
        })
        .error(function(data) {
          defer.reject(data);
        });

      return defer.promise;
    };
  })
  .controller('SimulationsCtrl', function($scope, simulation, SimulationsData){
    $scope.scopes = [
      {name: 'Dzienny', id: 1},
      {name: 'Miesięczny', id: 2},
      {name: 'Roczny', id: 3}
    ];

    $scope.simulation = {
      scope: $scope.scopes[1]
    };

    $scope.$watch('simulation.income', function() {
      expectedIncome();
    });

    $scope.$watch('simulation.outcome', function() {
      expectedOutcome();
    });

    $scope.$watch('simulation.scope', function() {
      if($scope.simulation.scope)
        SimulationsData($scope.simulation.scope.id)
          .then(function(simulation) {
            refreshData(simulation);
          });
    });

    //init graph
    $scope.options = {
      options: {
        yAxis: {
          title: {
            text: 'bilans (zł)'
          },
          min: 0,
          plotLines: [{
            value: 0,
            width: 1,
            color: '#808080',
          }]
        },
        tooltip: {
          valueSuffix: 'zł'
        },
        subtitle: {
          text: 'ostatni miesiąc',
        },
        credits: {
          enabled: false
        }
      },
      title: {
        text: 'Przychody/Wydatki'
      },
      xAxis: {
        categories: []
      },
      series: [],
      loading: true
    };

    function expectedIncome() {
      if($scope.simulation.income) {
        var estimatedIncome = $scope.options.series[2].data;
        var pastIncomes = $scope.options.series[0].data;
        $scope.options.series[4].data = estimatedIncome.map(function() {
          return $scope.simulation.income;
        });
        $scope.options.series[6].data = pastIncomes.map(function(val) {
          return val + $scope.simulation.income;
        });
      }
    }

    function expectedOutcome() {
      if($scope.simulation.outcome) {
        var estimatedOutcome = $scope.options.series[2].data;
        var pastOutcomes = $scope.options.series[1].data;
        $scope.options.series[5].data = estimatedOutcome.map(function() {
          return $scope.simulation.Outcome;
        });
        $scope.options.series[7].data = pastOutcomes.map(function(val) {
          return val + $scope.simulation.outcome;
        });
      }
    }

    function refreshData(data) {
      if(data.estimatedIncome === 0 && data.estimatedOutcome === 0){
        $scope.noData = true;
        $scope.options.loading = false;
        return;
      }
      $scope.options.xAxis.categories = data.dates;
      $scope.options.series = [];
      $scope.options.series.push({
        name: 'Przychody',
        data: data.pastIncomes
      });
      $scope.options.series.push({
        name: 'Wydatki',
        data: data.pastOutcomes
      });
      var estimatedIncome = data.pastIncomes.map(function() {
        return data.estimatedIncome;
      });
      estimatedIncome.push(data.estimatedIncome);
      $scope.options.series.push({
        name: 'Wyestymowany zysk',
        data: estimatedIncome
      });

      var estimatedOutcome = data.pastOutcomes.map(function() {
        return data.estimatedOutcome;
      });
      estimatedOutcome.push(data.estimatedOutcome);
      $scope.options.series.push({
        name: 'Wyestymowany wydatek',
        data: estimatedOutcome
      });
      $scope.options.series.push({
        name: 'Przewidywany wydatek',
        data: []
      });
      $scope.options.series.push({
        name: 'Przewidywany przychód',
        data: []
      });

      $scope.options.series.push({
        name: 'Historycznie dodany wydatek',
        data: []
      });
      $scope.options.series.push({
        name: 'Historycznie dodany przychód',
        data: []
      });
      expectedOutcome();
      expectedIncome();
      $scope.options.loading = false;
    }

    (function init() {
      refreshData(simulation);
    })();
  });
