/**
* tagsGraph Module
*
* Description
*/
angular.module('resources.tagsGraph', [])
  .factory('TagsGraph', ['$resource', function($resource){
    return $resource('/api/tagsGraph', {id: '@id'});
  }])
