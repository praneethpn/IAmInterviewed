angular.module('IAMInterviewed').controller('IndexController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage) {
    $scope.loadTop10Ratings = function () {
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

    //var objDataOSAP = {
    //    QuestionId: null,
    //    QuestionText: 'Question  Test 1',
    //    DomainId: 3,
    //    ComplexityId: 1,
    //    QuestionTypeId: 7,
    //    answers: [
    //        { "AnswerId": 0, "AnswerText": "Test Answer 4", "AnswerOrder": 4, "CorrectAnswer": true },
    //        { "AnswerId": 0, "AnswerText": "Test Answer 4", "AnswerOrder": 4, "CorrectAnswer": true },
    //        { "AnswerId": 0, "AnswerText": "Test Answer 4", "AnswerOrder": 4, "CorrectAnswer": true },
    //        { "AnswerId": 0, "AnswerText": "Test Answer 4", "AnswerOrder": 4, "CorrectAnswer": true }
    //    ]
    //}
    //var testOSAPURL = "https://osapapi.contrajob.com/api/Questions";
    //$http.post(testOSAPURL, objDataOSAP, { headers: { 'Authorization': 'Bearer Om_JwT3G-aOwInL2NoKYQ6MoYC0Fzmz2IGGbYOEXJDEiUyx6rU6mbbql8I__LH5Q9bOIfyKM4msW7E7W_LMz_UykjOp-VUfdGD98Hw10kiliqO7sJK_OQn9_fb-IdyIQycm45TT46PII01_gvhbwFJ-Nvwp9OkAFn9pyMka7AnGjFj_23QDabgFB7_-JERimuydMVUEYgKakyWtLYP_0OclRwtZFW3HO7pA0rVLyoBSFAnZVoZyAV1dF9g8F1YzUnh8h90m7TQ5QKW7mqm5ofvr6SEsUNvhwJsnNhZCGvzENmxrFG_erNhusCvg1l-v4' } }).then(function (response) {
    //    console.log(response);
    //}, function (response) {
    //    console.log(response);
    //});

    //var testOSAPURL = "http://localhost:53459/api/Users/UserDetails";
    //$http.get(testOSAPURL, { headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer duxliN1i6Pggg9uPVV7vnphPbxRyuQlHTkdVn66aQQ5e-xfAG_HfFxrbCCuqSbv4bGKVOR2RlU69JLTrVFq5QjShe1xrp2BxWOpqxySkYHG1u3vfkDHaF3OaDbHfN4GEgaFXoAmLK06SmZ_xshj27krT2LemzuwARWDt57HMEx5fT4VFgO_DWehlyXxMEYKio1O5ByaEewuMCSPgsIIWvdTBZF0iWyBLVPfzocqfh4gaa4xMgvrUklsZTZ_2O0XE82lniemODqRD7xdmVFxOWRX459vcanBgZxrzINaazApjtFDfdbWj0LdJD1LnngUi' } }).then(function (response) {
    //    console.log(response);
    //}, function (response) {
    //    console.log(response);
    //});

    $(document).on('keydown', function (event) {
        if (event.key == "Escape") {
            //alert('Esc key pressed.');
            manageLoader()
        }
    });
}]);