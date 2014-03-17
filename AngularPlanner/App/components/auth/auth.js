'use strict';

/**
* auth.service Module
*
* Service for logging etc
*/
angular.module('auth.service', [])
  .factory('auth', ['$http', '$q', '$window', function($http, $q, $window){
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

        $http.postUrlEncoded('/token', {
            'grant_type': 'password',
            username: username,
            password: password
          })
          .success(function(token) {
            $window.localStorage.token = token.access_token;
            defer.resolve();
          })
          .error(function(data) {
            defer.reject(data);
          });

        return defer.promise;
      },
      isAuthenticated: function() {
        var defer = $q.defer();

        $http
          .get('api/account/userInfo')
          .success(function(userInfo) {
            defer.resolve(userInfo);
          })
          .error(function(err, status) {
            console.log('[App] Security (' + status + '): ' + err);
            defer.reject(err);
          });

        return defer.promise;
      }
    };

    return service;
  }]);
