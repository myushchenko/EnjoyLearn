using System;
using System.ComponentModel.DataAnnotations.Schema;
using EnjoyLearn.Models.Interfaces;
using EnjoyLearn.Models.Tranings.Words;

namespace EnjoyLearn.Models.Tranings
{
  public class TraningResultsModel :IEntity<Guid>
  {
    public Guid Id { get; set; }

    public Guid TrueOrFalseTraningResultId { get; set; }
    [ForeignKey("TrueOrFalseTraningResultId")]
    public virtual TraningResultModel<TrueOrFalseTraningModel> TrueOrFalseTraningResult { get; set; }

  }
}
