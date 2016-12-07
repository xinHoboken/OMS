
var LoginController = function ($scope, $location, AuthenticationService, $state) {
    initController();
    function initController() {
        // reset login status
        AuthenticationService.Logout();
    };

    $scope.vm = {};                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
    $scope.vm.login = function () {
        $scope.vm.loading = true;
        console.log("enter login");
        AuthenticationService.Login($scope.vm.username, $scope.vm.password, function (result) {
            console.log("back to login");
            if (result === true) {
                $scope.vm.error = "";
                $state.go("Summary");


            } else {
                $scope.vm.error = "LoginID or password is incorrect";
                $scope.vm.loading = false;
            }
        });
    };
}