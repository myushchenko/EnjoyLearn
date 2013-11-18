define(['appModule'], function (app) {
    app.controller('ProfileDialogController', function ($scope, dialog, events) {
            $scope.close = function(result) {
                dialog.close(result);
            };
        
            events.on('passwordChanged', function () {
                dialog.close();
            });
        
            events.on('updateProfile', function () {
                dialog.close();
            });
        }
    );
});