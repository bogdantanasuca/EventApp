using System.Collections.Generic;
using EventApp.DTOs;

namespace EventApp.Services.Events
{
    public interface IEventService
    {
        IEnumerable<EventDTO> GetEvents();
        IEnumerable<EventDTO> GetEventsByName(string eventName);
        IEnumerable<EventDTO> GetEventsByName(string eventName);

    }
}