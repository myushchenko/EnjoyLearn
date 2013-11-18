using System;
using EnjoyLearn.Models.Interfaces;

namespace EnjoyLearn.Models.Tranings
{
  public class BaseTraningModel : PercentagePoints, IEntity<Guid>
  {
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
  }
}
