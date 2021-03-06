'use strict';
/**
* http Module
*
* Description
*/
angular.module('http', [])
  .config(function($provide){
    $provide.decorator('$http', function($delegate) {
      function urlEncode(data) {
        var str = '';
        for(var key in data) {
          str += key + '=' + data[key] + '&';
        }
        return str.substr(0, str.length - 1);
      }

      $delegate.postUrlEncoded = function(url, data) {
        return $delegate({
          url: url,
          method: 'POST',
          headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
          },
          data: urlEncode(data)
        });
      };

      return $delegate;
    });
  });
