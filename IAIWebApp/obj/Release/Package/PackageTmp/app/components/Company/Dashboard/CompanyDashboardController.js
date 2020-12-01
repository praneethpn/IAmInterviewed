angular.module("IAMInterviewed").controller('CompanyDashboardController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    if ($rootScope.loggedInUserDetails.UserType == "Recruiter" || $rootScope.loggedInUserDetails.UserType == "PM") {
        $scope.RedirectUrl('RecruiterDashboard');
    }
    $scope.loadDashboardDetails = function () {
        manageLoader('load');
        var getDasbboardDetailsURL = IAMInterviewed.Company.getCompanyDashboardDetails + "?companyId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getDasbboardDetailsURL).then(function success(response) {
            $scope.CompanyDashboardDetails = response.data.data;
            //$scope.$broadcast("loadDunutChart", $rootScope.CandidateDashboardDetails);
            //console.log($scope.CompanyDashboardDetails);
            BindDonut(Math.floor(($scope.CompanyDashboardDetails.interviews[0].UsedInterviewes / $scope.CompanyDashboardDetails.interviews[0].InterviewsAllowed) * 100));
            BindRequirementsPie();
            BindProfilesBySkillChart();
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

    function BindDonut(chart_Value) {
        var chartProgress = document.getElementById("chartInterviews");
        if (chartProgress) {
            var myChartCircle = new Chart(chartProgress, {
                type: 'doughnut',
                data: {
                    labels: ["Rating", 'null'],
                    datasets: [{
                        label: "",
                        backgroundColor: ["#147fb7"],
                        data: [chart_Value, 100 - chart_Value]
                    }]
                },
                plugins: [{
                    beforeDraw: function (chart) {
                        var width = chart.chart.width,
                            height = chart.chart.height,
                            ctx = chart.chart.ctx;

                        ctx.restore();
                        var fontSize = (height / 100).toFixed(2);
                        ctx.font = fontSize + "em sans-serif";
                        ctx.fillStyle = "#00950c";
                        ctx.textBaseline = "middle";

                        var text = "" + chart_Value + "%",
                            textX = Math.round((width - ctx.measureText(text).width) / 2),
                            textY = height / 2;

                        ctx.fillText(text, textX, textY);
                        ctx.save();
                    }
                }],
                options: {
                    legend: {
                        display: false,
                    },
                    responsive: true,
                    maintainAspectRatio: false,
                    cutoutPercentage: 75
                }

            });


        }
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

    function BindProfilesBySkillChart() {
        var dataBarChartSkill = [];
        var dataBarChart = [];
        var backgroundColors = []
        angular.forEach($scope.CompanyDashboardDetails.skillReport, function (value, key) {
            dataBarChartSkill.push(value.PrimarySkillName);
            dataBarChart.push(value.CompanyProfiles);
            backgroundColors.push("#36c");
        }); 
        var chartProgress = document.getElementById("chartProfilesBySkill");
        if (chartProgress) {
            var myChartCircle = new Chart(chartProgress, {
                type: 'bar',
                data: {
                    datasets: [{
                        barPercentage: 0.5,
                        barThickness: 20,
                        minBarLength: 2,
                        data: dataBarChart,
                        backgroundColor: backgroundColors
                    }],
                    labels: dataBarChartSkill
                },
                options: {
                    legend: {
                        display: false,
                    },
                    scales: {
                        xAxes: [{
                            scaleLabel: {
                                display: true,
                                labelString: 'Primary Skill'
                            },
                            ticks: {
                                display: false //this will remove only the label
                            }
                        }],
                        yAxes: [{
                            display: true,
                            scaleLabel: {
                                display: true,
                                labelString: 'No Of Profiles'
                            }
                        }]
                    },
                    responsive: true,
                    maintainAspectRatio: false
                }

            });


        }
    }
}]);