define(['appModule'], function (app) {
    app.lazy.controller('AboutController',
    [
        '$scope',

        function ($scope) {
            $scope.page =
            {
                heading: "I'm Michael Yushchenko"
            };
        }
    ]);
});