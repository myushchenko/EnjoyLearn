using System;
using System.Collections.Generic;
using EnjoyLearn.Models.Chat;
using EnjoyLearn.Models.Interfaces;
using EnjoyLearn.Models.Rating;

namespace EnjoyLearn.Models.Authorize
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public class UserModel
  {
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    public string UserName { get; set; }

    [InverseProperty("SenderUser")]
    public virtual ICollection<PrivateChatMessageModel> SenderPrivateChatMessages { get; set; }

    [InverseProperty("ReceiverUser")]
    public virtual ICollection<PrivateChatMessageModel> ReceiverPrivateChatMessages { get; set; }

    //[InverseProperty("UserPoints")]
    //public virtual PointsModel UserPoints22 { get; set; }

    /*public int ProfileId { get; set; }*/

   // [ForeignKey("ProfileId")]
   // public virtual UserProfileModel UserProfile { get; set; }

  }
}
