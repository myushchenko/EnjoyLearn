define(['appModule'], function (app) {
    app.lazy.controller('DictionaryController',
    [
        '$scope',
        '$http',
        '$dialog',
        'EnjoyLearnContext',
        'Validation',
        'events',
        'EnjoyLearnAPI',
        function (scope, $http, $dialog, context, validation, events, enjoyLearnApi) {
            scope.name = 'Dictionary';
            scope.loading = true;
            scope.words = [];

            var init = function () {
                scope.loading = false;
                scope.rowCollection = context.dictionaryWords;
                for (var i = 0; i < scope.rowCollection.length; i++) {
                    var obj = scope.rowCollection[i];
                    obj.id = obj.Id;
                }
                
                scope.columnCollection = [
                    { label: 'English Word', map: 'EnglishWord', isSortable: true, isEditable: true },
                    { label: 'Transcription', map: 'TranscriptionWord', isEditable: true },
                    { label: 'Russian', map: 'RussianWord', isSortable: true, isEditable: true },
                    { cellTemplateUrl: 'Scripts/app/directives/templates/SmartTableExtColumn.html' }
                ];

                scope.$on('updateDataRow', function (event, args) {
                    alert(JSON.stringify(args));
                });

                scope.$on('click', function (event, args) {
                    alert(JSON.stringify(args));
                });

                scope.globalConfig = {
                    isPaginationEnabled: true,
                    isGlobalSearchActivated: true,
                    itemsByPage: 12,
                    maxSize: 10,
                    selectionMode: 'single'
                };
            };

            init();
    
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
            
            scope.onAddNewWord = function () {

                scope.optsAddWordDialog = {
                    backdrop: true,
                    keyboard: true,
                    backdropClick: true,
                    templateUrl: 'Scripts/app/views/dialogs/AddEditVocabularyDialog.html',
                    controller: 'AddWordDialogController'
                };

                var dialog = $dialog.dialog(scope.optsAddWordDialog);
                dialog.open().then(function (result) {
                    if (result) {
                        alert('dialog closed with result: ' + result);
                    }
                });
            };
            
            scope.deleteWord = function () {
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
                var index = context.words.indexOf(word);

                word.$delete(function () {
                    context.words.splice(index, 1);
                    events.trigger('flash:success', {
                        message: '\"' + title + '\" word deleted.'
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
