define(['appModule'], function (app) {
    app.lazy.controller('ChatController',
    [
        '$scope',
        'EnjoyLearnContext',
        function ($scope,context) {

            // Reference the auto-generated proxy for the hub. 
            var chatHub = $.connection.chatHub;
    
            $.connection.hub.start()
                .done(function () {
                        chatHub.server.connect();
                        console.log("hub.start.done");              
                })
                .fail(function (error) {
                    console.log(error);
                });
            
           
            registerClientMethods();
 
            $scope.btnSendMsg = function () {
                var msg = $scope.txtMessage;
                if (msg.length > 0) {
                    var userName = $scope.hdUserName;
                    chatHub.server.sendMessageToAll(userName, msg);
                    $scope.txtMessage = "";
                }
            };
            
            initCurrentChatUser();
            initChatMessages();
            initChatUsers();
                     
            // Load Messages
            function initChatMessages() {
               // alert(JSON.stringify(context.userChatMessages));
                angular.forEach(context.userChatMessages, function (msg) {
                   // alert(msg.ReceivedOn);
                    addMessage(msg.UserName, msg.Message, msg.ReceivedOn);
                });
            }
            
            
            // Add All Users
            function initChatUsers() {              
                angular.forEach(context.chatUsers, function (user) {
                    addUser( user.ConnectionId, user.UserName);
                });
            }

            // Add User
            function initCurrentChatUser() {
                if (context.chatCurrentUser.id != undefined) {
                    $scope.hdId = context.chatCurrentUser.id;
                    $scope.hdUserName = context.chatCurrentUser.userName;
                    $scope.spanUser = context.chatCurrentUser.userName;
                };
            }
            function registerClientMethods() {
                // Calls when user successfully logged in
                chatHub.client.onConnected = function (id, userName, allUsers, messages, privateMessages) {
                    $scope.$apply(function() {
                        // alert(JSON.stringify(messages));
                        context.initUserChatMessages(messages);
                        context.initUserPrivateChatMessages(privateMessages);
                        context.initChatUsers(allUsers);
                        context.initCurrentChatUser({ id: id, userName: userName });

                        initCurrentChatUser();
                        initChatMessages();
                        initChatUsers();

                    });
                };

                // On New User Connected
                chatHub.client.onNewUserConnected = function(id, name) {
                    addUser( id, name);
                };

                // On User Disconnected
                chatHub.client.onUserDisconnected = function(id, userName) {
                    $('#' + id).remove();
                    var ctrId = 'private_' + id;
                    $('#' + ctrId).remove();
                    var disc = $('<div class="disconnect">"' + userName + '" logged off.</div>');
                    $(disc).hide();
                    $('#divusers').prepend(disc);
                    $(disc).fadeIn(200).delay(2000).fadeOut(200);
                };

                chatHub.client.messageReceived = function(userName, message) {
                    //$scope.$apply(function () {
                    //    $scope.messages.push({ userName: userName, message: message });
                    //});
                    addMessage(userName, message);
                };

                chatHub.client.sendPrivateMessage = function (windowId, fromUserName, message, receivedOn) {
                    var ctrId = 'private_' + windowId;
                    if ($('#' + ctrId).length == 0) {
                        createPrivateChatWindow(windowId, ctrId, fromUserName);
                    }
                    $('#' + ctrId).find('#divMessage').append('<div class="message"><span class="userName">' + fromUserName + '</span>: ' + message
                        + '<div style="float:right;">' + new Date(receivedOn).toUTCString() + '</div> </div>');
                    // set scrollbar
                    var height = $('#' + ctrId).find('#divMessage')[0].scrollHeight;
                    $('#' + ctrId).find('#divMessage').scrollTop(height);
                };
            }

            function addUser( id, name) {
                var userId = $scope.hdId;
                var code = "";
                if (userId == id) {
                    code = $('<div class="loginUser">' + name + "</div>");
                }
                else {
                    code = $('<a id="' + id + '" class="user" >' + name + '<a>');
                    $(code).click(function () {
                        var id = $(this).attr('id');
                        if (userId != id) {
                            openPrivateChatWindow(id, name);
                        }
                    });
                }
                $("#divusers").append(code);
            }

            function addMessage(userName, message, receivedOn) {

                $('#divChatWindow').append('<div class="message"><span class="userName">' + userName + '</span>: ' + message  
                    + '<div style="float:right; font-size:10px;">' + new Date(receivedOn).toUTCString() + '</div> </div>');

                var height = $('#divChatWindow')[0].scrollHeight;
                $('#divChatWindow').scrollTop(height);
            }

            function openPrivateChatWindow(id, userName) {
                
                var ctrId = 'private_' + id;
                if ($('#' + ctrId).length > 0) return;
                createPrivateChatWindow(id, ctrId, userName);
            }

            function createPrivateChatWindow(userId, ctrId, userName) {
                var div = '<div id="' + ctrId + '" class="draggable" title="'+userName+'" rel="0">' +

                           '<div id="divMessage" class="messageArea">' +

                           '</div>' +
                           '<div class="buttonBar">' +
                              '<input id="txtPrivateMessage" class="msgText" type="text"   />' +
                              '<input id="btnSendMessage" class="btn btn-primary" type="button" value="Send"   />' +
                           '</div>' +
                        '</div>';
                var $div = $(div);

                angular.forEach(context.userPrivateChatMessages, function (msg) {
                    // alert(msg.ReceivedOn);
                   // msg.UserName, msg.Message, msg.ReceivedOn
                    $div.find('#divMessage').append('<div class="message"><span class="userName">' + msg.UserName + '</span>: ' + msg.Message 
                      + '<div style="float:right; font-size:10px;">' + new Date(msg.ReceivedOn).toUTCString() + '</div> </div>');
                });
                

                // Send Button event
                $div.find("#btnSendMessage").click(function () {
                    $textBox = $div.find("#txtPrivateMessage");
                    var msg = $textBox.val();
                    if (msg.length > 0) {
                        chatHub.server.sendPrivateMessage(userId, msg);
                        $textBox.val('');
                    }
                });
                // Text Box event
                $div.find("#txtPrivateMessage").keypress(function (e) {
                    if (e.which == 13) {
                        $div.find("#btnSendMessage").click();
                    }
                });
                addDivToContainer($div, ctrId);
            }

            function addDivToContainer($div, ctrId) {
                $('#divContainer').prepend($div);
                $div.dialog({
                    height: 400,
                    width: 350,
                    show: {
                        effect: "blind",
                        duration: 500
                    },
                    hide: {
                        effect: "explode",
                        duration: 500
                    },
                    close: function () {
                        
                        $('#' + ctrId).remove();
                    }
                });
            }
        }
    ]);
});