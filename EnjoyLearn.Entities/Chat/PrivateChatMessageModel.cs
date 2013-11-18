using System;
using EnjoyLearn.Models.Interfaces;

namespace EnjoyLearn.Models.Chat
{
  using EnjoyLearn.Models.Authorize;
  using System.ComponentModel.DataAnnotations.Schema;

  public class PrivateChatMessageModel : IEntity<Guid>
  {
    public Guid Id { get; set; }

    public string Message { get; set; }

    public DateTimeOffset ReceivedOn { get; set; }

    public Guid UserChatConnectionId { get; set; }

    public UserChatConnectionModel UserChatConnection { get; set; }

    [ForeignKey("SenderUser")]
    public int SenderUserId { get; set; }
    public virtual UserModel SenderUser { get; set; }

    [ForeignKey("ReceiverUser")]
    public int ReceiverUserId { get; set; }
    public virtual UserModel ReceiverUser { get; set; }
  }
}
