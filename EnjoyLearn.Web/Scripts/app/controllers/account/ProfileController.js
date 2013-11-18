define(['appModule'], function (app) {
    app.controller('ProfileController',
    [
        '$scope',
        '$http',
        '$dialog',
        'EnjoyLearnContext',
        'Validation',
        'events',
        'Resource',
        
        function (scope, $http, $dialog, context, validation, events, resource) {

            scope.profile = context.userProfile;
            //alert(JSON.stringify(context.profiles[0]));
            //alert(JSON.stringify(context.userProfile.Points.Points));

            var messageTitle = scope.profile.FirstName;///scope.profile.FirstName;

            //$scope.word = resource.copy(item);
            
            
            scope.submit = function () {
                scope.modelErrors = void (0);
                //resource.merge($scope.word, item);
                scope.profile
                    .$update(function () {
                        events.trigger('flash:success', {
                            message: '\"' + messageTitle + '\" account updated.'
                        });
                        events.trigger('updateProfile');
                        //dialog.close();
                    }, function (response) {
                        if (validation.hasModelErrors(response)) {
                            scope.modelErrors = response;// Validation.getModelErrors(response);
                            alert(JSON.stringify($scope.modelErrors));
                            return;
                        }
                        scope.modelErrors = response;/*['An unexpected error has occurred while ' +
                                'updating \"' + messageTitle + '\" account.'];*/
                   });
            };
    
        }
    ]);
});
