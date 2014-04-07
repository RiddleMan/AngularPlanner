'use strict';
/**
* register Module
*
* Module for user registration
*/
angular.module('register', ['auth']).config([
  '$routeProvider',
  function ($routeProvider) {
    $routeProvider.when('/register', {
      controller: 'RegisterCtrl',
      templateUrl: 'App/register/register.html'
    });
  }
]).controller('RegisterCtrl', [
  'auth',
  '$scope',
  function (auth, $scope) {
    $scope.user = {};
    $scope.register = function () {
      auth.register($scope.user.name, $scope.user.password, $scope.user.repassword).success(function (data) {
        console.log(data);
      }).error(function (data) {
        console.error(data);
      });
    };
  }
]);