﻿using EventApp.DTOs;
using EventApp.Data.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using EventApp.Data.Entities;
using Omu.ValueInjecter;
using System;
using EventApp.Data.Enums;
using Microsoft.EntityFrameworkCore;
using EventApp.Services.Locations;
using EventApp.Services.Guests;

namespace EventApp.Services.Events
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> eventRepo;
        private readonly IRepository<EventGuest> eventGuestRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IGuestService guestService;
        private readonly IRepository<Guest> guestRepo;

        public EventService(IRepository<Event> eventRepo, IUnitOfWork unitOfWork, IRepository<EventGuest> eventGuestRepo, IGuestService guestService, IRepository<Guest> guestRepo)
        {
            this.eventRepo = eventRepo;
            this.unitOfWork = unitOfWork;
            this.eventGuestRepo = eventGuestRepo;
            this.guestService = guestService;
            this.guestRepo = guestRepo;
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
            var newEvent = (Event)new Event().InjectFrom(eventDTO);
            eventRepo.Add(newEvent);
            unitOfWork.Commit();
            return newEvent.Id;
        }

        public int AddGuestsToEvent(int eventId, List<GuestDTO> guests)
        {
            foreach (var guest in guests)
            {
                var dataGuest = guestRepo.Get(e => e.FirstName == guest.FirstName && e.LastName == guest.LastName && e.Phone == guest.Phone).Select(g => g.Id).FirstOrDefault();
                EventGuestDTO eventGuest = null;
                if (dataGuest == 0)
                {
                    var guestID = guestService.CreateGuest(guest);
                    eventGuest = new EventGuestDTO
                    {
                        EventId = eventId,
                        GuestId = guestID
                    };
                }
                else
                {
                    eventGuest = new EventGuestDTO
                    {
                        EventId = eventId,
                        GuestId = dataGuest
                    };
                }
                    var newEventGuest = (EventGuest)new EventGuest().InjectFrom(eventGuest);
                    eventGuestRepo.Add(newEventGuest);
                    unitOfWork.Commit();
            }
            return 0;
        }

        public void DeleteEventByID(int eventId)
        {
            foreach (var item in eventGuestRepo.Get(x => x.EventId == eventId).ToList())
            {
                eventGuestRepo.Delete((EventGuest)new EventGuest().InjectFrom(item));
            }
            eventRepo.Delete(eventRepo.GetById(eventId));
            unitOfWork.Commit();
        }
    }
}