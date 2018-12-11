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
        EventDTO GetEventById(int id);
        void UpdateEvent(EventDTO eventDTO);
        void DeleteEventByID(int eventId);
        int CreateEvent(EventDTO eventDTO);
        int AddGuestsToEvent(int eventId, List<GuestDTO> guests);
    }
}