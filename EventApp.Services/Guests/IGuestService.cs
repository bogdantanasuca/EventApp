
using EventApp.DTOs;
using System.Collections.Generic;

namespace EventApp.Services.Guests
{
    public interface IGuestService

    {
        int CreateGuest(GuestDTO guest);
        void DeleteByID(int guestID);
        List<GuestDTO> GetGuestsByEventId(int eventId);
    }
}