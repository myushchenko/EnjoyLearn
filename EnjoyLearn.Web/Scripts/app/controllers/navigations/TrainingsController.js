define(['appModule'], function (app) {
    app.lazy.controller('TrainingsController',
    [
        '$scope',

        function ($scope) {
            $scope.games =
            {
                title1: "First Game",
                title2: "Second Game",
                title3: "Third Game",
                title4: "Rotate Game"
            };
            

        }
    ]);
});