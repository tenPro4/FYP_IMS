using BusinessLogic.Services.Interfaces;
using EntityFramework.Repositories.Interfaces;
using System;
using BusinessLogic.Dtos.Log;
using System.Threading.Tasks;
using BusinessLogic.Mapper;

namespace BusinessLogic.Services
{
    public class LogService : ILogService
    {
        protected readonly ILogRepository Repository;

        public LogService(ILogRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<bool> DeleteLogsOlderThanAsync(DateTime deleteOlderThan)
        {
            return await Repository.DeleteLogsOlderThanAsync(deleteOlderThan);
        }

        public virtual async Task<LogsDto> GetLogsAsync(string search, int page = 1, int pageSize = 10)
        {
            var pagedList = await Repository.GetLogsAsync(search, page, pageSize);
            var logs = pagedList.ToModel();

            return logs;
        }
    }
}
