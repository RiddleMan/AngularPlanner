'use strict';
/**
* statistics Module
*
* User stats
*/
angular.module('statistics', ['graphs', 'auth'])
  .config(['$routeProvider' , 'authCheckerProvider', function($routeProvider, authCheckerProvider) {
    $routeProvider
      .when('/statistics', {
        templateUrl: '/App/statistics/statistics.html',
        controller: 'StatisticsCtrl',
        resolve: {
          currentUser: authCheckerProvider.require
        }
      });
  }])
  .controller('StatisticsCtrl', function(){
});
