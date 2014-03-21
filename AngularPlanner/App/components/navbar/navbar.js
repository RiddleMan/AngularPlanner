'use strict';
/**
* navbar Module
*
* Ctrl for navbar
*/
angular.module('navbar', ['auth'])
  .controller('NavBarCtrl', ['$scope','auth', function($scope, auth){
    auth.isAuthenticated().then(function(userInfo) {
      $scope.user = userInfo;
    });
  }]);
