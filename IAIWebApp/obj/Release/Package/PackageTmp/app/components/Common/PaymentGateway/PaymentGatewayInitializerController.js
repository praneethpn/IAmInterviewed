angular.module('IAMInterviewed').controller('PaymentGatewayInitializerController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', 'myService', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location, myService) {
    console.log($localStorage.CandidateIdPayment);
    var workingKey = "BFA49CBAF6917F14495037E924FF0CEE";//put in the 32bit alpha numeric key in the quotes provided here 	
    var ccaRequest = "";
    var strEncRequest = "";
    var strAccessCode = "AVKJ06CH04BA50JKAB";// put the access key in the quotes provided here.
    $scope.getCandidateProfile = function () {
        var getCandidateProfileURL = IAMInterviewed.Candidate.getCandidateProfileForCall + "?CandidateId=" + $localStorage.CandidateIdPayment;
        $http.get(getCandidateProfileURL).then(function success(response) {
            $("#billing_name").val(response.data.data[0].CandidateName);
            $("#billing_tel").val(response.data.data[0].Email);
            $("#billing_email").val(response.data.data[0].Mobile);
            var formPayment = $("#nonseamlessRequest").serialize();
            //$scope.source_string = 'Adam,John';
            //console.log('source String = ' + $scope.source_string);
            //$rootScope.base64Key = CryptoJS.enc.Hex.parse('0123456789abcdef0123456789abcdef')
            //$rootScope.iv = CryptoJS.enc.Hex.parse('abcdef9876543210abcdef9876543210');
            //var encrypted = CryptoJS.AES.encrypt(
            //    formPayment,
            //    workingKey,
            //    { iv: $rootScope.iv }
            //);
            //console.log(formPayment);
            strEncRequest = $scope.encrypt(formPayment, workingKey);
            $("#encRequest").val(strEncRequest);
            $("#access_code").val(strAccessCode);
            var encRequestBeforePost = $("#encRequest").val();
            var encRequestReplacing = encRequestBeforePost.split('+').join('');
            $("#encRequest").val(encRequestReplacing);
            //$("#nonseamless").submit();
            console.log(encRequestBeforePost);
            console.log(encRequestReplacing);
        }, function error(response) {
        });
    }
    if ($localStorage.CandidateIdPayment == undefined || $localStorage.CandidateIdPayment == '' || $localStorage.CandidateIdPayment == null) {
        $window.location.href = '/Candidate.html';
    }
    else {
        $scope.getCandidateProfile();
    }

    $scope.encrypt = function (plainText, workingKey) {
        $rootScope.base64Key = CryptoJS.enc.Hex.parse(workingKey)
        $rootScope.iv = CryptoJS.enc.Hex.parse('\x00\x01\x02\x03\x04\x05\x06\x07\x08\x09\x0a\x0b\x0c\x0d\x0e\x0f');
        var encrypted = CryptoJS.AES.encrypt(plainText, $rootScope.base64Key, { iv: $rootScope.iv })
        //.createHash('md5');
        //m.update(workingKey);
        //var key = m.digest('binary'); 
        //$scope.ciphertext = encrypted.ciphertext.toString(CryptoJS.enc.Base64);
        //var cipher = CryptoJS.createCipheriv('aes-128-cbc', key, iv);
        //var encoded = cipher.update(plainText, 'utf8', 'hex');
        //encoded += cipher.final('hex');
        return encrypted;
    };

    $(document).ready(function () {
        $timeout(function () {

        });
    });
}]);