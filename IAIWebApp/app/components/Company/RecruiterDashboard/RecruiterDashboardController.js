angular.module("IAMInterviewed").controller('RecruiterDashboardController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.loadDashboardDetails = function () {
        manageLoader('load');
        var getDasbboardDetailsURL = IAMInterviewed.Company.getCompanyDashboardDetails + "?companyId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getDasbboardDetailsURL).then(function success(response) {
            $scope.CompanyDashboardDetails = response.data.data;
            //$scope.$broadcast("loadDunutChart", $rootScope.CandidateDashboardDetails);
            //console.log($scope.CompanyDashboardDetails);
            //BindDonut(Math.floor(($scope.CompanyDashboardDetails.interviews[0].UsedInterviewes / $scope.CompanyDashboardDetails.interviews[0].InterviewsAllowed) * 100));
            BindRequirementsPie();
            //BindProfilesBySkillChart();
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            return false;
        });
    };
    $scope.loadDashboardDetails();

    $scope.redirectToDashboard = function (status) {
        $rootScope.RequirementsDashboardStatus = status;
        $scope.RedirectUrl('RequirementsDashboard');
    }

    function BindRequirementsPie() {
        var dataPieChart = [];
        dataPieChart.push(parseInt($scope.CompanyDashboardDetails.requirements[0].OpenRequirements));
        dataPieChart.push(parseInt($scope.CompanyDashboardDetails.requirements[0].ClosedRequirements));
        dataPieChart.push(parseInt($scope.CompanyDashboardDetails.requirements[0].OnHoldRequirements));
        var chartProgress = document.getElementById("chartRequirements");
        if (chartProgress) {
            var myChartCircle = new Chart(chartProgress, {
                type: 'pie',
                data: {
                    labels: {
                        render: 'label'
                    },
                    datasets: [{
                        data: dataPieChart,
                        backgroundColor: [
                            "#36c",
                            "#dc3912",
                            "#ffff00"
                        ]
                    }],
                    labels: [
                        'Open',
                        'Closed',
                        'On Hold'
                    ]
                },
                options: {
                    legend: {
                        display: true,
                    },
                    responsive: true,
                    maintainAspectRatio: false
                }

            });


        }
    }
}]);