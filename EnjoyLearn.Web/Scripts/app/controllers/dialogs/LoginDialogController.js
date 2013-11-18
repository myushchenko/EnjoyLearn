define(['appModule'], function (app) {
    app.controller('LoginDialogController', function ($scope, dialog, events) {
            $scope.close = function(result) {
                dialog.close(result);
            };
        
            events.on('signedIn', function () {
                dialog.close();
            });
        
            events.on('signedUp', function () {
                dialog.close();
            });
        
            events.on('externalLogin', function () {
                dialog.close();
            });
            events.on('passwordResetRequested', function () {
                dialog.close();
            });
    
       
        }
    );
});