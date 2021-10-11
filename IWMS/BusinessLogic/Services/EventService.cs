using BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Dtos.Event;
using System.Threading.Tasks;
using EntityFramework.Repositories.Interfaces;
using EntityFramework.Entities;
using BusinessLogic.Mapper;

namespace BusinessLogic.Services
{
    public class EventService : IEventService
    {
        private readonly IAsyncRepository<Event> eventRepo;

        public EventService(IAsyncRepository<Event> eventRepo)
        {
            this.eventRepo = eventRepo;
        }

        public async Task<EventDto> AddAsync(EventDto dto)
        {
            var result = await eventRepo.AddAsync(dto.ToEventModel<Event>());

            return result.ToEventModel<EventDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var find = await eventRepo.GetByIdAsync(id);

            await eventRepo.DeleteAsync(find);
        }

        public async Task<List<EventDto>> GetAllAsync()
        {
            var models = await eventRepo.GetAllAsync();

            return models.ToEventListModel<EventDto>();
        }
    }
}
