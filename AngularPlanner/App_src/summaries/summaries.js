'use strict';
/**
* Summaries Module
*
* Module for Summaries
*/
angular.module('summaries', ['graphs', 'resources', 'auth'])
  .config(function($routeProvider, authCheckerProvider) {
    $routeProvider
      .when('/summaries', {
        templateUrl: '/App/Summaries/Summaries.html',
        controller: 'SummariesCtrl',
        resolve: {
          summaries: function(Summaries) {
            return Summaries.query().$promise;
          },
          currentUser: authCheckerProvider.require
        }
      });
  })
  .controller('SummariesCtrl', function($scope, summaries) {
    $scope.summaries = summaries;

    $scope.$on('summary:delete', function(e, summary) {
      e.stopPropagation();
      var i = $scope.summaries.indexOf(summary);
      $scope.summaries.splice(i, 1);
    });

    $scope.$on('summary:push', function(e, summary) {
      e.stopPropagation();
      $scope.summaries.push(summary);
    });

  })
  .controller('SummariesAddCtrl', function($scope, Summaries){
    $scope.summaryScopes = [
      {name: 'Dziennie', value: 3},
      {name: 'Miesięcznie', value: 1},
      {name: 'Rocznie', value: 2}
    ];
    $scope.addicon = true;
    $scope.toggle = function() {
      $scope.addicon = !$scope.addicon;
    };

    $scope.add = function() {
      var summary = new Summaries({
        name: $scope.summary.name,
        from: $scope.summary.from,
        to: $scope.summary.to,
        tags: $scope.summary.tags,
        type: 1,
        scope: $scope.summary.scope.value
      });

      $scope.addicon = true;
      delete $scope.summary;
      $scope.form.$setPristine(true);

      summary.$save(function(summary) {
        $scope.$emit('summary:push', summary);
      });
    };
  });
