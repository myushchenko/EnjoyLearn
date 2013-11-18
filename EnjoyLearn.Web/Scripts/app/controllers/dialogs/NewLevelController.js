define(['appModule'], function (app) {
    app.controller('NewLevelController', [
        '$scope',
        'dialog',
        'EnjoyLearnAPI',
        'EnjoyLearnContext',
        'Validation',
        'events',
         'Level',
        function (scope, dialog, enjoyLearnApi, enjoyLearnContext, validation, events, level) {

            scope.Title = "Congratulations!";


            scope.LevelName = level.NewLevelName;

            scope.NewPoints = level.NextLevelPoints;
            
            scope.close = function (result) {
                dialog.close(result);
            };
        }
    ]);
});