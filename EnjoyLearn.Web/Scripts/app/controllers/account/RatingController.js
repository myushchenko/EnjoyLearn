define(['appModule'], function (app) {
    app.controller('RatingController',
    [
        '$scope',
        '$http',
        '$dialog',
        'EnjoyLearnContext',
        'events',
        'EnjoyLearnAPI',
        'Validation',
        function (scope, $http, $dialog, context, events, api, validation) {
            scope.isShow = false;

            scope.points = 0;
            scope.pointsInLevel = 0;
            scope.pointsToNextLevel = 0;
            scope.pointsProc = 0;
            scope.pointsInLevelProc = 0;
            scope.ratingLevelName = "";

            events.on('OnUserProfileLoaded', function () {
                angular.forEach(context.ratingLevels, function (ratingLevel) {
                    if (ratingLevel.Id == context.userProfile.Points.NextLevelId) {
                        scope.pointsInLevel = ratingLevel.Points;
                    }
                    if (ratingLevel.Id == context.userProfile.Points.LevelId) {
                        scope.ratingLevelName = ratingLevel.Type;
                    }
                });
                
                scope.ranksNobilityName = context.userProfile.RanksNobility.MaleName;
                
                scope.points = context.userProfile.Points.Points;
                scope.pointsToNextLevel = scope.pointsInLevel - scope.points;

                scope.pointsProc = (scope.points * 100) / (scope.points + scope.pointsToNextLevel);
                scope.pointsInLevelProc = 100 - scope.pointsProc;
                scope.isShow = true;
            });

            events.on('OnAddPointsToProfile', function () {
                scope.profile = context.userProfile;
                scope.addedPoints = context.userProfile.Points.Points - scope.points;
                scope.profile
                    .$update(function (level) {
                        context.initUserProfile();
                        if (level.IsNewLevel) {

                            scope.optsAddWordDialog = {
                                backdrop: true,
                                keyboard: true,
                                backdropClick: true,
                                templateUrl: 'Scripts/app/views/dialogs/NewLevelDialog.html',
                                controller: 'NewLevelController',
                                resolve: { Level: function () { return angular.copy(level); } }
                            };

                            var dialog = $dialog.dialog(scope.optsAddWordDialog);
                            dialog.open().then(function (result) {
                                if (result) {
                                    alert('dialog closed with result: ' + result);
                                }
                            });

                        }
                        events.trigger('flash:success', {
                            message: 'You are got \"' + scope.addedPoints + '\" points.'
                        });

                    }, function (response) {
                        if (validation.hasModelErrors(response)) {
                            scope.modelErrors = response;// Validation.getModelErrors(response);
                            alert(JSON.stringify(scope.modelErrors));
                            return;
                        }
                        scope.modelErrors = response;/*['An unexpected error has occurred while ' +
                                'updating \"' + messageTitle + '\" account.'];*/
                    });
            });
            
            /*setInterval(function () {}, 1000);*/
        }
    ]);
});
