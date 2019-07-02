
angular.module("BuildScreenApp", [
        "ngRoute", "ngTouch", "ngDialog", "ngAnimate", "ngTagsInput",
        "BuildscreenApp.services",
        "BuildScreenApp.directives",
        "BuildScreenApp.filters"
    ])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider
          .when('/', {
              templateUrl: 'Scripts/App/Views/Buildscreen.html',
              controller: 'BuildScreenController'
            })
          .otherwise({
              redirectTo: '/'
          });

        $locationProvider.html5Mode(true);
    });

angular.module("BuildscreenApp.services", ["ngResource"]);

angular.module("BuildScreenApp.directives", []);

