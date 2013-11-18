using EnjoyLearn.Models.Interfaces;
using System;

namespace EnjoyLearn.Models.Rating
{
  public class LevelModel : IEntity<Guid>
  {
    public Guid Id { get; set; }

    public int Type { get; set; }

    public string Name { get; set; }

    public int Points { get; set; }
  }
}
