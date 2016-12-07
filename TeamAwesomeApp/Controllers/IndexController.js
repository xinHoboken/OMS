var IndexController = function ($scope, $localStorage, $http, $localStorage) {
    $scope.isAuthed = $localStorage.currentUser;
    $scope.$watch(function () { return $localStorage.currentUser; }, function (newVal, oldVal) {
        if (oldVal !== newVal) {
            $scope.isAuthed = $localStorage.currentUser;
            console.log($scope.isAuthed);
            if ($scope.isAuthed) {
                $http.get("http://10.10.10.20/TeamAwesomeAPI/api/Permissions/" + $scope.isAuthed.username)
                .success(function (response) {
                    $localStorage.currentUser.UserTypeNo = response.UserTypeNo;
                    $localStorage.currentUser.UserTypeName = response.UserTypeName;
                    $localStorage.currentUser.AR = response.AR;
                    $localStorage.currentUser.PR = response.PR;
                    console.log($localStorage.currentUser);
                })
                .error(function (error) {
                    console.log("Failed to get permission."); 
                });
            }
        }
    })
}