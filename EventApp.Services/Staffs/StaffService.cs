using EventApp.Data.Entities;
using EventApp.Data.Infrastructure;
using EventApp.DTOs;
using Omu.ValueInjecter;

namespace EventApp.Services.Staffs
{
    public class StaffService : IStaffService
    {
        private readonly IRepository<Staff> staffRepo;
        private readonly IUnitOfWork unitOfWork;

        public StaffService(IRepository<Staff> staffRepo, IUnitOfWork unitOfWork)
        {
            this.staffRepo = staffRepo;
            this.unitOfWork = unitOfWork;
        }

        public int CreateStaff(StaffDTO staff)
        {
            var newStaff = (Staff)new Staff().InjectFrom(staff);
            staffRepo.Add(newStaff);
            unitOfWork.Commit();
            return newStaff.Id;
        }

        public void DeleteStaffByID(int staffID)
        {
            staffRepo.Delete(staffRepo.GetById(staffID));
        }
    }
}