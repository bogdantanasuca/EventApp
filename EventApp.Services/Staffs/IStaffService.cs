
using EventApp.DTOs;

namespace EventApp.Services.Staffs
{
    public interface IStaffService
    {
        int CreateStaff(StaffDTO staff);
        void DeleteStaffByID(int staffID);
    }
}