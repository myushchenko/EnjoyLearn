

define(['appModule'], function (app) {
    app.lazy.controller('Game1Controller',
    [
        '$scope',
        'EnjoyLearnContext',
        function ($scope, context) {
            $scope.wordsToLearn = [];
            $scope.wordsShowed = [];
            $scope.words = [];

            $scope.name = 'Game one ☺';
            $scope.correctAnswers = 0;
            $scope.incorrectAnswers = 0;

            $scope.countAnswerToShow = 5;
            $scope.wordsToShow = 10;

            $scope.isExist = false;
            $scope.isShow = false;
            $scope.isFinish = false;

            $scope.words = context.userDictionaryWords;
            loadWords();

            $scope.onStart = function () {
                $scope.isShow = true;
                $scope.isFinish = false;
                $scope.correctAnswers = 0;
                $scope.incorrectAnswers = 0;
                $scope.countAnswerToShow = 5;
                $scope.wordsToShow = 10;
            };

            $scope.onClick = function (index) {

                if ($scope.randomWord.RussianWord == $scope.wordsToLearn[index].RussianWord) {
                    $scope.correctAnswers++;

                } else {
                    $scope.incorrectAnswers++;
                }
                if ($scope.wordsToShow > 1) {
                    loadWords();
                } else {
                    $scope.isShow = false;
                    $scope.isFinish = true;
                }
            };

            function loadWords() {
                $scope.wordsToLearn = [];
                $scope.isExist = false;
                for (var i = 0; i < $scope.countAnswerToShow; i++) {
                    var word = $scope.words[getRandomInt(0, $scope.words.length - 1)];

                    angular.forEach($scope.wordsToLearn, function (wordToLearn) {
                        if (wordToLearn == word) {
                            $scope.isExist = true;
                        }
                    });


                    $scope.wordsToLearn.push(word);
                }

                if ($scope.isExist) {
                    loadWords();
                } else {
                    $scope.randomWord = $scope.wordsToLearn[getRandomInt(0, $scope.wordsToLearn.length - 1)];
                    $scope.wordsToShow--;
                    removeUsedWords();
                }

            }

            function removeUsedWords() {
                angular.forEach($scope.wordsToLearn, function (wordToLearn) {
                    console.log('delete=>' + wordToLearn.EnglishWord);
                    $scope.words.splice(wordToLearn.index, 1);
                });
            }

            function getRandomInt(min, max) {
                return Math.floor(Math.random() * (max - min + 1)) + min;
            }

        }
    ]);
});
