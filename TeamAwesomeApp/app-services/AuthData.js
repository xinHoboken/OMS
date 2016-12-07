'use strict';
angular.module('myModule').factory('authData', [function () {
    var authDataFactory = {};//Object for AuthData

    var _authentication = {
        IsAuthenticated: false,
        userName: ""
    };
    authDataFactory.authenticationData = _authentication;

    return authDataFactory;
}]);
