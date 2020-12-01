angular.module("IAMInterviewed").controller('AllProfilesController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.startDateSearch = "";
        $scope.endDateSearch = "";
        $scope.primarySkillSearch = "";
        $scope.companyAddedProfilesList = [];
        $scope.currentPage = 1;
        $scope.numPerPage = 20;
        $scope.viewRatingURl = "";
        $scope.nameSearch = "";
        $scope.recruiterSearch = "";
    }
    $scope.ClearAll();

    $scope.LoadPrimarySkills = function () {
        manageLoader('load');
        var getPrimarySkills = IAMInterviewed.Skills.GetSkills;
        $http.get(getPrimarySkills).then(function success(response) {
            //console.log(response.data);
            $scope.PrimarySkills = response.data.data;
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.LoadPrimarySkills();

    $scope.getCompanyAddedProfiles = function () {
        manageLoader('load');
        var getCompanyAddedCandidateDetialsURL = IAMInterviewed.Company.getCompanyAddedCandidateDetials + "?primaryskill=" + $scope.primarySkillSearch + "&companyId=" + $rootScope.loggedInUserDetails.UserID
            + "&startDate=" + $scope.startDateSearch + "&endDate=" + $scope.endDateSearch;
        $http.get(getCompanyAddedCandidateDetialsURL).then(function success(response) {
            $scope.companyAddedProfilesList = response.data.data;
            $scope.companyAddedProfilesListBase = response.data.data;
            $scope.totalItems = $scope.companyAddedProfilesList.length;
            //console.log($scope.companyAddedProfilesList);
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.viewRatingDetails = function (objCandidateRating) {
        $scope.$broadcast('bindCandidateRatingDetails', objCandidateRating.ReqId, objCandidateRating.ScheduleId);
        //$("#candidateratingdetails").modal('show');
    }

    $scope.paginate = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;
        index = $scope.companyAddedProfilesList.indexOf(value);
        return (begin <= index && index < end);
    };

    $scope.searchFilterHeader = function () {
        if ($scope.nameSearch == "" && $scope.recruiterSearch == "") {
            $scope.companyAddedProfilesList = $scope.companyAddedProfilesListBase;            
            $scope.totalItems = $scope.companyAddedProfilesList.length;
        }
        else if ($scope.nameSearch != "" && $scope.recruiterSearch == "") {
            $scope.companyAddedProfilesList = $.grep($scope.companyAddedProfilesListBase, function (value) {
                return value.CandidateName.toLowerCase().indexOf($scope.nameSearch.toLowerCase()) >= 0;
            });
            $scope.totalItems = $scope.companyAddedProfilesList.length;
        }
        else if ($scope.nameSearch == "" && $scope.recruiterSearch != "") {
            $scope.companyAddedProfilesList = $.grep($scope.companyAddedProfilesListBase, function (value) {
                return value.RecruiterName.toLowerCase().indexOf($scope.recruiterSearch.toLowerCase()) >= 0;
            });
            $scope.totalItems = $scope.companyAddedProfilesList.length;
        }
        else {
            $scope.companyAddedProfilesList = $.grep($scope.companyAddedProfilesListBase, function (value) {
                return value.CandidateName.toLowerCase().indexOf($scope.nameSearch.toLowerCase()) >= 0;
            });
            $scope.companyAddedProfilesList = $.grep($scope.companyAddedProfilesList, function (value) {
                return value.RecruiterName.toLowerCase().indexOf($scope.recruiterSearch.toLowerCase()) >= 0;
            });
            $scope.totalItems = $scope.companyAddedProfilesList.length;
        }
    }

    $(document).ready(function () {
        $timeout(function () {
            //onComponentLoad();
            DatePickerfuncAllDates();
            //$(".select2").select2();
        });
    });
}]);