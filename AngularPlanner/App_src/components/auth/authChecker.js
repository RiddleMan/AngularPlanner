'use strict';
/**
* auth.checker Module
*
* promises for $routeProvider
*/
angular.module('auth.checker', ['auth.service'])
  .provider('authChecker', {
    require: function(authChecker) {
      return authChecker.require();
    },
    $get: function(auth) {
      var service = {
        require: function() {
          return auth.isAuthenticated();
        }
      };

      return service;
    }
  });
