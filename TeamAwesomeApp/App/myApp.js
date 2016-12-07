var myApp = angular.module("myModule", ["ui.grid", "ui.router", "ui.grid.edit", 'ngMessages', 'ngStorage', "LocalStorageModule", "ngAnimate"]);

(function () {
    myApp.service("DeptList", function ($http) {
            return {
                getDeptList: function () {
                    return $http.get("http://10.10.10.20/TeamAwesomeAPI/api/Departments");
                }
            };
        })
        .service("UserTypeList", function ($http) {
            return {
                getUserTypeList: function () {
                    return $http.get("http://10.10.10.20/TeamAwesomeAPI/api/Permissions");
                }
            };
        })
        .service("EmpList", function ($http) {
        return {
            getEmpList: function() {
                return $http.get("http://10.10.10.20/TeamAwesomeAPI/api/Users");
            }
            };
            })
    .service("OrderList", function ($http) {
        return {
                getOrderList: function () {
                return $http.get("http://10.10.10.20/TeamAwesomeAPI/api/Orders");
                }
                };
                })
    .service("PartList", function ($http) {
        return {
        getPartList: function () {
                return $http.get("http://10.10.10.20/TeamAwesomeAPI/api/Parts");
            }
            }
            });


    myApp.config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/Login');

        $stateProvider
            .state("Index", {
                abstract: true,
                templateUrl: "Index.html",
            })
            .state("Login", {
                url: "/Login",
                templateUrl: "Login.html"
            })
            .state("Users", {
                url: "/Users",
                templateUrl: "Users.html"
            })
            .state("Summary", {
                url: "/Summary",
                templateUrl: "Summary.html"
            })
            .state("Labor", {
                url: "/Labor",
                templateUrl: "Labor.html"
            })
            .state("ChangePass", {
                url: "/ChangePass",
                templateUrl: "ChangePass.html"
            })
            .state("PolicyManager", {
                url: "/PolicyManager",
                templateUrl: "PolicyManager.html"
            })

    });

    myApp.run(function run($rootScope, $http, $location, $localStorage, $state) {
    
        //// keep user logged in after page refresh
        if ($localStorage.currentUser) {            
            $http.defaults.headers.common.Authorization = 'Bearer ' + $localStorage.currentUser.token;
        }

        // redirect to login page if not logged in and trying to access a restricted page
        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            var publicPages = ['/login'];
            var restrictedPage = publicPages.indexOf($location.path()) === -1;
            if (restrictedPage && !$localStorage.currentUser) {
                //$location.path('/login')
            $state.go("Login");
               
            }
        });
    });
    myApp.controller("IndexController", IndexController);

    myApp.controller("LoginController", LoginController);

    myApp.controller("SummaryController", SummaryController);

    myApp.controller("LaborController", ["$scope", "$http", "EmpList", "OrderList", "PartList", LaborController]);

    myApp.controller("UsersController", UsersController)
        .filter('mapDept', function (DeptList) {
            //var deptHash = {};
            //DeptList.getDeptList()
            //    .success(function (response) {
            //        for (var i in response) {
            //            var k = response[i].DeptNo;
            //            var v = response[i].DeptName;
            //            deptHash[k] = v;
            //        }
            //        console.log(deptHash);
            //    });

            var deptHash = {
                "1": "IT",
                "2": "Executive",
                "3": "Shipping"
            };

            return function (input) {
                console.log(deptHash[input]);
                if (!input) {
                    return '';
                } else {
                    return deptHash[input];
                }
            };
        })
        .filter('mapUserType', function () {
            var userTypeHash = {
                "1": "Normal",
                "2": "Admin",
                "3": "SuperAdmin"
            };

            return function (input) {
                if (!input) {
                    return '';
                } else {
                    return userTypeHash[input];
                }
            };
        });
})();

myApp.controller("ChangePassController", ChangePassController);

myApp.controller("PolicyManagerController", PolicyManagerController);

//this part is the validation for password match
var compareTo = function () {
    return {
        require: "ngModel", //required directive
        scope: {
            otherModelValue: "=compareTo" //isolated scope
        },
        link: function (scope, element, attributes, ngModel) {

            ngModel.$validators.compareTo = function (modelValue) {
                return modelValue == scope.otherModelValue;
            };

            scope.$watch("otherModelValue", function () {
                ngModel.$validate();
            });
        }
    };
};

myApp.directive("compareTo", compareTo);
