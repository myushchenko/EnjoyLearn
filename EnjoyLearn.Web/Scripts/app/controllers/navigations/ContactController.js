define(['appModule'], function (app) {
    app.lazy.controller('ContactController',
    [
        '$scope',

        function ($scope) {
            $scope.page =
            {
                heading: 'myu@sitecore.net'
            };
        }
    ]);
});