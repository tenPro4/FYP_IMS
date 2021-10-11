using BusinessLogic.Dtos.Event;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IEventService
    {
        Task<List<EventDto>> GetAllAsync();
        Task<EventDto> AddAsync(EventDto dto);
        Task DeleteAsync(int id);
    }
}
