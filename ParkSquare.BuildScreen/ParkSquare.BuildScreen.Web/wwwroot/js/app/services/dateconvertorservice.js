angular.module("BuildscreenApp.services").
factory("DateConvertor", function () {
    var convertToTicks = function (dateString) {
	    return new Date(dateString).getTime();
    }
    return {
        convertDatesToTicks: function (builds) {
            for (var i = 0; i < builds.length; i++) {
                builds[i].finishBuildDateTime = parseInt(convertToTicks(builds[i].finishBuildDateTime));
                builds[i].startBuildDateTime = parseInt(convertToTicks(builds[i].startBuildDateTime));
            }
            return builds;
        }
    }
});

