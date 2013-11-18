using EnjoyLearn.Data.Core;
using EnjoyLearn.Data.DBInteractions;
using EnjoyLearn.Models;
using EnjoyLearn.Repositories.Interfaces;
namespace EnjoyLearn.Repositories
{
    public class LogEntryRepository : EntityRepositoryBase<LogModel>, ILogEntryRepository
    {
       public LogEntryRepository(IDBFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
