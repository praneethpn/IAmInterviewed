angular.module('IAMInterviewed').controller('CandidateRatingController', ['$scope', '$http', '$filter', 'DataServices', '$rootScope', 'LoaderService', '$timeout', '$window', '$localStorage', function ($scope, $http, $filter, DataServices, $rootScope, LoaderService, $timeout, $window, $localStorage, $location) {
    $scope.bindInterviewSchedulebyId = function () {
        manageLoader('load');
        var getInterviewSchedulebyIdURL = IAMInterviewed.Candidate.getInterviewSchedulebyId + "?ScheduleId=" + $rootScope.ScheduleIdForRating;
        $http.get(getInterviewSchedulebyIdURL).then(function success(response) {
            $scope.RatingDetails = response.data.data[0];
            $scope.bindPieChart();
            $scope.bindSkillChart();
            //console.log($scope.RatingDetails);
            manageLoader();
        },
        function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            return false;
        });
    }
    if ($rootScope.ScheduleIdForRating == null || $rootScope.ScheduleIdForRating == undefined || $rootScope.ScheduleIdForRating == '') {
        $timeout(function () {
            $scope.RedirectUrl('Dashboard');
        }, 100);
    }
    else {
        $scope.bindInterviewSchedulebyId();
    }    

    $scope.acceptRating = function (status) {
        manageLoader('load');
        var acceptRatingURL = IAMInterviewed.Candidate.acceptRating + "?ScheduleId=" + $rootScope.ScheduleIdForRating + "&status=" + status;
        $http.get(acceptRatingURL).then(function success(response) {
            $rootScope.resultMessage = "Rating Accepted.";
            showNotification('success');
            $scope.bindInterviewSchedulebyId();
            manageLoader();
        },
        function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            return false;
        });
    }

    $scope.bindPieChart = function () {
        var Rating = $scope.RatingDetails.TotalRating;
        var MaxRating = 5;
        var UsedPercentage = (Rating / MaxRating) * 100;
        var $ppc = $('.progress-pie-chart'),
        percent = Math.round(UsedPercentage),
        deg = 360 * percent / 100;
        if (percent > 50) {
            $ppc.addClass('gt-50');
        }
        $('.ppc-progress-fill').css('transform', 'rotate(' + deg + 'deg)');
        $('.ppc-percents #percentage').html(percent);
    }

    $scope.bindSkillChart = function () {
        var SkillList = [];
        SkillList.push($scope.RatingDetails.SecondarySkill1);
        SkillList.push($scope.RatingDetails.SecondarySkill2);
        SkillList.push($scope.RatingDetails.SecondarySkill3);
        SkillList.push($scope.RatingDetails.SecondarySkill4);
        SkillList.push($scope.RatingDetails.SecondarySkill5);
        Highcharts.chart('divRatingBarChart', {
            chart: {
                type: 'column'
            },
            credits: {
                enabled: false
            },
            title: {
                text: ''
            },
            xAxis: {
                categories: SkillList,
                crosshair: true
            },
            yAxis: {
                min: 0,
                max: 5,
                tickInterval: 1,
                title: {
                    text: 'Rating'
                }
            },
            tooltip: {
                pointFormat: '<b>{point.y}</b>',
                shared: true
            },
            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                }
            },
            series: [{
                name: 'skill',
                data: [parseInt($scope.RatingDetails.SecondarySkill1Rating), parseInt($scope.RatingDetails.SecondarySkill2Rating), parseInt($scope.RatingDetails.SecondarySkill3Rating), parseInt($scope.RatingDetails.SecondarySkill4Rating), parseInt($scope.RatingDetails.SecondarySkill5Rating)],
                color: '#1e8cd6'
            }]
        });
    }

    var doc = new jsPDF();
    var specialElementHandlers = {
        '#editor': function (element, renderer) {
            return true;
        }
    };

    $("#btnGeneratePdf").click(function () {
        var options = {};
        let doc = new jsPDF('p', 'pt', 'a4');

        doc.addHTML($("#badgeContainerForDownload"), 15, 15, options, function () {
            doc.save('Badge.pdf');
        });
        //var options = {
        //};
        //var pdf = new jsPDF('p', 'pt', 'a4');
        //pdf.addHTML($("#badgeContainerForDownload"), 15, 15, options, function () {
        //    pdf.save('Badge.pdf');
        //});
        
    });
    var element = $("#badgeContainerForDownload"); // global variable
    var getCanvas;
    $("#btnImageDownload").click(function () {
        html2canvas(element, {
            onrendered: function (canvas) {
                $("#previewImage").append(canvas);
                getCanvas = canvas;
            }
        });

        var imgageData = getCanvas.toDataURL("image/png");
        // Now browser starts downloading it instead of just showing it
        var newData = imgageData.replace(/^data:image\/png/, "data:application/octet-stream");
        $("#btn-Convert-Html2Image").attr("download", "Badge.png").attr("href", newData);
        $("#btnImageDownload").hide();
        $("#btn-Convert-Html2Image").show();
    });
}]);