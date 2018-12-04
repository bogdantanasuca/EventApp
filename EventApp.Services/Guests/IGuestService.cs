
using EventApp.DTOs;

namespace EventApp.Services.Guests
{
    public interface IGuestService

    {
        int CreateGuest(GuestDTO guest);
        void DeleteByID(int guestID);
    }
}