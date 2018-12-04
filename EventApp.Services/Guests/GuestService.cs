using EventApp.Data.Entities;
using EventApp.Data.Infrastructure;
using EventApp.DTOs;
using Omu.ValueInjecter;
using System.Linq;

namespace EventApp.Services.Guests
{
    public class GuestService : IGuestService
    {
        private readonly IRepository<Guest> guestRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<EventGuest> eventGuestRepo;

        public GuestService(IRepository<Guest> guestRepo, IUnitOfWork unitOfWork,IRepository<EventGuest> eventGuestRepo)
        {
            this.guestRepo = guestRepo;
            this.unitOfWork = unitOfWork;
            this.eventGuestRepo = eventGuestRepo;
        }

        public int CreateGuest(GuestDTO guest)
        {
            var newGuest = (Guest)new Guest().InjectFrom(guest);
            guestRepo.Add(newGuest);
            unitOfWork.Commit();
            return newGuest.Id;
        }

        public void DeleteByID(int guestID)
        {
            var eventGuestsToDelete = eventGuestRepo.Query().Where(eg => eg.GuestId == guestID);
            eventGuestRepo.Delete(eventGuestsToDelete);

            guestRepo.Delete(guestRepo.GetById(guestID));
            unitOfWork.Commit();
        }
    }
}