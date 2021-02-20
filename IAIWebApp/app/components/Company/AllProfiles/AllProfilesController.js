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

    $scope.getHistory = function (profileId) {
        manageLoader('load');
        var getProfileHistoryURL = IAMInterviewed.Company.getProfileHistory + "?ProfileId=" + profileId + "&companyId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getProfileHistoryURL).then(function success(response) {
            //console.log(response.data);
            $scope.profileHistoryData = response.data.data;
            $("#candidateHistory").modal("show");
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.exportToExcel = function () {
        var createXLSLFormatObj = [];
        /* XLS Head Columns */
        var xlsHeader = ["Name", "Email Id", "Mobile Number", "Primary Skill", "Designation", "Recruiter", "Created Date", "Interview Date", "Interview Type", "Rating", "Select Status", "Status Comments"];
        var xlsRows = [];
        angular.forEach($scope.companyAddedProfilesList, function (value, key) {
            var xlsRowObject = {
                "Name": value.CandidateName,
                "Email Id": value.Email,
                "Mobile Number": value.MobileNumber,
                "Primary Skill": value.PrimarySKillName,
                "Designation": value.Designation,
                "Recruiter": value.RecruiterName,
                "Created Date": value.DisplayDate,
                "Interview Date": value.InterviewDateNew,
                "Interview Type": value.InterviewType,
                "Rating": value.OveralRating,
                "Select Status": value.SelectStetus,
                "Status Comments": value.StatusRemarks
            }
            xlsRows.push(xlsRowObject);
        });
        createXLSLFormatObj.push(xlsHeader);
        $.each(xlsRows, function (index, value) {
            var innerRowData = [];
            //$("tbody").append('<tr><td>' + value.CandidateName + '</td><td>' + value.Email + '</td><td>' + value.MobileNumber + '</td><td>' + value.PrimarySKillName
            //    + '</td><td>' + value.Designation + '</td><td>' + value.RecruiterName + '</td><td>' + value.DisplayDate + '</td><td>' + value.InterviewDateNew
            //    + '</td><td>' + value.InterviewType + '</td><td>' + value.OveralRating + '</td><td>' + value.SelectStetus + '</td><td>' + value.StatusRemarks + '</td></tr>');
            $.each(value, function (ind, val) {

                innerRowData.push(val);
            });
            createXLSLFormatObj.push(innerRowData);
        });
        /* File Name */
        var filename = "AllProfiles.xlsx";
        /* Sheet Name */
        var ws_name = "AllProfiles";

        if (typeof console !== 'undefined') console.log(new Date());
        var wb = XLSX.utils.book_new(),
            ws = XLSX.utils.aoa_to_sheet(createXLSLFormatObj);

        /* Add worksheet to workbook */
        XLSX.utils.book_append_sheet(wb, ws, ws_name);

        /* Write workbook and Download */
        if (typeof console !== 'undefined') console.log(new Date());
        XLSX.writeFile(wb, filename);
        if (typeof console !== 'undefined') console.log(new Date());
    }

    $(document).ready(function () {
        $timeout(function () {
            //onComponentLoad();
            DatePickerfuncAllDates();
            //$(".select2").select2();
        });
    });
}]);