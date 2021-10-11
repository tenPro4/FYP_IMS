using EntityFramework.Entities;
using EntityFrameworkExtension.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntityFramework.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<PagedList<Log>> GetLogsAsync(string search, int page = 1, int pageSize = 10);

        Task<bool> DeleteLogsOlderThanAsync(DateTime deleteOlderThan);

    }
}
