var UsersController = function ($scope, $http, UserTypeList, DeptList) {
    var url = "http://10.10.10.20/TeamAwesomeAPI/api/users";

    var deptList = [];
    DeptList.getDeptList().success(function (response) {
        for (var i in response) {
            deptList[i] = { id: response[i].DeptNo, DeptName: response[i].DeptName };
        }
        $scope.DepartmentList = deptList;
    });

    var userTypeList = [];
    UserTypeList.getUserTypeList().success(function (response) {
        for (var i in response) {
            userTypeList[i] = { id: response[i].UserTypeNo, UserTypeName: response[i].UserTypeName };
        }
        $scope.UserTypeList = userTypeList;
    });

    $scope.formData = {
        EmpID: '',
        FName: '',
        LName: '',
        Desc: '',
        DeptNo: '',
        DeptName: '',
        LoginID: '',
        Password: '',
        UserTypeNo: '',
        UserTypeName: '',
    }

    var blankFormData = angular.copy($scope.formData);

    $scope.gridOptions =
    {
        columnDefs: [
             { name: "FName", displayName: "First Name" },
             { name: "LName", displayName: "Last Name" },
             { name: "Desc", displayName: "Desc" },
             { name: "EmpID", displayName: "Emp ID", enableCellEdit: false },
             {
                 name: "UserTypeNo", displayName: "User Role",
                 editableCellTemplate: "ui-grid/dropdownEditor",
                 cellFilter: "mapUserType", //Transform data format
                 editDropdownValueLabel: "UserTypeName",
                 editDropdownOptionsArray: userTypeList
             },
             {
                 name: "DeptNo", displayName: "Department",
                 editableCellTemplate: "ui-grid/dropdownEditor",
                 cellFilter: "mapDept",
                 editDropdownValueLabel: "DeptName",
                 editDropdownOptionsArray: deptList
             },
             { name: "LoginID", displayName: "Login ID", enableCellEdit: false },
             { name: "Password", displayName: "Password" },
             {
                 name: "Delete",
                 cellTemplate: '<center><button class="btn btn-xs btn-danger delete-btn" ng-click="grid.appScope.deleteUser(row)">Delete</button></center>'
             }
        ]
    };

    $scope.gridOptions.onRegisterApi = function (gridApi) {
        //set gridApi on scope
        $scope.gridApi = gridApi;
        gridApi.edit.on.afterCellEdit($scope, function (rowEntity, colDef, newValue, oldValue) {
            $http.post(url + "/put/" + rowEntity.EmpID, rowEntity)
            .success(function (response) {
                console.log("Succeeded" + response);
            }).error(function (error) {
                console.log("Failed." + error);
            });
        });
    };

    $http.get(url).success(function (response) {
        $scope.gridOptions.data = response;
        console.log($scope.gridOptions);
    });

    $scope.deleteUser = function (row) {
        var ifDelete = confirm("Are you sure you want to delete " + row.entity.FName + " " + row.entity.LName + "?");
        if (ifDelete) {
            $http.post(url + "/delete/" + row.entity.EmpID)
            .success(function (response) {
                    var index = $scope.gridOptions.data.indexOf(row.entity);
                    $scope.gridOptions.data.splice(index, 1);
                }).error(function (error) {
                    console.log("Error occured when deleting an user.");
                });
        }
    };

    $scope.addUser = function () {
        console.log($scope.newUser);
        $http.post(url, $scope.newUser)
            .success(function (data) {
                $("#addUser").modal("hide");
                var n = $scope.gridOptions.data.length + 1;
                $scope.gridOptions.data.push(data);
            }).error(function (error) {
                console.log("Error ocurred when adding a new user.");
            });
        $scope.newUser = angular.copy(blankFormData);
        $scope.form.$setPristine();
    };

    

    $scope.ClearFormContent = function () {
        $scope.newUser = angular.copy(blankFormData);
        $scope.form.$setPristine();
    }
};