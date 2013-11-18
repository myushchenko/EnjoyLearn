define(['appModule'], function (app) {
    app.controller('LanguageController', function ($scope, localize) {
        localize.setLanguage('en-US');
        $scope.lang = 'en';
        $scope.langTitle = '_EnglishLanguage_';
        $scope.setEnglishLanguage = function () {
            localize.setLanguage('en-US');
            $scope.lang = 'en';
            $scope.langTitle = '_EnglishLanguage_';
        };
        
        $scope.setUkraineLanguage = function () {
            localize.setLanguage('uk-UA');
            $scope.lang = 'ua';
            $scope.langTitle = '_UkraineLanguage_';
        };


        $scope.setRussianLanguage = function () {
            localize.setLanguage('ru-RU');
            $scope.lang = 'ru';
            $scope.langTitle = '_RussianLanguage_';
        };
    });
});