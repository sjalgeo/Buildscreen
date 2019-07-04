angular.module("BuildscreenApp.services")
    .factory("Build", function($resource) {
            return $resource("/api/:urlString/:since");
        }
    );