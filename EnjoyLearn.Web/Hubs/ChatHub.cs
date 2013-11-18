using EnjoyLearn.Data.Core.Interfaces;
using EnjoyLearn.Models.Authorize;
using EnjoyLearn.Models.Chat;

using Microsoft.AspNet.SignalR;
using NLog.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebMatrix.WebData;

namespace EnjoyLearn.Web.Hubs
{
  public class MessageDetail : ChatMessageModel
  {
    public string UserName { get; set; }
  }

  public class PrivateMessageDetail : PrivateChatMessageModel
  {
    public string UserName { get; set; }
  }

  public class UserDetail
  {
    public int UserId { get; set; }
    public string ConnectionId { get; set; }
    public string UserName { get; set; }
  }

  public class ChatHub : Hub
  {

    #region Data Members

    private static readonly List<UserDetail> ConnectedUsers = new List<UserDetail>();
    private readonly List<MessageDetail> _currentMessage = new List<MessageDetail>();
    private readonly List<PrivateMessageDetail> _privateMessages = new List<PrivateMessageDetail>();

    private readonly IEntityRepository<UserProfileModel> _userProfileRepository;
    private readonly IEntityRepository<ChatMessageModel> _chatMessageRepository;
    private readonly IEntityRepository<UserChatConnectionModel> _userChatConnectionRepository;
    private readonly IEntityRepository<PrivateChatMessageModel> _privateChatMessageRepository;
    private readonly ILogger _logger;

    #endregion

    #region Methods

    public ChatHub(
      ILogger logger,
      IEntityRepository<UserProfileModel> userProfileRepository,
      IEntityRepository<ChatMessageModel> chatMessageRepository,
      IEntityRepository<UserChatConnectionModel> userChatConnectionRepository,
      IEntityRepository<PrivateChatMessageModel> privateChatMessageRepository
     )
    {
      this._logger = logger;
      this._userProfileRepository = userProfileRepository;
      this._chatMessageRepository = chatMessageRepository;
      this._userChatConnectionRepository = userChatConnectionRepository;
      this._privateChatMessageRepository = privateChatMessageRepository;
    }

    public void Connect()
    {
      var connectionId = Context.ConnectionId;
      var userName = WebSecurity.CurrentUserName;
      var profile = _userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId);
      if (profile != null)
      {
        userName = profile.FirstName;
      }
      if (ConnectedUsers.Count(x => x.ConnectionId == connectionId) == 0)
      {
        ConnectedUsers.Add(new UserDetail { ConnectionId = connectionId, UserName = userName, UserId = WebSecurity.CurrentUserId });
        // send to caller
        foreach (var userChat in _userChatConnectionRepository.GetAll())
        {
          foreach (var msg in userChat.Messages)
          {
            var userProfile = _userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == userChat.UserId);
            if (userProfile != null)
            {
              _currentMessage.Add(new MessageDetail
              {
                UserName = userProfile.FirstName,
                Message = msg.Message,
                ReceivedOn = msg.ReceivedOn
              });
            }
          }
          foreach (var privateMsg in userChat.PrivateMessages)
          {
            var userProfile = _userProfileRepository.GetAll().FirstOrDefault(x => x.UserId == userChat.UserId);
            if (userProfile != null)
            {
              _privateMessages.Add(new PrivateMessageDetail
              {
                UserName = userProfile.FirstName,
                Message = privateMsg.Message,
                ReceivedOn = privateMsg.ReceivedOn,
                ReceiverUserId = privateMsg.ReceiverUserId,
                SenderUserId = privateMsg.SenderUserId
              });
            }
          }
        }

        Clients.Caller.onConnected(connectionId, userName, ConnectedUsers, _currentMessage, _privateMessages);
        // send to all except caller client
        Clients.AllExcept(connectionId).onNewUserConnected(connectionId, userName);
      }
    }

    public void SendMessageToAll(string userName, string message)
    {
      // Add message to user chat history
      var userChatMessage = new ChatMessageModel
                              {
                                Id = Guid.NewGuid(),
                                ReceivedOn = DateTime.Now,
                                Message = message
                              };

      var userChat = _userChatConnectionRepository.GetAll().SingleOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
      if (userChat == null)
      {
        var newUser = new UserChatConnectionModel
        {
          Id = Guid.NewGuid(),
          UserId = WebSecurity.CurrentUserId
        };

        _userChatConnectionRepository.Add(newUser);
        _userChatConnectionRepository.SaveAsync();
        _chatMessageRepository.Add(userChatMessage);
        _chatMessageRepository.SaveAsync();
      }
      else
      {
         userChatMessage.UserChatConnectionId = userChat.Id;
        _chatMessageRepository.Add(userChatMessage);
        _chatMessageRepository.SaveAsync();

      }

      // Broad cast message
      Clients.All.messageReceived(userName, message);
    }

    public void SendPrivateMessage(string toUserId, string message)
    {
      string fromUserId = Context.ConnectionId;
      var toUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
      var fromUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);
      if (toUser != null && fromUser != null)
      {
        var userChatMessage = new PrivateChatMessageModel
                                {
                                  Id = Guid.NewGuid(),
                                  ReceivedOn = DateTime.Now,
                                  Message = message,
                                  ReceiverUserId = toUser.UserId,
                                  SenderUserId = fromUser.UserId
                                };

        var userChatConnectionModel = _userChatConnectionRepository.GetAll().SingleOrDefault(t => t.UserId == fromUser.UserId);
        if (userChatConnectionModel == null)
        {
          var newUser = new UserChatConnectionModel
          {
            Id = Guid.NewGuid(),
            UserId = WebSecurity.CurrentUserId
          };
          _userChatConnectionRepository.Add(newUser);
          _userChatConnectionRepository.SaveAsync();

          userChatMessage.UserChatConnectionId = newUser.Id;
          _privateChatMessageRepository.Add(userChatMessage);
          //_privateChatMessageRepository.Save();
          _privateChatMessageRepository.SaveAsync();
        }
        else
        {
          userChatMessage.UserChatConnectionId = userChatConnectionModel.Id;
          _privateChatMessageRepository.Add(userChatMessage);
          _privateChatMessageRepository.SaveAsync();
        }

        // send to 
        Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message, DateTime.Now);
        // send to caller user
        Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message, DateTime.Now);
      }
    }

    public override System.Threading.Tasks.Task OnDisconnected()
    {
      var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
      if (item != null)
      {
        ConnectedUsers.Remove(item);
        var id = Context.ConnectionId;
        Clients.All.onUserDisconnected(id, item.UserName);
      }
      return base.OnDisconnected();
    }


    #endregion

  }
}