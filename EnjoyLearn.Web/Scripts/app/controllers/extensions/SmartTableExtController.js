define(['appModule'], function (app) {
    app.controller('SmartTableExtController',
    [
        '$scope',
        '$http',
        '$dialog',
        'EnjoyLearnContext',
        'Validation',
        'events',
        'EnjoyLearnAPI',
        '$route',
        function (scope, $http, $dialog, context, validation, events, enjoyLearnApi, route) {
            
            scope.isEditWord = false;
            
            if (route.current.scope.name == 'My Vocabulary') {
                scope.isDeletWord = true;
            }
            if (route.current.scope.name == 'Dictionary') {
                scope.isAddToUserDictionary = true;
                angular.forEach(context.userDictionaryWords, function (userDictionaryWord) {
                    if (scope.dataRow.EnglishWord == userDictionaryWord.EnglishWord) {
                        scope.isAddToUserDictionary = false;
                        scope.isAddOk = true;
                    }
                });
            }
            

            scope.onAddToUserDictionary = function () {           
                scope.word = new enjoyLearnApi.UserDictionary();
                scope.word.DictionaryId = scope.dataRow.Id;
            
               scope.word
               .$create(function () {
                   context.userDictionaryWords.push(scope.dataRow);
                   scope.isAddToUserDictionary = false;
                   scope.isAddOk = true;
                   events.trigger('flash:success', {
                       message: 'New word  "' +
                        scope.dataRow.EnglishWord + '" was added.'
                   });
               }, function (response) {
                   if (validation.hasModelErrors(response)) {
                       events.trigger('flash:error', {
                           message: validation.getModelErrors(response)
                       });     
                       return;
                   }
                   events.trigger('flash:error', {
                       message: response /*['An unexpected error has occurred ' +
                   'while creating new account.'];*/
                   });
               });
    
            };
            
            scope.onEditWord = function () {

                scope.optsAddWordDialog = {
                    backdrop: true,
                    keyboard: true,
                    backdropClick: true,
                    templateUrl: 'Scripts/app/views/dialogs/AddEditVocabularyDialog.html',
                    controller: 'EditWordDialogController',
                    resolve: { item: function () { return angular.copy(scope.dataRow); } }
                };

                var dialog = $dialog.dialog(scope.optsAddWordDialog);
                dialog.open().then(function (result) {
                    if (result) {
                        alert('dialog closed with result: ' + result);
                    }
                });
            };
            
            scope.onDeleteWord = function () {
                $dialog.messageBox('Delete?'
                    , 'Are you sure you want to delete "' +
                        scope.dataRow.EnglishWord + '" word?', [
                    { label: 'Ok', result: true },
                    { label: 'Cancel', result: false, cssClass: 'btn-primary' }
                        ]).
                    open().
                    then(function (result) {
                        if (result) {
                            destroy(scope.dataRow);
                        }
                    });
            };

            function destroy(word) {         
                var title = word.EnglishWord;
                var index = context.userDictionaryWords.indexOf(word);
                word.id = word.DictionaryId;
                word.$delete(function () {
                    context.userDictionaryWords.splice(index, 1);
                    events.trigger('flash:success', {
                        message: 'The word \"' + title + '\" was deleted.'
                    });
                }, function (response) {
                    var message = 'An unexpected error has occurred while deleting \"' +
                        title + '\" word.';

                    if (validation.hasModelErrors(response)) {
                        var modelErrors = validation.getModelErrors(response);
                        if (modelErrors) {
                            message = modelErrors[0];
                        }
                    }
                    events.trigger('flash:error', {
                        message: message
                    });
                });
            }
        }
    ]);
});
