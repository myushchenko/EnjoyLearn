define(['appModule'], function (app) {
    app.lazy.controller('Game2Controller',
    [
        '$scope',
        'EnjoyLearnContext',
        'events',
        function ($scope, context, events) {
            $scope.wordsToLearn = [];
            $scope.words = [];
            $scope.wordsToShow = 10;

            $scope.name = 'Game second ☺';
            $scope.correctAnswers = 0;
            $scope.incorrectAnswers = 0;
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
                $scope.wordsToShow = 10;
            };

            $(document).keyup(function (event) {
                // handle cursor keys
                if ($scope.wordsToShow >= 1) {
                    if (event.keyCode == 37) {
                        $scope.$apply(function () {
                            $scope.onClick(false);
                        });
                    } else if (event.keyCode == 39) {
                        $scope.$apply(function () {
                            $scope.onClick(true);
                        });
                    }
                }
            });

            $scope.onClick = function (type) {
                //alert(type);
                
                
                if ($scope.randomWordFirst == $scope.randomWordSecond && type == true) {
                    $scope.correctAnswers++;

                } else if ($scope.randomWordFirst != $scope.randomWordSecond && type == false) {
                    $scope.correctAnswers++;

                } else {
                    $scope.incorrectAnswers++;
                }
                if ($scope.wordsToShow > 1) {
                    loadWords();
                } else {
                    
                    $scope.isShow = false;
                    $scope.isFinish = true;
                  
                    context.userProfile.Points.Points = context.userProfile.Points.Points + 5;
                    events.trigger('OnAddPointsToProfile');
                }
                

                 
            };

            function loadWords() {

                $scope.wordsToLearn = [];
                $scope.isExist = false;
                for (var i = 0; i < 2; i++) {
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
                    $scope.randomWordFirst = $scope.wordsToLearn[getRandomInt(0, $scope.wordsToLearn.length - 1)];
                    $scope.randomWordSecond = $scope.wordsToLearn[getRandomInt(0, $scope.wordsToLearn.length - 1)];
                    $scope.wordsToShow--;
                    removeUsedWords();
                }
            }

            function removeUsedWords() {
                angular.forEach($scope.wordsToLearn, function (wordToLearn) {
                   // console.log('delete=>' + wordToLearn.EnglishWord);
                   // $scope.words.splice(wordToLearn.index, 1);
                });
            }

            function getRandomInt(min, max) {
                return Math.floor(Math.random() * (max - min + 1)) + min;
            }
        }
    ]);
});
