angular.module("IAMInterviewed").controller('PostJobController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.ClearAll = function () {
        $scope.jobType = "";
        $scope.jobCode = "";
        $scope.jobDescription = "";
        $scope.keyResponsibilities = "";
        $scope.primarySkill = "";
        $scope.secondarySkill1 = "";
        $scope.secondarySkill2 = "";
        $scope.secondarySkill3 = "";
        $scope.secondarySkill4 = "";
        $scope.secondarySkill5 = "";
        $scope.additionalSkills = "";
        $scope.minExperience = "";
        $scope.maxExperience = "";
        $scope.startDate = "";
        $scope.endDate = "";
        $scope.location = "";
        $scope.designation = "";
        $scope.highestPay = "";
        $scope.primarySkillSearch = "";
        $scope.jobCodeSearch = "";
        $scope.requirementId = 0;
        $scope.currentPage = 1;
        $scope.numPerPage = 20;
    }
    $scope.ClearAll();

    $scope.LoadPrimarySkills = function () {
        var deferred = $q.defer();
        var getPrimarySkills = IAMInterviewed.Skills.GetSkills;
        $http.get(getPrimarySkills).then(function success(response) {
            //console.log(response.data);
            $scope.PrimarySkills = response.data.data;
            deferred.resolve(response.data.data);
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            deferred.reject;
        });
        return deferred.promise;
    }

    $scope.LoadSecondarySkills = function () {
        manageLoader('load');
        var deferred = $q.defer();
        var GetSecondarySkillsURL = IAMInterviewed.Skills.GetSecondarySkills + "?PrimarySkill=" + $scope.primarySkill;
        $http.get(GetSecondarySkillsURL).then(function success(response) {
            //console.log(response.data);
            $scope.SecondarySkills = response.data.data;
            manageLoader();
            deferred.resolve(response.data.data);
        },
            function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                deferred.reject;
            });
        return deferred.promise;
    }

    $scope.LoadCities = function () {
        var deferred = $q.defer();
        var getCitiesURL = IAMInterviewed.Skills.getCities + "?Country=" + $rootScope.loggedInUserDetails.Country;
        $http.get(getCitiesURL).then(function success(response) {
            //console.log(response.data);
            $scope.Cities = response.data.data;
            deferred.resolve(response.data.data);
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            deferred.reject;
        });
        return deferred.promise;
    }

    $scope.getAllDesignations = function () {
        var getDesignationURL = IAMInterviewed.Company.getDesignation + "?companyId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getDesignationURL).then(function success(response) {
            //console.log(response.data);
            $scope.designationList = response.data.data;
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
        });
    }

    $scope.LoadPrimarySkills();
    $scope.LoadCities();
    $scope.getAllDesignations();

    $scope.getCompanyRequirements = function () {
        var deferred = $q.defer();
        manageLoader('load');
        var status = "Open";
        var postedBy = "";
        var getCompanyRequirementsURL = IAMInterviewed.Company.getCompanyRequirements + "?companyId=" + $rootScope.loggedInUserDetails.UserID + "&primarySkill=" + $scope.primarySkillSearch
            + "&jobCode=" + $scope.jobCodeSearch + "&postedBy=" + postedBy + "&status=" + status;
        $http.get(getCompanyRequirementsURL).then(function success(response) {
            $scope.companyRequirementsAll = response.data.data;
            $scope.companyRequirements = response.data.data;
            $scope.totalItems = $scope.companyRequirements.length;
            manageLoader();
            deferred.resolve(response.data.data);
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            deferred.reject;
        });
        return deferred.promise;
    }

    $scope.getCompanyRequirements();

    $scope.getCompanyRequirementsFiltered = function () {
        if ($scope.jobCodeSearch == "" && $scope.primarySkillSearch != "") {
            var filteredArray = $.grep($scope.companyRequirementsAll, function (e) {
                return (e.PrimarySkill == parseInt($scope.primarySkillSearch));
            });
            //if (filteredArray.length > 0) {
            $scope.companyRequirements = filteredArray;
            //}
        }
        else if ($scope.jobCodeSearch != "" && $scope.primarySkillSearch == "") {
            var filteredArray = $.grep($scope.companyRequirementsAll, function (e) {
                return (e.JobCode == $scope.jobCodeSearch);
            });
            //if (filteredArray.length > 0) {
            $scope.companyRequirements = filteredArray;
            //}
        }
        else if ($scope.jobCodeSearch != "" && $scope.primarySkillSearch != "") {
            var filteredArray = $.grep($scope.companyRequirementsAll, function (e) {
                return (e.JobCode == $scope.jobCodeSearch && e.PrimarySkill == parseInt($scope.primarySkillSearch));
            });
            //if (filteredArray.length > 0) {
            $scope.companyRequirements = filteredArray;
            //}
        }
        else {
            $scope.companyRequirements = $scope.companyRequirementsAll;
        }
    }

    $scope.editRequirement = function (objRequirement) {
        $scope.primarySkill = objRequirement.PrimarySkill.toString();
        $scope.LoadSecondarySkills().then(function (response) {
            $scope.jobType = objRequirement.JobType.toString();
            $scope.jobCode = objRequirement.JobCode;
            $scope.jobDescription = objRequirement.JobDesc;
            $scope.keyResponsibilities = objRequirement.Remarks;
            $scope.secondarySkill1 = objRequirement.SecSkill1.toString();
            $scope.secondarySkill2 = objRequirement.SecSkill2.toString();
            $scope.secondarySkill3 = objRequirement.SecSkill3.toString();
            $scope.secondarySkill4 = objRequirement.SecSkill4.toString();
            $scope.secondarySkill5 = objRequirement.SecSkill5.toString();
            $scope.additionalSkills = objRequirement.AdditionalSkills;
            $scope.minExperience = objRequirement.MinExp.toString();
            $scope.maxExperience = objRequirement.MaxExp.toString();
            $scope.startDate = objRequirement.DisplayJobStartDate;
            $scope.endDate = objRequirement.DisplayJobEndDate;
            $scope.location = objRequirement.Location.toString();
            $scope.designation = objRequirement.JobTitleId.toString();
            $scope.HighestPay = objRequirement.HighestPay.toString();
            $scope.requirementId = objRequirement.ReqId;
            $(".scroll-to-top").click();
        });
    }

    $scope.saveCompanyRequirements = function () {
        manageLoader('load');
        var objData = {
            ReqId: $scope.requirementId,
            JobCode: $scope.jobCode,
            JobTitle: $scope.designation,
            JobType: $scope.jobType,
            Location: $scope.location,
            MinExp: $scope.minExperience,
            MaxExp: $scope.maxExperience,
            HighestPay: $scope.highestPay,
            PrimarySkill: $scope.primarySkill,
            SecSkill1: $scope.secondarySkill1,
            SecSkill2: $scope.secondarySkill2,
            SecSkill3: $scope.secondarySkill3,
            SecSkill4: $scope.secondarySkill4,
            SecSkill5: $scope.secondarySkill5,
            JobDesc: $scope.jobDescription,
            JobPostingSDate: $scope.startDate,
            JobPostingEDate: $scope.endDate,
            Remarks: $scope.keyResponsibilities,
            UserId: $rootScope.loggedInUserDetails.UserID,
            AdditionalSkills: $scope.additionalSkills,
        }
        var saveCompanyRequirementsURL = IAMInterviewed.Company.saveCompanyRequirements;
        $http.post(saveCompanyRequirementsURL, objData).then(function (response) {
            if (response.data.Success == true) {
                $rootScope.resultMessage = "Job Posted Successfylly.";
                showNotification('success');
                manageLoader();
                //$scope.ClearAll();
                //request_Interviewer_form.$setUntouched();
                //$('#form_wizard_1').bootstrapWizard('show', 1);
                //$scope.getCompanyRequirements();
                $scope.getCompanyRequirements().then(function (response) {
                    var jobcodeFiltered = $.grep($scope.companyRequirementsAll, function (e) {
                        return (e.JobCode == $scope.jobCode);
                    });
                    if (jobcodeFiltered.length > 0) {
                        $rootScope.objRequirement = jobcodeFiltered[0];
                        $timeout(function () {
                            $scope.RedirectUrl('AddProfiles');
                        }, 1000);
                    }
                });
            }
            else {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
            }
        }, function (response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
        });
    }

    $scope.paginate = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;
        index = $scope.companyRequirements.indexOf(value);
        return (begin <= index && index < end);
    };

    $(document).ready(function () {
        $timeout(function () {
            onComponentLoad();
            DatePickerfuncAllDates();
            //DatePickerfuncAllDatesddmm();
        });
    });
}]);