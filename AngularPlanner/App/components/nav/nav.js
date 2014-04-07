'use strict';
/**
* nav Module
*
* Site navigation module
*/
angular.module('nav', []).controller('NavCtrl', [
  '$scope',
  '$location',
  function ($scope, $location) {
    var path = $location.path();
    if (path.indexOf('expenses') !== -1) {
      $scope.expenses = true;
    } else if (path.indexOf('simulations') !== -1) {
      $scope.simulations = true;
    } else if (path.indexOf('statistics') !== -1) {
      $scope.statistics = true;
    } else if (path.indexOf('limits') !== -1) {
      $scope.limits = true;
    } else if (path.indexOf('summary') !== -1) {
      $scope.summary = true;
    } else if (path.indexOf('users') !== -1) {
      $scope.users = true;
    } else {
      $scope.start = true;
    }
  }
]);