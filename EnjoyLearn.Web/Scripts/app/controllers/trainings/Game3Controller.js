define(['appModule'], function (app) {
    app.lazy.controller('Game3Controller',
    [
        '$scope',
        'EnjoyLearnContext',
        function ($scope, context) {
            
            $scope.wordsToLearn = [];
            $scope.wordsToLearnShow = [];
            $scope.words = [];
            $scope.wordsToShow = 5;

            $scope.name = 'Game third ☺';
            $scope.correctAnswers = 0;
            $scope.incorrectAnswers = 0;
            $scope.isShow = false;
            $scope.isFinish = false;

            $scope.wordSelected1 = '';
            $scope.wordSelected2 = '';
            $scope.wordSelected3 = '';
            $scope.wordSelected4 = '';
            $scope.wordSelected5 = '';
            
            $scope.words = context.userDictionaryWords;
           
            function loadWords() {
                $scope.wordsToLearn = [];
                for (var i = 0; i < $scope.wordsToShow; i++) {
                    var word = $scope.words[getRandomInt(0, $scope.words.length - 1)];
                    angular.forEach($scope.wordsToLearn, function (wordToLearn) {
                    });
                    word.isDisabled = false;
                    $scope.wordsToLearn.push(word);
                }

                $scope.wordsToLearnShow = [];
                angular.forEach($scope.wordsToLearn, function (word2) {
                    $scope.wordsToLearnShow.push(word2);
                });
                $scope.wordsToLearnShow = randomSortArray($scope.wordsToLearnShow, 3);
            }

            loadWords();

            $scope.onStart = function () {
                $scope.isShow = true;
                $scope.isFinish = false;
                $scope.correctAnswers = 0;
                $scope.incorrectAnswers = 0;
                $scope.wordsToShow = 5;
            };

            $(document).keyup(function (event) {
                // handle cursor keys
                if (!$scope.isFinish) {
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


            $scope.$watch(
                    "wordSelected1",
                    function (value) {
                        //alert(value);
                        /* // No "valid" value was selected.
                        if (value === "hrule") {

                            // Reset the selection
                            $scope.selection = null;

                            // A random friend was selected.
                        } else if (value === "random") {

                            var index = Math.floor(Math.random() * 3);

                            $scope.selection = $scope.friends[index];

                            // NULL or an actual friend was selected.
                            // In either case, we can use the selection
                            // value as our selected friend.
                        } else {

                            $scope.selectedFriend = $scope.selection;

                        }*/

                    }
                );
            
            $scope.onClick = function () {

               // alert($scope.wordSelected1.value);

                $scope.correctAnswers = 0;
                $scope.incorrectAnswers = 0;
                if ($scope.wordsToLearnShow[0] == $scope.wordSelected1) {
                    $scope.wordsToLearnShow[0].color = 'success';
                    $scope.correctAnswers++;
                } else {
                    $scope.wordsToLearnShow[0].color = 'error';
                    $scope.incorrectAnswers++;
                }
                if ($scope.wordsToLearnShow[1] == $scope.wordSelected2) {
                    $scope.wordsToLearnShow[1].color = 'success';
                    $scope.correctAnswers++;
                } else {
                    $scope.wordsToLearnShow[1].color = 'error';
                    $scope.incorrectAnswers++;
                }
                if ($scope.wordsToLearnShow[2] == $scope.wordSelected3) {
                    $scope.wordsToLearnShow[2].color = 'success';
                    $scope.correctAnswers++;
                } else {
                    $scope.wordsToLearnShow[2].color = 'error';
                    $scope.incorrectAnswers++;
                }               
                if ($scope.wordsToLearnShow[3] == $scope.wordSelected4) {
                    $scope.wordsToLearnShow[3].color = 'success';
                    $scope.correctAnswers++;
                } else {
                    $scope.wordsToLearnShow[3].color = 'error';
                    $scope.incorrectAnswers++;
                }
                if ($scope.wordsToLearnShow[4] == $scope.wordSelected5) {
                    $scope.wordsToLearnShow[4].color = 'success';
                    $scope.correctAnswers++;
                } else {
                    $scope.wordsToLearnShow[4].color = 'error';
                    $scope.incorrectAnswers++;
                }
                    
                $scope.wordSelected1.isDisabled = true;
                $scope.wordSelected2.isDisabled = true;
                $scope.wordSelected3.isDisabled = true;
                $scope.wordSelected4.isDisabled = true;
                $scope.wordSelected5.isDisabled = true;

            };

         
            function randomSortArray(object, countRepeat) {        
                angular.forEach(object, function () {
                    var i = getRandomInt(0, object.length - 1);
                    var j = getRandomInt(0, object.length - 1);
                    var temp = object[i];
                    object[i] = object[j];
                    object[j] = temp;
                });
                if (countRepeat > 0)
                    randomSortArray(object, countRepeat - 1);
                return object;
            }
            
            function getRandomInt(min, max) {
                return Math.floor(Math.random() * (max - min + 1)) + min;
            }
        }
    ]);
});
