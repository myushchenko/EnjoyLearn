using System.ComponentModel.DataAnnotations.Schema;
using EnjoyLearn.Models.Authorize;
using EnjoyLearn.Models.Interfaces;
using System;

namespace EnjoyLearn.Models.Rating
{
  public class PointsModel : IEntity<Guid>
  {
    public Guid Id { get; set; }

    public int? Points { get; set; }

    public Guid LevelId { get; set; }
    [ForeignKey("LevelId")]
    public virtual LevelModel Level { get; set; }

    public Guid NextLevelId { get; set; }

  }
 
}
