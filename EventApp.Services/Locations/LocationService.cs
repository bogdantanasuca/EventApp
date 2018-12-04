using EventApp.Data.Entities;
using EventApp.Data.Infrastructure;
using EventApp.DTOs;
using EventApp.Services.Events;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventApp.Services.Locations
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> locationRepo;
        private readonly IRepository<Staff> staffRepo;
        private readonly IEventService eventService;
        private readonly IRepository<Event> eventRepo;
        private readonly IUnitOfWork unitOfWork;

        public LocationService(IRepository<Location> locationRepo, IUnitOfWork unitOfWork, IRepository<Staff> staffRepo, IEventService eventService,IRepository<Event> eventRepo)
        {
            this.locationRepo = locationRepo;
            this.unitOfWork = unitOfWork;
            this.staffRepo = staffRepo;
            this.eventService = eventService;
            this.eventRepo = eventRepo;
        }

        public int AddStaffToLocation(List<StaffDTO> staff)
        {
            staff.ForEach(x => staffRepo.Add((Staff)new Staff().InjectFrom(x)));
            unitOfWork.Commit();
            return 0;
        }

        public void ChangeEventLocation(int eventId, LocationDTO location)
        {
            var LocationID = CreateLocation(location);
            var Event = eventRepo.Get(x => x.Id == eventId).FirstOrDefault();
            Event.LocationId = LocationID;
            unitOfWork.Commit();
        }

        public int CreateLocation(LocationDTO locationDTO)
        {
            var newLocation = (Location)new Location().InjectFrom(locationDTO);
            locationRepo.Add(newLocation);
            unitOfWork.Commit();
            return newLocation.Id;
        }

        public void DeleteLocationById(int locationID)
        {
            foreach (var item in staffRepo.Get(x => x.LocationId == locationID).ToList())
            {
                staffRepo.Delete((Staff)new Staff().InjectFrom(item));
            }
            foreach (var item in eventService.GetEventsByLocationId(locationID))
            {
                eventService.DeleteEventByID(item.Id);
            }
            locationRepo.Delete(locationRepo.GetById(locationID));
            unitOfWork.Commit();
        }

        public IEnumerable<LocationDTO> GetLocations()
        {
            return locationRepo.Query().Select(e => new LocationDTO().InjectFrom(e) as LocationDTO);
        }
    }
}
