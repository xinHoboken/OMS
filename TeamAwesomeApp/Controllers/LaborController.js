var LaborController = function ($scope, $http, EmpList, OrderList, PartList) {
    var url = "http://10.10.10.20/TeamAwesomeAPI/api/labors";

    var empList = [];
    EmpList.getEmpList().success(function (response) {
        for (var i in response) {
            empList[i] = { id: response[i].EmpID, FullName: response[i].FName + " " + response[i].LName };
        }
        $scope.EmpList = empList;
    });

    var orderList = [];
    OrderList.getOrderList().success(function (response) {
        for (var i in response) {
            orderList[i] = { id: response[i].OrderNo, Order: response[i].OrderNo };
        }
        $scope.OrderList = orderList;
    });

    var partList = [];
    PartList.getPartList().success(function (response) {
        for (var i in response) {
            partList[i] = { id: response[i].PartNo, PartName: response[i].PartNo };
        }
        $scope.PartList = partList;
    });

    $scope.gridOptions = {
        columnDefs: [
            { name: "FName", displayName: "Employee Name" },
            { name: "OrderNo", displayName: "Order No" },
            { name: "PartNo", displayName: "Part No" },
            { name: "Hours", displayName: "Hours" },
            { name: "Mins", displayName: "Mins" }
        ]
    }
    $http.get(url).success(function (response) {
        $scope.gridOptions.data = response;
    });

    $scope.AddLabor = function () {
        console.log("NewLabor")
        $http.post(url, $scope.NewLabor)
            .success(function (data) {
                console.log(data);
                var n = $scope.gridOptions.data.length + 1;
                $scope.gridOptions.data.push(data);
                $("#AddLabor").modal("hide");
            }).error(function (error) {
                console.log("error");
            });
        var master = {
            EmpID: '',
            OrderNo: '',
            PartNo: '',
            Hours: '',
            Mins: ''
        }
        $scope.NewLabor = angular.copy(master);
        $scope.form.$setPristine();
        $scope.form.$setUntouched();
    };
    $scope.reset = function () {
        var master = {
            EmpID: '',
            OrderNo: '',
            PartNo: '',
            Hours: '',
            Mins: ''
        }
        $scope.NewLabor = angular.copy(master);
        $scope.form.$setPristine();
        $scope.form.$setUntouched();
    }
};