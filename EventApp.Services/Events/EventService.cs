using EventApp.DTOs;
using EventApp.Data.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using EventApp.Data.Entities;
using Omu.ValueInjecter;
using System;

namespace EventApp.Services.Events
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> eventRepo;
        private readonly IRepository<EventGuest> eventGuestRepo;
        private readonly IUnitOfWork unitOfWork;

        public EventService(IRepository<Event> eventRepo, IUnitOfWork unitOfWork, IRepository<EventGuest> eventGuestRepo)
        {
            this.eventRepo = eventRepo;
            this.unitOfWork = unitOfWork;
            this.eventGuestRepo = eventGuestRepo;
        }

        public IEnumerable<EventDTO> GetEvents()
        {
            return eventRepo.Query(_ => true).Select(e => new EventDTO().InjectFrom(e) as EventDTO);
        }

        public IEnumerable<EventDTO> GetEventsByName(string eventName)
        {
            return eventRepo.Query(e => e.Name.Contains(eventName))
                            .Select(e => new EventDTO().InjectFrom(e) as EventDTO);
        }

        public IEnumerable<EventDTO> GetEventsByDate(DateTime eventDate)
        {
            return eventRepo.Query(e => e.StartTime == eventDate)
                            .Select(e => new EventDTO().InjectFrom(e) as EventDTO);
        }

        public IEnumerable<EventDTO> GetEventsByLocation(int locationId)
        {
            return eventRepo.Query(e => e.LocationId == locationId)
                            .Select(e => new EventDTO().InjectFrom(e) as EventDTO);
        }

        //public IEnumerable<EventDTO> GetEventsBySize(int eventSize)
        //{
        //    return eventRepo.Query(_ => true )
        //                    .Join(eventGuestRepo.Query(_ => true), e => e.Id, eg => eg.EventId, (e, eg) => new { e, eg })

        //}

        public int CreateEvent(EventDTO eventDTO)
        {
            eventRepo.Add((Event)new Event().InjectFrom(eventDTO));
            unitOfWork.Commit();
            return eventRepo.Query(e => e.Name == eventDTO.Name).Select(e => e.Id).First();
        }
    }
}
