define(['appModule'], function (app) {
    app.service('EnjoyLearnContext',
        [
        'EnjoyLearnAPI',
             'events',
            function (api, events) {
                var userSignedIn = false;
                this.dictionaryWords = [];
                this.userDictionaryWords = [];
                this.userChatMessages = [];
                this.userPrivateChatMessages = [];               
                this.chatUsers = [];
                this.chatCurrentUser = [];
                
                this.ratingLevels = [];
                
                this.userProfile = null;
                
                this.isUserSignedIn = function () {
                    return userSignedIn;
                };

                this.userSignedIn = function (options) {
                    userSignedIn = true;
                    if (options && options.load) {
                        // this.words = ServerAPI.LearnWord.query();
                        // alert(JSON.stringify(his.words));
                    }
                };

                this.userSignedOut = function () {
                    userSignedIn = false;
                    this.userProfile = null;
                    this.userChatMessages = [];
                    this.userPrivateChatMessages = [];
                };

                this.initRatingLevel = function () {
                    this.ratingLevels = api.RatingLevel.query(function (items) {
                        events.trigger('OnRatingLevelLoaded');
                        return items;
                    });

                };
                
                this.initDictionary = function () {
                    this.dictionaryWords = api.Dictionary.query(function (items) {
                        return items;
                    });
                  
                };
                this.initUserDictionary = function () {
                    this.userDictionaryWords = api.UserDictionary.query(function (items) {
                        return items;
                    });
                };
                
                this.initUserChatMessages = function (messages) {
                    this.userChatMessages = messages;
     
                };
                
                this.initUserPrivateChatMessages = function (messages) {
                    this.userPrivateChatMessages = messages;

                };
                
                this.initChatUsers = function (users) {
                    this.chatUsers = users;

                };
                
                this.initCurrentChatUser = function (user) {
                    this.chatCurrentUser = user;

                };
                
              /*  this.initUserChatMessages = function () {
                    this.userChatMessages = api.ChatMessages.query(function (items) {
                        return items;
                    });
                };*/
                
                this.initUserProfile = function () {        
                    this.userProfile = api.UserProfile.get(function () {
                        events.trigger('OnUserProfileLoaded'); 
                    });
                };
            }
        ]);
});