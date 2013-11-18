define(['appModule'], function (app) {
    app.controller('EditWordDialogController',
        [
        '$scope',
        'Resource',
        'Validation',
        'dialog',
        'events',
        'item',
        function ($scope, resource, validation, dialog, events, item) {
            $scope.Title = "Edit word";
            var messageTitle = item.EnglishWord;
            $scope.word = resource.copy(item);

            $scope.close = function (result) {
                dialog.close(result);
            };

            $scope.submit = function () {
                $scope.modelErrors = void (0);
                resource.merge($scope.word, item);
                item
                    .$update(function () {
                        events.trigger('flash:success', {
                            message: '\"' + messageTitle + '\" account updated.'
                        });
                        dialog.close();
                    }, function (response) {
                        if (validation.hasModelErrors(response)) {
                            $scope.modelErrors = response;// Validation.getModelErrors(response);
                            alert(JSON.stringify($scope.modelErrors));
                            return;
                        }
                        $scope.modelErrors = response;/*['An unexpected error has occurred while ' +
                                'updating \"' + messageTitle + '\" account.'];*/
                    });
            };
        }
        ]);
});