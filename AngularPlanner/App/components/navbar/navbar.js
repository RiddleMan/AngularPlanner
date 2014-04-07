'use strict';
/**
* navbar Module
*
* Ctrl for navbar
*/
angular.module('navbar', ['auth']).controller('NavBarCtrl', [
  '$scope',
  'auth',
  '$location',
  function ($scope, auth, $location) {
    auth.isAuthenticated().then(function (userInfo) {
      $scope.user = userInfo;
      $scope.logout = function () {
        auth.logout();
        $location.path('/');
      };
    });
  }
]);