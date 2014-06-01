'use strict';
/**
* index Module
*
* index module
*/
angular.module('index', [
  'auth',
  'resources'
]).controller('IndexCtrl', [
  '$scope',
  '$route',
  'Values',
  'currentUser',
  function ($scope, $route, Values, currentUser) {
    console.log(currentUser);
    Values.get().$promise.then(function (data) {
      console.log(data);
    });
  }
]);