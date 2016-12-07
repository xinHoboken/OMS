var SummaryController = function ($scope, $http, $filter) {
    var url = "http://10.10.10.20/TeamAwesomeAPI/api/orders";
    $scope.gridOptions =
    {
        enableFiltering: true,
        columnDefs: [
             { name: "OrderNo", displayName: "Order No", enableFiltering: false },
             { name: "PartNo", displayName: "Part No", enableFiltering: false },
             //{ name: "Die", displayName: "Die", enableFiltering: false },
             { name: "PartDesc", displayName: "Part Desc", enableFiltering: false },
             { name: "MachNo", displayName: "Machine", enableFiltering: false },
             //{ name: "Std", displayName: "Std", enableFiltering: false },
             //{ name: "Act", displayName: "Act", enableFiltering: false },
             //{ name: "PackerQty", displayName: "Packer Qty", enableFiltering: false },
             //{ name: "Adj", displayName: "Adj", enableFiltering: false },
             { name: "Total", displayName: "Total", enableFiltering: false },
             //{ name: "OnHrs", displayName: "OnHrs", enableFiltering: false },
             //{ name: "BoxSize", displayName: "Box Desc", enableFiltering: false },
             { name: "BoxCount", displayName: "Box Count", enableFiltering: false },
             { name: "BoxCode", displayName: "Box Code", enableFiltering: false },
             { name: "Shift", displayName: "Shift", enableFiltering: true },
             { name: "OrderDate", displayName: "Order Date", enableFiltering: true, cellFilter: "date:\'yyyy-MM-dd'" }
        ],
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };
    $http.get(url).success(function (response) {
        $scope.gridOptions.data = response;
        console.log($scope.gridOptions);
    });

    $scope.filter = function () {
        console.log($scope.gridApi.grid);
        var date = new Date($scope.orderDate);
        var mm = date.getMonth() + 1;
        if (mm.toString().length < 2) {
            mm = 0 + mm.toString()
        }
        var dd = date.getDate();
        if (dd.toString().length < 2) {
            dd = 0 + dd.toString()
        }
        var yyyy = date.getFullYear();
        var date_yyyymmdd = yyyy + "-" + mm + "-" + dd;
        console.log(date_yyyymmdd);
        if (date_yyyymmdd !== "NaN-NaN-NaN") {
            $scope.gridApi.grid.columns[8].filters[0] = { term: date_yyyymmdd };
        }
        $scope.gridApi.grid.columns[7].filters[0] = { term: $scope.shift };
        //filter a specific column

        //filter all the columns
        //$scope.gridOptions.data = $filter('filter')($scope.gridOptions.data,'se34', undefined);
    }
};
