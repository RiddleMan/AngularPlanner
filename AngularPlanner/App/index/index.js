'use strict';
/**
* index Module
*
* index module
*/
angular.module('index', ['auth', 'app', 'resources'])
  .config(['$routeProvider',function($routeProvider) {
    $routeProvider
      .when('/', {
        templateUrl: '/App/index/index.html',
        controller: 'IndexCtrl'
      });
  }])
  .controller('IndexCtrl', ['$scope', '$route', 'Values', function($scope, $route, Values) {
    Values.get().$promise.then(function(data) {
      console.log(data);
    });
  }]);
