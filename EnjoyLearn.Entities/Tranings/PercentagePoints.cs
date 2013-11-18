namespace EnjoyLearn.Models.Tranings
{
  using EnjoyLearn.Models.Interfaces;

  public abstract class PercentagePoints : IPercentagePoints
  {
    public int ProcPoints100 { get; set; }
    public int ProcPoints90 { get; set; }
    public int ProcPoints80 { get; set; }
    public int ProcPoints60 { get; set; }
    public int ProcPoints50 { get; set; }
    public int ProcPoints40 { get; set; }
  }
}
