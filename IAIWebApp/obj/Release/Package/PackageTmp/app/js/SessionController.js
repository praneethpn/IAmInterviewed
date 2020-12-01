//var app = angular.module('IAMInterviewed', ['ngStorage', 'ngIdle']);
angular.module('IAMInterviewed').controller('SessionController', function ($scope, $http, $rootScope, $localStorage, $location, $window, Idle) {
    //angular.module('IAMInterviewed',['ngStorage', 'ngIdle'])
    ////app
    //    .controller('SessionHandler', [function ($scope) {

    $scope.events = [];
    $scope.idle = 21600;     //permitted idle time for user
    $scope.timeout = 5;  //warn and denotes time taken to log out

    $scope.$on('IdleStart', function () {
        addEvent({ event: 'IdleStart', date: new Date() });
    });

    $scope.$on('IdleEnd', function () {
        addEvent({ event: 'IdleEnd', date: new Date() });
    });

    $scope.$on('IdleWarn', function (e, countdown) {
        addEvent({ event: 'IdleWarn', date: new Date(), countdown: countdown });
    });

    $scope.$on('IdleTimeout', function () {
        addEvent({ event: 'IdleTimeout', date: new Date() });
        //Log Out when timed out after being idle for specified minutes
        delete $localStorage.sessionId;
        delete $localStorage.userid;
        $window.location.href = '/Index.html';
    });

    $scope.$on('Keepalive', function () {
        addEvent({ event: 'Keepalive', date: new Date() });
    });

    function addEvent(evt) {
        $scope.$evalAsync(function () {
            $scope.events.push(evt);
        })
    }

    $scope.reset = function () {
        Idle.watch();
    }

    $scope.$watch('idle', function (value) {
        if (value !== null) Idle.setIdle(value);
    });

    $scope.$watch('timeout', function (value) {
        if (value !== null) Idle.setTimeout(value);
    })


})
    .config(function (IdleProvider, KeepaliveProvider) {
        KeepaliveProvider.interval(5);
        IdleProvider.windowInterrupt('focus');
    })
    .run(function ($rootScope, Idle, $log, Keepalive) {
        Idle.watch();

        $log.debug('app started.');
    });