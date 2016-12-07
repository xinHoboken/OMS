(function () {
    'use strict';

    angular
        .module('myModule')
        .service('AuthenticationService', Service);

    function Service($http, $localStorage) {
        var service = {};
        var tokenInfo;

        service.Login = Login;
        service.Logout = Logout;

        return service;
        
        function Login(username, password, callback) {
            console.log("auth service");
            var data = "grant_type=password&username=" + username + "&password=" + password;
            $http.post("http://10.10.10.20/TeamAwesomeAPI/Token", data, { headers: { "Content-Type": "application/x-www-form-urlencoded" } })
                .success(function (response) {
                    // login successful if there's a token in the response
                    console.log(response);

                    if (response.access_token) {
                        // store username and token in local storage to keep user logged in between page refreshes
                        $localStorage.currentUser = { username: username, token: response.access_token, UserTypeNo: null, UserTypeName: null, AR: null, PR: null };
                        console.log($localStorage.currentUser);
                        // add jwt token to auth header for all requests made by the $http service
                        $http.defaults.headers.common.Authorization = 'Bearer ' + response.access_token;

                        // execute callback with true to indicate successful login
                        callback(true);
                    } else {
                        // execute callback with false to indicate failed login
                        callback(false);
                    }
                })
                .error(function (error) {
                    callback(false);
                });
        }


        function Logout() {
            // remove user from local storage and clear http auth header
            delete $localStorage.currentUser;
            //$http.defaults.headers.common.Authorization = '';            
        }
    }
})();


//        var url = "http://10.10.10.20/TeamAwesome/api/logins/" + $scope.user.id + "/" + $scope.user.pass;
//        $http.get(url).success(function (response) {
//            if (response === null) {
//                alert("Login id or password is not correct.")
//                return;
//            }
//            $state.go('Summary');
//        });