using EventApp.Data.Entities;
using EventApp.Data.Infrastructure;
using EventApp.DTOs;
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
        private readonly IUnitOfWork unitOfWork;

        public LocationService(IRepository<Location> locationRepo, IUnitOfWork unitOfWork,IRepository<Staff>staffRepo)
        {
            this.locationRepo = locationRepo;
            this.unitOfWork = unitOfWork;
            this.staffRepo = staffRepo;
        }

        public int AddStaffToLocation(List<StaffDTO> staff)
        {
            staff.ForEach(x => staffRepo.Add((Staff)new Staff().InjectFrom(x)));
            unitOfWork.Commit();
            return 0;
        }

        public int CreateLocation(LocationDTO location)
        {
            locationRepo.Add((Location)new Location().InjectFrom(location));
            unitOfWork.Commit();
            return locationRepo.Query().Select(e => e.Id).Last();
        }

        public IEnumerable<LocationDTO> GetLocations()
        {
            return locationRepo.Query().Select(e => new LocationDTO().InjectFrom(e) as LocationDTO);
        }
    }
}
