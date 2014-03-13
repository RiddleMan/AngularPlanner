'use strict';

/**
* auth.service Module
*
* Service for logging etc
*/
angular.module('auth.service', [])
  .factory('auth', ['$http', '$q', '$window', function($http, $q, $window){
    function parseToken(token) {
      token.issued = new Date(token['.isued']);
      token.expire = new Date(token['.expire']);
    }

    var service = {
      register: function(username, password, password2) {
        if(!username || !password || !password2) {
          throw new Error('[App] Security: You must specify username and password');
        }

        return $http
          .post('/api/account/register', {
            userName:   username,
            password:   password,
            confirmPassword: password2
          });
      },
      login: function(username, password) {
        var defer = $q.defer();

        if(!username || !password) {
          throw new Error('[App] Security: You must specify username and password');
        }

        $http
          .post('/token', {
            'grant_type': 'password',
            username: username,
            password: password
          })
          .succes(function(token) {
            $window.sessionStorage.token = parseToken(token);
            defer.resolve();
          })
          .error(function(data) {
            defer.reject(data);
          });

        return defer.promise;
      },
      isAuthenticated: function() {
        if($window.sessionStorage.token &&
          ($window.sessionStorage.token.expire - new Date()) > 0) {
          return true;
        }

        return false;
      }
    };

    return service;
  }]);
