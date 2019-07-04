angular.module("BuildScreenApp.directives")
    .directive("buildItem", function () {
        return {
            restrict: "AE",
            replace: true,
            priority: 1001,
            templateUrl: "js/app/templates/builddirectivetemplate.html"
        };
    });