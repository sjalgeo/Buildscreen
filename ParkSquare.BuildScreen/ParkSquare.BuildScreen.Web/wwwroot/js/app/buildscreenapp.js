
angular.module("BuildScreenApp", [
        "ngRoute", "ngTouch", "ngDialog", "ngAnimate", "ngTagsInput",
        "BuildscreenApp.services",
        "BuildScreenApp.directives",
        "BuildScreenApp.filters"
    ])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider
          .when('/', {
              templateUrl: 'js/app/views/Buildscreen.html',
              controller: 'buildscreencontroller'
            })
          .otherwise({
              redirectTo: '/'
          });

        $locationProvider.html5Mode(true);
    });

angular.module("BuildscreenApp.services", ["ngResource"]);

angular.module("BuildScreenApp.directives", []);

