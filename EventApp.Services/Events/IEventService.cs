using System;
using System.Collections.Generic;
using EventApp.Data.Enums;
using EventApp.DTOs;

namespace EventApp.Services.Events
{
    public interface IEventService
    {
        IEnumerable<EventDTO> GetEvents();
        IEnumerable<EventDTO> GetEventsByName(string eventName);
        IEnumerable<EventDTO> GetEventsByDate(DateTime eventDate);
        IEnumerable<EventDTO> GetEventsByLocationId(int locationId);
        IEnumerable<EventDTO> GetEventsBySize(EventSize eventSize);
        int CreateEvent(EventDTO eventDTO);
        int AddGuestsToEvent(List<EventGuestDTO> guests);
        void ChangeEventLocation(int eventId, LocationDTO location);
    }
}