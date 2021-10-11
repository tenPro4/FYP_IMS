using BusinessLogicShared.Common;
using BusinessLogicShared.Filters;
using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Repositories.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<List<AttendanceModel>> GetActiveAsync(AttendanceFilter filter);
        Task<List<AttendanceModel>> GetAbsentAsync(AttendanceFilter filter);
        Task<List<AttendanceModel>> GetHistoryAsync(AttendanceFilter filter);
        long GetLastestRecord(int id);
    }
}
