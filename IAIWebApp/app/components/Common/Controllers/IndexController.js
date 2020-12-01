angular.module('IAMInterviewed').controller('IndexController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage) {
    $scope.loadTop10Ratings = function() {
        //manageLoader('load');
        var getTop10RatingsURL = IAMInterviewed.Admin.GetTop10Ratings;
        $http.get(getTop10RatingsURL).then(function success(response) {
            $scope.listTop10Ratings = response.data.data;
            //console.log($scope.listTop10Ratings);
            //manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            //manageLoader();
            return false;
        });
    };
    $scope.loadTop10Ratings();

    $(document).on('keydown', function (event) {
        if (event.key == "Escape") {
            //alert('Esc key pressed.');
            manageLoader()
        }
    });
}]);