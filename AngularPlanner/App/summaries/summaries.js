'use strict';
/**
* Summaries Module
*
* Module for Summaries
*/
angular.module('summaries', [
  'graphs',
  'resources',
  'auth'
]).config([
  '$routeProvider',
  'authCheckerProvider',
  function ($routeProvider, authCheckerProvider) {
    $routeProvider.when('/summaries', {
      templateUrl: '/App/Summaries/Summaries.html',
      controller: 'SummariesCtrl',
      resolve: {
        summaries: function (Summaries) {
          return Summaries.query().$promise;
        },
        currentUser: authCheckerProvider.require
      }
    });
  }
]).controller('SummariesCtrl', [
  '$scope',
  'summaries',
  function ($scope, summaries) {
    $scope.summaries = summaries.map(function (elem) {
      elem.from = new Date(elem.from);
      elem.to = new Date(elem.to);
      return elem;
    });
    $scope.$on('summary:delete', function (e, summary) {
      e.stopPropagation();
      var i = $scope.summaries.indexOf(summary);
      $scope.summaries.splice(i, 1);
    });
    $scope.$on('summary:push', function (e, summary) {
      e.stopPropagation();
      summary.from = new Date(summary.from);
      summary.to = new Date(summary.to);
      $scope.summaries.push(summary);
    });
  }
]).controller('SummariesAddCtrl', [
  '$scope',
  'Summaries',
  function ($scope, Summaries) {
    $scope.summaryScopes = [
      {
        name: 'Dziennie',
        value: 3
      },
      {
        name: 'Miesi\u0119cznie',
        value: 1
      },
      {
        name: 'Rocznie',
        value: 2
      }
    ];
    $scope.addicon = true;
    $scope.toggle = function () {
      $scope.addicon = !$scope.addicon;
    };
    $scope.add = function () {
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
      summary.$save(function (summary) {
        $scope.$emit('summary:push', summary);
      });
    };
  }
]);