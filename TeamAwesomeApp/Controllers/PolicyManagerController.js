var PolicyManagerController = function ($scope, $http) {
    $scope.admin = {};
    $scope.normal = {};

    $http.get("http://10.10.10.20/TeamAwesomeAPI/api/Permissions")
    .success(function (response) {
        $scope.Admin = response[1];
        $scope.Normal = response[2];
        $scope.admin.isChecked = $scope.Admin.AR;
        $scope.user.isChecked = $scope.Admin.AR;
        $scope.adminPM = $scope.Admin.PR;
        $scope.userPM = $scope.Normal.PR;
    })
    .error(function (error) {
        console.log("Failed to get permission.");
    });

    $scope.adminChange = function () {
        $scope.user.isChecked = $scope.admin.isChecked;
        var data = {
            UserTypeNo: 2,
            UserTypeName: "Admin",
            AR: $scope.admin.isChecked,
            PR: $scope.adminPM
        };
        console.log(data);
        $http.put("http://10.10.10.20/TeamAwesomeAPI/api/Permissions/2", data)
        .success(function (response) {


        })
        .error(function () {
        });
    }

    $scope.userChange = function () {
        //If there are multiple users selections, use this 
        //$scope.admin.isChecked = $scope.user.isChecked || $scope.user2.isChecked;

        $scope.admin.isChecked = $scope.user.isChecked;

        var data = {
            UserTypeNo: 2,
            UserTypeName: "Admin",
            AR: $scope.admin.isChecked,
            PR: $scope.adminPM.isChecked
        };
        console.log(data);
        $http.put("http://10.10.10.20/TeamAwesomeAPI/api/Permissions/2", data)
        .success(function (response) {


        })
        .error(function () {
        });
    }


    $scope.normalChange = function () {
        var data = {
            UserTypeNo: 3,
            UserTypeName: "Normal",
            AR: false,
            PR: $scope.userPM
        };
        console.log(data);
        $http.put("http://10.10.10.20/TeamAwesomeAPI/api/Permissions/3", data)
        .success(function (response) {


        })
        .error(function () {
        });
    }

    $scope.admin = {
        name: 'Administration',
        type: 'parent',
        isChecked: false
    };
    $scope.user = {
        name: 'Users',
        type: 'child',
        isChecked: false
    };

    //$scope.user2 = {
    //    name: 'Usertest',
    //    type: 'child',
    //    isChecked: false
    //};

   
}