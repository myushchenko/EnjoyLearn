using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnjoyLearn.Models.Interfaces;

namespace EnjoyLearn.Models.Rating
{
  public class RanksNobilityModel : IEntity<Guid>
  {
    public Guid Id { get; set; }

    public string MaleName { get; set; }

    public string FemaleName { get; set; }

    public string Description { get; set; }

    public int Points { get; set; }
  }
}
