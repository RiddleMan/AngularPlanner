'use strict';
/**
* login Module
*
* module for sign up
*/
angular.module('login', ['auth']).config([
  '$routeProvider',
  function ($routeProvider) {
    $routeProvider.when('/login', {
      templateUrl: '/App/login/login.html',
      controller: 'LoginCtrl'
    }).when('/login/:returnUrl', {
      templateUrl: '/App/login/login.html',
      controller: 'LoginCtrl'
    });
  }
]).controller('LoginCtrl', [
  '$scope',
  'auth',
  '$location',
  '$route',
  function ($scope, auth, $location, $route) {
    $scope.user = {};
    $scope.authError = false;
    $scope.login = function () {
      auth.login($scope.user.name, $scope.user.password).then(function () {
        if ($route.returnUrl) {
          $location.path(decodeURI($route.returnUrl));
        } else {
          $location.path('/');
        }
      }).catch(function () {
        $scope.authError = true;
      });
    };
  }
]);