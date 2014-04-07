'use strict';
/**
* auth.checker Module
*
* promises for $routeProvider
*/
angular.module('auth.checker', ['auth.service']).provider('authChecker', {
  require: [
    'authChecker',
    function (authChecker) {
      return authChecker.require();
    }
  ],
  $get: [
    'auth',
    function (auth) {
      var service = {
          require: function () {
            return auth.isAuthenticated();
          }
        };
      return service;
    }
  ]
});