define(['appModule'], function (app) {
    app.lazy.controller('404Controller',
    [
        '$scope',

        function ($scope) {
            $scope.page =
            {
                heading: 'You got an error!!!'
            };
        }
    ]);
});