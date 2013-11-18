define(['appModule'], function (app) {
    app.controller('ChangePasswordController', function ($scope
        , $location
        , $dialog
        , EnjoyLearnAPI
        , Validation
        , events) {

    $scope.password = {
        oldPassword: null,
        newPassword: null,
        confirmPassword: null
    };

    $scope.changePassword = function () {
        $scope.modelErrors = void (0);
        EnjoyLearnAPI.Password.change($scope.password).
            success(function () {
                events.trigger('passwordChanged');
                $location.path('/');
            }).
            error(function (response) {
                if (Validation.hasModelErrors(response)) {
                    $scope.modelErrors = Validation.getModelErrors(response);
                    return;
                }
                $scope.modelErrors = ['An unexpected error has occurred ' +
                    'while changing your password.'];
            });
    };

    });
});