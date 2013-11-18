define([], function () {
    var app = angular.module('EnjoyLearnApp',
        [
            'routeResolverServices',
            'localization',
            'ngResource',
            'ui.bootstrap',
            'ui.validate',
            '$strap',
            'smartTable.table'
        ]);

    app.config(
    [
        '$routeProvider',
        'routeResolverProvider',
        '$locationProvider',
        '$controllerProvider',
        '$compileProvider',
        '$filterProvider',
        '$provide',
        function ($routeProvider, routeResolverProvider, $locationProvider, $controllerProvider, $compileProvider, $filterProvider, $provide) {
            app.lazy =
            {
                controller: $controllerProvider.register,
                directive: $compileProvider.directive,
                filter: $filterProvider.register,
                factory: $provide.factory,
                service: $provide.service
            };

        }
    ]);


    app.run(function ($rootScope, $location, $window, events, EnjoyLearnContext) {

        var options = $window.options;
        if (options) {
            if (options.userSignedIn)
                EnjoyLearnContext.userSignedIn();
        }
        EnjoyLearnContext.initRatingLevel();
        EnjoyLearnContext.initUserProfile();
        EnjoyLearnContext.initDictionary();
        EnjoyLearnContext.initUserDictionary();

        $rootScope.$on('$routeChangeStart', function (e, next) {
            if (next.secured && !EnjoyLearnContext.isUserSignedIn()) {
                $location.path('/sign-in');
            }
        });

        events.on('signedUp', function () {
            showFlashSuccess('Thank you for signing up, an email with a ' +
                'confirmation link has been sent to your email address. ' +
                'Please open the link to activate your account.');
        });

        events.on('passwordResetRequested', function () {
            showFlashSuccess('An email with a password reset link has been ' +
                'sent to your email address. Please open the link to reset ' +
                'your password.');
        });

        events.on('signedIn', function () {

            showFlashSuccess('You are now signed in.');
            EnjoyLearnContext.userSignedIn({ load: true });
        });

        events.on('passwordChanged', function () {
            showFlashSuccess('Your password is successfully changed.');
        });

        events.on('signedOut', function () {
            showFlashSuccess('You are now signed out.');
            EnjoyLearnContext.userSignedOut();
            $location.path('/');
        });

        function showFlashSuccess(message) {
            events.trigger('flash:success', {
                message: message
            });
        }
    });

    return app;


});
