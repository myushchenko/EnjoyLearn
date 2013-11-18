define(['appModule'], function (app) {
    app.controller('SignInController', function ($scope
        , $location
        , EnjoyLearnAPI
        , Validation
        , events) {

        $scope.session = new EnjoyLearnAPI.Session;
    $scope.isShow = true;
    $scope.submit = function () {
        
        $scope.modelErrors = void(0);
        $scope.session
            .$save(function() {
                events.trigger('signedIn');
                
                $scope.isShow = false;
                //$location.path('/');
            }, function(response) {
                var error = Validation.hasModelErrors(response) ?
                    'Invalid credentials.' :
                    'An unexpected error has occurred while signing in.';
                $scope.modelErrors = [error];
            });
    };
    });
});