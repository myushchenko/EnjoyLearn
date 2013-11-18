define(['appModule'], function (app) {
    app.controller('ForgotPasswordController', function ($scope
        , $location
        , EnjoyLearnAPI
        , events) {

        $scope.password = {
            email: null
        };

        $scope.submit = function () {
            $scope.modelErrors = void (0);
            EnjoyLearnAPI.Password.forgot($scope.password)
                .success(function () {
                    events.trigger('passwordResetRequested');
                }).
                error(function (response) {
                    $scope.modelErrors = response;/*['An unexpected error has occurred ' +
                        'while requesting password reset.'];*/
                });
        };
    });
});