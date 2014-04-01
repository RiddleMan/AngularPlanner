'use strict';
/**
* limits Module
*
* limits
*/
angular.module('limits', ['ngRoute', 'auth', 'tagsPicker', 'resources'])
  .config(['$routeProvider', 'authCheckerProvider', function($routeProvider, authCheckerProvider) {
    $routeProvider
      .when('/limits', {
        templateUrl: '/App/limits/limits.html',
        controller: 'LimitsCtrl',
        resolve: {
          currentUser: authCheckerProvider.require,
          limits: ['Limits', function(Limits) {
            return Limits.query().$promise;
          }]
        }
      });
  }])
  .controller('LimitsAddCtrl', ['$scope', 'Limits', function($scope, Limits){
    $scope.addicon = true;
    $scope.toggle = function() {
      $scope.addicon = !$scope.addicon;
    };

    $scope.add = function() {
      var limit = new Limits({
        name: $scope.limit.name,
        from: $scope.limit.from,
        to: $scope.limit.to,
        tags: $scope.limit.tags,
        amount: $scope.limit.amount
      });

      $scope.addicon = true;
      delete $scope.limit;
      $scope.form.$setPristine(true);

      limit.$save(function(limit) {
        $scope.$emit('limits:push', limit);
      });
    };
  }])
  .controller('LimitsCtrl', ['$scope', 'limits', function($scope, limits){
    $scope.$on('limit:delete', function(e, limit) {
      e.stopPropagation();
      var i = $scope.limits.indexOf(limit);
      $scope.limits.splice(i, 1);
    });

    $scope.$on('limits:push', function(e, limit) {
      e.stopPropagation();
      $scope.limits.push(limit);
    });

    limits = limits.map(function(limit) {
      limit.from = new Date(limit.from.toString());
      limit.to = new Date(limit.to.toString());
      return limit;
    });

    $scope.limits = limits;
  }]);
