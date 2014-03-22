'use strict';
/**
* tagsPicker Module
*
* Pick tags
*/
angular.module('tagsPicker', ['resources'])
  .controller('TagsPickerCtrl', ['$scope', 'Tags', function($scope, Tags){
    $scope.checked = [];
    $scope.tagsList = [];

    function updateChecked() {
      if(!$scope.tags) {
        $scope.tags = [];
      }
      $scope.checked = [];
      $scope.tagsList.forEach(function(tag) {
        var founded = false;
        $scope.tags.forEach(function(selectedTag) {
          if(selectedTag.id === tag.id) {
            founded = true;
          }
        });

        $scope.checked.push(founded);
      });
    }

    $scope.delete = function($index) {
      var tmp = $scope.tagsList[$index];
      $scope.tagsList.splice($index, 1);
      tmp.delete();
    };

    $scope.toggle = function($index) {
      var selectedTag = $scope.tagsList[$index],
          indexOf = -1;

      $scope.tags.forEach(function(tag, index) {
        if(tag.id === selectedTag.id) {
          indexOf = index;
        }
      });

      if(indexOf !== -1) {
        $scope.tags.splice(indexOf, 1);
      } else {
        $scope.tags.push(selectedTag);
      }
    };

    $scope.$watchCollection('tags', updateChecked);

    (function() {
      Tags.query(function(tags) {
        $scope.tagsList = tags;
        updateChecked();
      });
    })();
  }])
  .directive('tagsPicker', function(){
    return {
      scope: {
        tags: '=',
      },
      controller: 'TagsPickerCtrl',
      restrict: 'EA',
      templateUrl: '/App/components/tagsPicker/tagsPicker.html',
      replace: true,
      link: function($scope, iElm, iAttrs, controller) {
      }
    };
  });
