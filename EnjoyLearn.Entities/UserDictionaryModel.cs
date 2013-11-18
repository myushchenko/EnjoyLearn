using EnjoyLearn.Models.Interfaces;

namespace EnjoyLearn.Models
{
  using EnjoyLearn.Models.Authorize;
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public class UserDictionaryModel : IEntity<Guid>
  {
    [Key]
    public Guid Id { get; set; }

    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual UserModel User { get; set; }

    public Guid DictionaryId { get; set; }
    [ForeignKey("DictionaryId")]
    public virtual DictionaryModel Dictionary { get; set; }
  }
}
