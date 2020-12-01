angular.module("IAMInterviewed").controller('LoginController', ['$scope', '$http', '$q', '$filter', '$rootScope', '$timeout', '$window', '$localStorage', function ($scope, $http, $q, $filter, $rootScope, $timeout, $window, $localStorage, $location) {
    document.cookie = "sessionId=;secure;HttpOnly;"
    delete $localStorage.sessionId;
    delete $localStorage.userid;
    delete $localStorage.UserSessionObject;
    $localStorage.RedirectKey = 'Dashboard';
    $scope.ShowForgotPassword = false;
    $scope.ClearAll = function () {
        $scope.Username = "";
        $scope.Password = "";
        $scope.forgotEmailAddress = "";
    }
    $scope.ClearAll();


    $scope.SignIn = function () {
        manageLoader('load');
        var objLogin = {
            username: $scope.Username,
            password: $scope.Password
        }
        //var AuthenticateUrl = IAMInterviewed.userManagent.authenticateUser + "?Username=" + $scope.Username + "&Password=" + $scope.Password;
        var AuthenticateUrl = IAMInterviewed.userManagent.authenticateUser;
        $scope.errorMessage = '';
        delete $localStorage.token;
        $http.post(AuthenticateUrl, objLogin).then(function success(data) {
            var resultData = data.data;
            //console.log(resultData);
            if (resultData.Success == true) {
                var userid = $scope.Username;
                var d = new Date();
                d.setTime(d.getTime() + (5));
                var expires = "expires=" + d.toUTCString();
                var sessionId = "";
                document.cookie = "sessionId=" + userid + "!" + sessionId.toString() + ";" + expires + ";secure; HttpOnly;";
                $localStorage.sessionId = $scope.Username + "!" + sessionId.toString();
                $localStorage.userid = userid;
                $localStorage.UserSessionObject = resultData.data;
                //Load the values into Session
                var data = { sessionId: sessionId.toString(), sessionAttributes: [{ name: "Username", value: userid + " " + userid }, { name: "UserId", value: userid }] };
                $rootScope.loggedIn = true;
                //$localStorage.CurrentPage = 'Home';
                //console.log(resultData);
                manageLoader();
                if (resultData.data.RoleName == "Candidate") {
                    $window.location.href = '/Candidate.html';
                }
                else if (resultData.data.RoleName == "Interviewer") {
                    $window.location.href = '/Interviewer.html';
                }
                else if (resultData.data.RoleName == "Company") {
                    $window.location.href = '/Company.html';
                }
            }
            else {
                $rootScope.resultMessage = IAMInterviewed.ConfigurationSettings.LoginErrorMessage;
                showNotification('error');
                manageLoader();
                return false;
            }
        },
        function error(data) {
            $rootScope.resultMessage = IAMInterviewed.ConfigurationSettings.LoginErrorMessage;
            showNotification('error');
            manageLoader();
            return false;
        });
    }

    $scope.SendPassword = function () {
        manageLoader('load');
        var forgotPasswordURL = IAMInterviewed.userManagent.forgotPassword + "?EmailAddress=" + $scope.forgotEmailAddress;
        $http.get(forgotPasswordURL).then(function success(response) {
            //console.log(response.data);
            $rootScope.resultMessage = "Password would be sent to the email entered. Please check email.";
            showNotification('success');
            manageLoader();
        },
        function error(response) {
            $rootScope.resultMessage = response.data.errorMessage;
            showNotification('error');
            manageLoader();
            return false;
        });
        $scope.ShowForgotPassword = false;
    }

    $(document).ready(function () {
        $("span.field-validation-valid[data-valmsg-for*=Password1]").hide();
    });

    function SubmitsEncry(event) {
        var txtUserName = $('#UserName').val();
        var txtpassword = $('#Password1').val();

        if (txtUserName == "") {
            if (txtpassword == "") {
                $("span.field-validation-valid[data-valmsg-for*=Password1]").show();
                $("span.field-validation-valid[data-valmsg-for*=Password1]").removeClass("field-validation-valid").addClass("field-validation-error");
            }

            return false;
        }
        else if (txtpassword == "") {
            $("span.field-validation-valid[data-valmsg-for*=Password1]").show();
            $("span.field-validation-valid[data-valmsg-for*=Password1]").removeClass("field-validation-valid").addClass("field-validation-error");
            event.preventDefault();
            return false;
        }
        else {
            //// var key = CryptoJS.enc.Utf8.parse('8080808080808080');
            //// var iv = CryptoJS.enc.Utf8.parse('8080808080808080');
            var CipherPrivateKey = 3263335010801581;
            var key = CryptoJS.enc.Utf8.parse(CipherPrivateKey);
            var iv = CryptoJS.enc.Utf8.parse(CipherPrivateKey);

            //// var encryptedlogin = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtUserName), key, { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });
            //// $('#HashedUserName').val(encryptedlogin);

            var encryptedpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtpassword), key, { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });
            $('#HashedPassword').val(encryptedpassword);
        }
        $("#loginform").submit();
    }

    $(document).on('keydown', function (event) {
        if (event.key == "Escape") {
            //alert('Esc key pressed.');
            manageLoader()
        }
        else if (event.keyCode === 13) {
            //alert('Esc key pressed.');
            $scope.SignIn();
        }
    });
}]);