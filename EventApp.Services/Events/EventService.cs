using EventApp.DTOs;
using EventApp.Data.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using EventApp.Data.Entities;
using Omu.ValueInjecter;
using System;
using EventApp.Data.Enums;
using Microsoft.EntityFrameworkCore;
using EventApp.Services.Locations;

namespace EventApp.Services.Events
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> eventRepo;
        private readonly IRepository<EventGuest> eventGuestRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILocationService locationService;

        public EventService(IRepository<Event> eventRepo, IUnitOfWork unitOfWork, IRepository<EventGuest> eventGuestRepo, ILocationService locationService)
        {
            this.eventRepo = eventRepo;
            this.unitOfWork = unitOfWork;
            this.eventGuestRepo = eventGuestRepo;
            this.locationService = locationService;
        }

        public IEnumerable<EventDTO> GetEvents()
        {
            return eventRepo.Query().Select(e => new EventDTO().InjectFrom(e) as EventDTO);
        }

        public IEnumerable<EventDTO> GetEventsByName(string eventName)
        {
            return eventRepo.Get(e => e.Name.Contains(eventName))
                            .Select(e => new EventDTO().InjectFrom(e) as EventDTO);
        }

        public IEnumerable<EventDTO> GetEventsByDate(DateTime eventDate)
        {
            return eventRepo.Get(e => e.StartTime == eventDate)
                            .Select(e => new EventDTO().InjectFrom(e) as EventDTO);
        }

        public IEnumerable<EventDTO> GetEventsByLocationId(int locationId)
        {
            return eventRepo.Get(e => e.LocationId == locationId)
                            .Select(e => new EventDTO().InjectFrom(e) as EventDTO);
        }

        public IEnumerable<EventDTO> GetEventsBySize(EventSize eventSize)
        {
            return eventGuestRepo.Query().Include(e => e.Event)
                                               .Include(e => e.Guest)
                                               .Where(e => e.HasAttended)
                                               .GroupBy(e => e.Event)
                                               .Where(g => g.Count() < (int)eventSize)
                                               .Select(g => new EventDTO().InjectFrom(g.Key) as EventDTO);

        }

        public int CreateEvent(EventDTO eventDTO)
        {
            eventRepo.Add((Event)new Event().InjectFrom(eventDTO));
            unitOfWork.Commit();
            return eventRepo.Query().Select(e => e.Id).Last();
        }

        public int AddGuestsToEvent(List<EventGuestDTO> guests)
        {
            guests.ForEach(x => eventGuestRepo.Add((EventGuest)new EventGuest().InjectFrom(x)));
            unitOfWork.Commit();
            return 0;
        }

        public void ChangeEventLocation(int eventId, LocationDTO location)
        {
            var LocationID = locationService.CreateLocation(location);
            var Event = eventRepo.Get(x => x.Id == eventId).FirstOrDefault();
            Event.LocationId = LocationID;
            unitOfWork.Commit();
        }
    }
}