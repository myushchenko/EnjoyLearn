define(['appModule'], function (app) {
    app.controller('SignUpController', function ($scope
        , $location
        , EnjoyLearnAPI
        , Validation
        , events) {

        $scope.user = new EnjoyLearnAPI.User;

        //alert(JSON.stringify($scope.user));
    $scope.submit = function() {
        $scope.modelErrors = void(0);
        $scope.user
            .$save(function () {
                events.trigger('signedUp');
                $location.path('/');
            }, function (response) {
                if (Validation.hasModelErrors(response)) {
                    try {
                        $scope.modelErrors = Validation.getModelErrors(response);
                    } catch (e) {
                        alert(e);
                    }
                    return;
                }
                $scope.modelErrors = response;/*['An unexpected error has occurred ' +
                    'while signing up.'];*/
            });
    };
    });
});