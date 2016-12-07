var ChangePassController = function ($scope, $location, $state, $http, $localStorage) {
    //Here I used specific loginId
    var loginID = $localStorage.currentUser.username;

    $scope.vm = {};
    $scope.submitted = false;
    $scope.vm.loginID = loginID;
    $scope.vm.update = "";
    $scope.vm.submit = function () {
        console.log($scope.vm.Password);
        var url = "http://10.10.10.20/TeamAwesomeAPI/api/Logins/" + loginID;
        $http.put(url, $scope.vm).success(function (response) { 
            console.log("update password successfully!");
            $scope.vm.update = "Update password successfully!";
            $scope.vm.Password = null;
            $scope.vm.confirmPassword = null;
            $scope.submitted = true; //use to clear input without activating validation
            $state.go("Login");
        });
    };

}

