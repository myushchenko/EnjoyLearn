
namespace EnjoyLearn.Data.DBInteractions
{
  using System.Data.Entity;

  public class DBFactory : Disposable, IDBFactory
  {

    public DBFactory()
    {
      Database.SetInitializer<EnjoyLearnContext>(null);
    }

    private EnjoyLearnContext _dataContext;

    public EnjoyLearnContext Get()
    {
      return _dataContext ?? (_dataContext = new EnjoyLearnContext());
    }

    protected override void DisposeCore()
    {
      if (_dataContext != null)
        _dataContext.Dispose();
    }
  }
}
