using EntityFramework.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EntityFramework.Entities;
using EntityFrameworkExtension.Extensions;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using EntityFrameworkExtension.Enums;
using EntityFramework.Context;

namespace EntityFramework.Repositories
{
    public class LogRepository<TDbContext>: ILogRepository
        where TDbContext : DbContext
    {
        protected readonly TDbContext DbContext;
        public bool AutoSaveChanges { get; set; } = true;

        public LogRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<bool> DeleteLogsOlderThanAsync(DateTime deleteOlderThan)
        {
            var logsToDelete = await DbContext.Set<Log>().AsNoTracking()
                .Where(x => x.TimeStamp.DateTime.Date < deleteOlderThan.Date).ToListAsync();


            if (logsToDelete.Count == 0) return false;

            DbContext.Set<Log>().RemoveRange(logsToDelete);

            return await AutoSaveChangesAsync() > 0 ? true : false;
            
        }

        public virtual async Task<PagedList<Log>> GetLogsAsync(string search, int page = 1, int pageSize = 10)
        {
            var pagedList = new PagedList<Log>();
            Expression<Func<Log, bool>> searchCondition = x => x.LogEvent.Contains(search) || x.Message.Contains(search) || x.Exception.Contains(search);
            var logs = await DbContext.Set<Log>()
                .WhereIf(!string.IsNullOrEmpty(search), searchCondition)
                .PageBy(x => x.Id, page, pageSize)
                .ToListAsync();

            pagedList.Data.AddRange(logs);
            pagedList.TotalCount = await DbContext.Set<Log>().WhereIf(!string.IsNullOrEmpty(search), searchCondition).CountAsync();

            return pagedList;
        }

        private async Task<int> AutoSaveChangesAsync()
        {
            return AutoSaveChanges ? await DbContext.SaveChangesAsync() : (int)SavedStatus.WillBeSavedExplicitly;
        }
    }
}
