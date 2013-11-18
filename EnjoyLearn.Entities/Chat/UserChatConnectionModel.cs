namespace EnjoyLearn.Models.Chat
{
  using EnjoyLearn.Models.Authorize;
  using EnjoyLearn.Models.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations.Schema;

  public class UserChatConnectionModel : IEntity<Guid>
  {
    public Guid Id { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual UserModel User { get; set; }

    public virtual ICollection<ChatMessageModel> Messages { get; set; }

    public virtual ICollection<PrivateChatMessageModel> PrivateMessages { get; set; }

  }
}
