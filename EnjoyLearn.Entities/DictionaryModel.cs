namespace EnjoyLearn.Models
{
  using EnjoyLearn.Models.Interfaces;
  using System;
  using System.ComponentModel.DataAnnotations;

  public class DictionaryModel : IEntity<Guid>
  {
    [Key]
    public Guid Id { get; set; }

    public string EnglishWord { get; set; }

    public string RussianWord { get; set; }

    public string TranscriptionWord { get; set; }
  }
}
