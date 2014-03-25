'use strict';
/**
* statistics Module
*
* User stats
*/
angular.module('statistics', [])
  .config(['$routeProvider', function($routeProvider) {
    $routeProvider
      .when('/statistics', {
        templateUrl: '/App/statistics/statistics.html',
        controller: 'StatisticsCtrl'
        //resolve
      });
  }])
  .controller('StatisticsCtrl', ['$scope', function($scope){

  }]);
