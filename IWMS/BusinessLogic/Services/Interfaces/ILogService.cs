using BusinessLogic.Dtos.Log;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface ILogService
    {
        Task<LogsDto> GetLogsAsync(string search, int page = 1, int pageSize = 10);

        Task<bool> DeleteLogsOlderThanAsync(DateTime deleteOlderThan);
    }
}
