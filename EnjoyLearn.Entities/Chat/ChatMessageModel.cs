using EnjoyLearn.Models.Authorize;

namespace EnjoyLearn.Models.Chat
{
  using EnjoyLearn.Models.Interfaces;
  using System;

  public class ChatMessageModel : IEntity<Guid>
  {
    public Guid Id { get; set; }

    public string Message { get; set; }

    public DateTimeOffset ReceivedOn { get; set; }

    public Guid UserChatConnectionId { get; set; }

    public UserChatConnectionModel UserChatConnection { get; set; }
  }
}
