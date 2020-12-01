angular.module("IAMInterviewed").controller('CandidateDashboardController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    $scope.currentPage = 1;
    $scope.numPerPage = 20;
    $scope.loadFavoriteCompany = function () {
        manageLoader('load');
        var getFavoriteCompanyURL = IAMInterviewed.Candidate.getFavoriteCompany + "?UserId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getFavoriteCompanyURL).then(function success(response) {
            //console.log(response.data);
            $scope.FavoriteCompanies = response.data.data;
            manageLoader();
        },
            function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                return false;
            });
    }
    $scope.loadFavoriteCompany();

    $scope.loadViewedProfiles = function () {
        manageLoader('load');
        var getViewedProfilesURL = IAMInterviewed.Candidate.getViewedProfiles + "?UserId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getViewedProfilesURL).then(function success(response) {
            //console.log(response.data);
            $scope.ViewedProfiles = response.data.data;
            manageLoader();
        },
            function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                return false;
            });
    }
    $scope.loadViewedProfiles();

    $scope.loadScheduledInterview = function () {
        manageLoader('load');
        var getScheduledInterviewURL = IAMInterviewed.Candidate.getScheduledInterview + "?UserId=" + $rootScope.loggedInUserDetails.UserID;
        $http.get(getScheduledInterviewURL).then(function success(response) {
            //console.log(response.data);
            $scope.ScheduledInterviews = response.data.data;
            $scope.totalItems = $scope.ScheduledInterviews.length;
            manageLoader();
        }, function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            return false;
        });
    }
    $scope.loadScheduledInterview();

    $scope.paginate = function (value) {
        var begin, end, index;
        begin = ($scope.currentPage - 1) * $scope.numPerPage;
        end = begin + $scope.numPerPage;
        index = $scope.ScheduledInterviews.indexOf(value);
        return (begin <= index && index < end);
    };

    $scope.grantAccessToCompany = function (objAccess) {
        manageLoader('load');
        var updateGrantAccessURL = IAMInterviewed.Candidate.updateGrantAccess + "?UserId=" + $rootScope.loggedInUserDetails.UserID + "&ReqId=" + objAccess.ReqId + "&JobCode=" + objAccess.JobCode;
        $http.get(updateGrantAccessURL).then(function success(response) {
            //console.log(response.data);
            $rootScope.resultMessage = response.data.data;
            showNotification('success');
            $scope.loadViewedProfiles();
            manageLoader();
        },
            function error(response) {
                $rootScope.resultMessage = response.data.errorMessage;
                showNotification('error');
                manageLoader();
                return false;
            });
    }

    $scope.viewRating = function (objInterview) {
        if (objInterview.CompanySchedule == "YES") {
            $("#divRestricted").show();
            BindRatingDonutChartTechRating(parseFloat(3));
            BindProfilesBySkillChart();
        }
        else {
            $("#divRestricted").hide();
            $rootScope.ScheduleIdForRating = objInterview.ScheduleId;
            $timeout(function () {
                $scope.RedirectUrl('Rating');
            }, 100);
        }
    }

    function BindDonut(chart_Value, companySchedule) {
        var chartProgress = document.getElementById("chartProgress");
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

                        var text = companySchedule == "YES" ? "R" : "" + chart_Value + "%",
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
                    cutoutPercentage: 65
                }

            });


        }
    }

    function BindRatingDonutChartTechRating(chart_Value) {
        var chart_Value_percentage = Math.floor((chart_Value / 5) * 100);
        var chartProgress = document.getElementById("chartTotalRatingIAIFormat");
        if (chartProgress) {
            var myChartCircle = new Chart(chartProgress, {
                type: 'doughnut',
                data: {
                    labels: ["Rating", 'null'],
                    datasets: [{
                        label: "",
                        backgroundColor: ["#147fb7"],
                        data: [chart_Value_percentage, 100 - chart_Value_percentage]
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

                        var text = "" + chart_Value + " / 5",
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
                    cutoutPercentage: 65
                }

            });
        }
        var chartProgressIAIFormat = document.getElementById("chartTotalRatingIAIFormat");
        if (chartProgressIAIFormat) {
            var myChartCircle = new Chart(chartProgressIAIFormat, {
                type: 'doughnut',
                data: {
                    labels: ["Rating", 'null'],
                    datasets: [{
                        label: "",
                        backgroundColor: ["#147fb7"],
                        data: [chart_Value_percentage, 100 - chart_Value_percentage]
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

                        var text = "" + chart_Value + " / 5",
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
                    cutoutPercentage: 65
                }

            });
        }
    }

    function BindProfilesBySkillChart() {
        var dataBarChartSkill = [];
        var dataBarChart = [];
        var backgroundColors = []
        //angular.forEach($scope.ratingDetailsIAI, function (value, key) {
        dataBarChartSkill.push('Java');
        dataBarChartSkill.push('Microservices');
        dataBarChartSkill.push('SpringBoot');
        dataBarChartSkill.push('WebService');
        dataBarChartSkill.push('Database');
        dataBarChart.push(parseInt(3));
        dataBarChart.push(parseInt(3));
        dataBarChart.push(parseInt(2));
        dataBarChart.push(parseInt(4));
        dataBarChart.push(parseInt(3));
        backgroundColors.push("#36c");
        backgroundColors.push("#36c");
        backgroundColors.push("#36c");
        backgroundColors.push("#36c");
        backgroundColors.push("#36c");
        //});
        var chartProgress = document.getElementById("chartRatingBySkillIAIFormat");
        if (chartProgress) {
            var myChartCircle = new Chart(chartProgress, {
                type: 'bar',
                data: {
                    datasets: [{
                        barPercentage: 0.5,
                        barThickness: 50,
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
                                labelString: 'Secondary Skills'
                            },
                            ticks: {
                                display: true //this will remove only the label
                            }
                        }],
                        yAxes: [{
                            display: true,
                            scaleLabel: {
                                display: true,
                                labelString: 'Rating'
                            },
                            ticks: {
                                max: 5,
                                min: 0,
                                stepSize: 1
                            }
                        }]
                    },
                    responsive: true,
                    maintainAspectRatio: false
                }
            });
        }
        var chartProgressIAIFormat = document.getElementById("chartRatingBySkillIAIFormat");
        if (chartProgressIAIFormat) {
            var myChartCircle = new Chart(chartProgressIAIFormat, {
                type: 'bar',
                data: {
                    datasets: [{
                        barPercentage: 0.5,
                        barThickness: 50,
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
                                labelString: 'Secondary Skills'
                            },
                            ticks: {
                                display: true //this will remove only the label
                            }
                        }],
                        yAxes: [{
                            display: true,
                            scaleLabel: {
                                display: true,
                                labelString: 'Rating'
                            },
                            ticks: {
                                max: 5,
                                min: 0,
                                stepSize: 1
                            }
                        }]
                    },
                    responsive: true,
                    maintainAspectRatio: false
                }
            });
        }
    }

    $scope.$on("loadDunutChart", function (evt, data) {
        // handler code here 
        console.log(data);
        BindDonut(Math.floor(data.TotalRating / 5 * 100), data.CompanySchedule);
    });

    if ($scope.$parent.CandidateDashboardDetails != undefined && $scope.$parent.CandidateDashboardDetails != "" && $scope.$parent.CandidateDashboardDetails != null) {
        BindDonut(Math.floor($scope.$parent.CandidateDashboardDetails.TotalRating / 5 * 100), $scope.$parent.CandidateDashboardDetails.CompanySchedule);
    }
}]);