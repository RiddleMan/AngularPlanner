'use strict';
/**
* register Module
*
* Module for user registration
*/
angular.module('register', ['auth'])
  .config(function($routeProvider) {
    $routeProvider
      .when('/register', {
        controller: 'RegisterCtrl',
        templateUrl: 'App/register/register.html'
      });
  })
  .controller('RegisterCtrl', function(auth, $scope, $location){
    $scope.user = {};

    $scope.register = function() {
      auth
        .register($scope.user.name,
          $scope.user.password,
          $scope.user.repassword)
        .success(function() {
          $location.url('/login');
        })
        .error(function(data) {
          console.error(data);
        });
    };
  });
