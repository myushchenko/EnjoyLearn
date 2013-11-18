define(['appModule'], function (app) {
    app.controller('AddWordDialogController', [
        '$scope',
        'dialog',
        'EnjoyLearnAPI',
        'EnjoyLearnContext',
        'Validation',
        'events',
        function ($scope, dialog, enjoyLearnApi, enjoyLearnContext, validation, events) {
            
        $scope.Title = "Add word";
        $scope.word = new enjoyLearnApi.Dictionary;

        $scope.submit = function () {

            $scope.modelErrors = void (0);
            $scope.word
           .$create(function () {
               enjoyLearnContext.dictionaryWords.push($scope.word);
               events.trigger('AddWordDialog');
               events.trigger('flash:success', {
                   message: 'New word was added.'
               });
               dialog.close();
           }, function (response) {
               if (validation.hasModelErrors(response)) {
                   $scope.modelErrors = validation.getModelErrors(response);
                   return;
               }
               $scope.modelErrors = response;/*['An unexpected error has occurred ' +
                   'while creating new account.'];*/
           });
        };

        $scope.close = function (result) {
            dialog.close(result);
        };
        }
    ]);
});