using EventApp.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Services.Locations
{
    public interface ILocationService
    {
        IEnumerable<LocationDTO> GetLocations();
        int CreateLocation(LocationDTO locationDTO);
        int AddStaffToLocation(List<StaffDTO> staff);
        void DeleteLocationById(int locationID);
        void ChangeEventLocation(int eventId, LocationDTO location);
    }
}
