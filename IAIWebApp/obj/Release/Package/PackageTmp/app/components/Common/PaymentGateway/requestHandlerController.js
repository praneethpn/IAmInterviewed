angular.module('IAMInterviewed').controller('requestHandlerController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', 'myService', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location, myService) {
    var workingKey = "BFA49CBAF6917F14495037E924FF0CEE";//put in the 32bit alpha numeric key in the quotes provided here 	
    var ccaRequest = "";
    var strEncRequest = "";
    var strAccessCode = "AVKJ06CH04BA50JKAB";// put the access key in the quotes provided here.
    console.log(strAccessCode);
    console.log(Request.form("nonseamless"));
    console.log(document.forms["nonseamless"]);
    $scope.encrypt = function (plainText, workingKey) {
        var m = crypto.createHash('md5');
        m.update(workingKey);
        var key = m.digest('binary');
        var iv = '\x00\x01\x02\x03\x04\x05\x06\x07\x08\x09\x0a\x0b\x0c\x0d\x0e\x0f';
        var cipher = crypto.createCipheriv('aes-128-cbc', key, iv);
        var encoded = cipher.update(plainText, 'utf8', 'hex');
        encoded += cipher.final('hex');
        return encoded;
    };

    $(document).ready(function () {
        $timeout(function () {
            $("#nonseamless").submit();
        });
    });
}]);