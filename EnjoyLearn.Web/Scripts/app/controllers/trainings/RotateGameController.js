define(['appModule'], function (app) {
    app.lazy.controller('RotateGameController',
    [
        '$scope',
        'EnjoyLearnContext',
        function ($scope, context) {

            $scope.wordsToLearn = [];
            $scope.wordsToLearnShow = [];
            $scope.words = [];
            $scope.wordsToShow = 2;

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
                // alert('words=>'+$scope.words.length);

                $scope.wordsToLearn = [];
                var divRotateId = 0;
                for (var i = 0; i < $scope.wordsToShow; i++) {
                    var word = $scope.words[getRandomInt(0, $scope.words.length - 1)];
                    //$scope.wordsToLearn.push(word);
                    // if (i < $scope.wordsToShow / 2) {
                    $scope.wordsToLearn.push({
                        Id: 'divRotate' + ++divRotateId,
                        EnglishWord: word.EnglishWord,
                        RussianWord: word.RussianWord,
                        RundomWord: word.EnglishWord,
                    });
                    // } else {
                    $scope.wordsToLearn.push({
                        Id: 'divRotate' + ++divRotateId,
                        EnglishWord: word.EnglishWord,
                        RussianWord: word.RussianWord,
                        RundomWord: word.RussianWord,
                    });
                    // }



                }
                // alert(JSON.stringify($scope.wordsToLearn));
                // alert($scope.wordsToLearn.length);

                $scope.wordsToLearnShow = [];
                angular.forEach($scope.wordsToLearn, function (word2) {
                    $scope.wordsToLearnShow.push(word2);
                });
                // $scope.wordsToLearnShow = randomSortArray($scope.wordsToLearnShow, 3);
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

            // $scope.circleClass = 'click panel circle';
            $scope.prevCircle = null;
            $scope.onCircleClick = function (index) {
                //alert(JSON.stringify($scope.wordsToLearnShow[index].Id));
                //alert($('#div' + id).hasClass('flip'));

                var divId = '#' + $scope.wordsToLearnShow[index].Id;
                if ($scope.prevCircle != null) {
                    if ($scope.prevCircle != $(divId)) {
                        if ($scope.prevCircleObject.EnglishWord != $scope.wordsToLearnShow[index].EnglishWord) {
                            $scope.prevCircle.removeClass('flip');
                        }
                    } else {
                        $scope.prevCircle = $(divId);
                        $scope.prevCircleObject = $scope.wordsToLearnShow[index];
                    }
                } else {
                    $scope.prevCircle = $(divId);
                    $scope.prevCircleObject = $scope.wordsToLearnShow[index];
                }
                //$('div.circle').removeClass('flip');
                if ($(divId).hasClass('flip')) {
                    $(divId).removeClass('flip');
                } else {
                    $(divId).addClass('flip');
                }

               
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
