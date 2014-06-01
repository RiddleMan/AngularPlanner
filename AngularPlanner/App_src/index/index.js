'use strict';
/**
* index Module
*
* index module
*/
angular.module('index', ['auth', 'resources'])
  // .config(function($routeProvider, authCheckerProvider) {
    //TODO: dodać jak bedzie strona główna
    // $routeProvider
    //   .when('/', {
    //     templateUrl: '/App/index/index.html',
    //     controller: 'IndexCtrl',
    //     resolve: {
    //       currentUser: authCheckerProvider.require
    //     }

    //   });
  // })
  .controller('IndexCtrl', function($scope, $route, Values, currentUser) {
    console.log(currentUser);
    Values.get().$promise.then(function(data) {
      console.log(data);
    });
  });
