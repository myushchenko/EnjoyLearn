define(['appModule'], function (app) {
    app.controller('LoginController',
        
        function ($scope, $location, $dialog, $timeout, EnjoyLearnContext, events, EnjoyLearnAPI) {//}, $timeout, context, events) {
            //$scope.accounts = context.accounts;
            
           /* $scope.words = EnjoyLearnAPI.ExternalLogin.query(function (items) {
                alert(JSON.stringify(items));
            });*/

            $scope.isUserSignedIn = EnjoyLearnContext.isUserSignedIn();
            if ($scope.isUserSignedIn) {
                $scope.myAccountTitle = "My Profile";        
            } else {
                $scope.myAccountTitle = "Sign In";
            }
       
            $scope.optsLoginDialog = {
                backdrop: true,
                keyboard: true,
                backdropClick: true,
                dialogFade: true,
                templateUrl: 'login-dialog.html',
                controller: 'LoginDialogController'
            };
            $scope.optsProfileDialog = {
                backdrop: true,
                keyboard: true,
                backdropClick: true,
                dialogFade: true,
                templateUrl: 'profile-dialog.html',
                controller: 'ProfileDialogController'
            };

            events.on('signedIn', function () {
                $scope.isUserSignedIn = true;
                $scope.myAccountTitle = "My Profile";
            });
            events.on('signedUp', function () {
                $scope.isUserSignedIn = true;
                $scope.myAccountTitle = "My Profile";
            });
            events.on('signedOut', function () {
                $scope.isUserSignedIn = true;
                $scope.myAccountTitle = "My Profile";
            });
            
            events.on('signedIn', sync);
            events.on('signedOut', sync);

            function sync() {
                $timeout(function () {
                    //$scope.accounts = context.accounts;
                    if (!$scope.$$phase) {
                        $scope.$apply();
                    }
                }, 0);
            }
            
            $scope.myAccount = function () {
                
                var dialog = $dialog.dialog($scope.optsLoginDialog);
                if (EnjoyLearnContext.isUserSignedIn())
                    dialog = $dialog.dialog($scope.optsProfileDialog);
               
                
               
                    dialog.open().then(function (result) {
                    if (result) {
                        alert('dialog closed with result: ' + result);
                    }
                });
            };
            
            $scope.signOut = function () {
                $dialog.messageBox('Sign out?', 'Are you sure you want to sign out?', [
                    { label: 'Ok', result: true },
                    { label: 'Cancel', result: false, cssClass: 'btn-primary' }
                ]).
                    open().
                    then(function(result) {
                        if (result) {
                            signOut();
                        }
                    });
            };

            function signOut() {
                var session = new EnjoyLearnAPI.Session;
                session
                    .$delete(function() {
                        events.trigger('signedOut');
                        $scope.isUserSignedIn = false;
                        $scope.myAccountTitle = "Sign In";
                       // $location.path('/');
                    }, function() {
                        events.trigger('flash:error', {
                            message: 'An unexpected error has occurred while signing out.'
                        });
                    });
            }
        }
    );
});