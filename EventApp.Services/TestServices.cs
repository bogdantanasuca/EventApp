using EventApp.Data;
using EventApp.Data.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using Omu.ValueInjecter;
using System.Text;

namespace EventApp.Services
{
    public class TestServices
    {
        public static EventAppContext Context = new EventAppContext();

        public IEnumerable<GuestsDTO> Query3()
        {
            var query = Context.Guests
                               .Where(s => s.Age >= 18)
                               .ToList();

            return query.Select(x => new GuestsDTO().InjectFrom(x) as GuestsDTO).ToList();

        }

        public IEnumerable<GuestNameDTO> Query4()
        {
            var query = Context.Guests
                               .Where(s => s.Age >= 18 && s.Age < 35)
                               .Select(x => new { x.FirstName, x.LastName })
                               .OrderBy(s => s.FirstName)
                               .ToList();

            return query.Select(x => new GuestNameDTO().InjectFrom(x) as GuestNameDTO).ToList();
        }

        public IEnumerable<StaffInfoDTO> Query5()
        {

            var query = Context.Staffs
                               .Where(x => x.LocationId == null)
                               .Select(x => new { x.FirstName, x.LastName, x.Email, x.Phone })
                               .ToList();

            return query.Select(x => new StaffInfoDTO().InjectFrom(x) as StaffInfoDTO).ToList();
        }

        public IEnumerable<LocationsDTO> Query6()
        {

            var query = Context.Locations
                               .Where(x => x.Address.Contains("Iasi"))
                               .ToList();

            return query.Select(x => new LocationsDTO().InjectFrom(x) as LocationsDTO).ToList();
        }

        public IEnumerable<StaffInfoDTO> Query7()
        {
            var query = Context.Staffs
                               .Join(Context.StaffRoles, s => s.StaffRoleId, sf => sf.Id, (s, sf) => new { s, sf })
                               .Where(x => (x.sf.Name.Contains("DJ") || x.sf.Name.Contains("Photographer") || x.sf.Name.Contains("Performer")) && x.s.Fee >= 1500)
                               .Select(x => new { x.s.LastName, x.s.FirstName })
                               .ToList();

            return query.Select(x => new StaffInfoDTO().InjectFrom(x) as StaffInfoDTO).ToList();
        }

        public IEnumerable<GuestsDTO> Query8()
        {
            var query = Context.Guests
                               .Join(Context.EventGuests, g => g.Id, eg => eg.GuestId, (g, eg) => new { g, eg })
                               .Join(Context.Events, ep => ep.eg.EventId, e => e.Id, (ep, e) => new { ep, e })
                               .Where(x => x.e.Name.Contains("Expert Network Christmas Party") && x.ep.eg.ConfirmedAttendence)
                               .Select(x => new { x.ep.g.FirstName, x.ep.g.LastName, x.ep.g.Email })
                               .ToList();

            return query.Select(x => new GuestsDTO().InjectFrom(x) as GuestsDTO).ToList();
        }

        public IEnumerable<GuestsDTO> Query9()
        {
            var query = Context.Guests
                              .Join(Context.EventGuests, g => g.Id, eg => eg.GuestId, (g, eg) => new { g, eg })
                              .Join(Context.Events, ep => ep.eg.EventId, e => e.Id, (ep, e) => new { ep, e })
                              .Join(Context.EventTypes, egp => egp.e.EventTypeId, et => et.Id, (egp, et) => new { egp, et })
                              .Where(x => x.egp.ep.eg.HasAttended && x.et.Name.Contains("wedding"))
                              .OrderByDescending(x => x.egp.ep.eg.GiftAmount)
                              .Take(5)
                              .ToList();

            return query.Select(x => new GuestsDTO().InjectFrom(x.egp.ep.g) as GuestsDTO);
        }

        public IEnumerable<LocationShortInfoDTO> Query10()
        {
            return Context.Locations
                             .Join(Context.Events, l => l.Id, e => e.LocationId, (l, e) => new { l, e })
                             .Where(x => x.e.StartTime.Year > DateTime.Today.Year)
                             .Select(x => new LocationShortInfoDTO { Name = x.l.Name, Address = x.l.Address })
                             .Distinct()
                             .ToList();
        }

        public IEnumerable<LocationsDTO> Query11()
        {
            var query = Context.Locations
                             .Join(Context.Events, l => l.Id, e => e.LocationId, (l, e) => new { l, e })
                             .Join(Context.EventGuests, le => le.e.Id, eg => eg.EventId, (le, eg) => new { le, eg })
                             .Join(Context.Guests, leeg => leeg.eg.GuestId, g => g.Id, (leeg, g) => new { leeg, g })
                             .Where(x => x.leeg.eg.HasAttended)
                             .OrderByDescending(x => x.g.eventGuests.Count())
                             .ToList();



            return query.GroupBy(x => new { x.leeg.le.e.Location })
                        .Select(x => new LocationsDTO().InjectFrom(new
                        {
                            x.Key.Location.Id,
                            x.Key.Location.Name,
                            x.Key.Location.Address,
                            x.Key.Location.Capacity,
                            x.Key.Location.RentFee
                        }) as LocationsDTO)
                         .ToList()
                         .Take(5);
        }

        public IEnumerable<GuestShortInfoDTO> Query12()
        {
            var query = Context.Guests
                               .Join(Context.EventGuests, g => g.Id, eg => eg.GuestId, (g, eg) => new { g, eg })
                               .Join(Context.Events, geg => geg.eg.EventId, e => e.Id, (geg, e) => new { geg, e })
                               .Join(Context.EventTypes, gege => gege.e.EventTypeId, et => et.Id, (gege, et) => new { gege, et })
                               .Where(x => x.et.Name.Contains("wedding") && x.gege.geg.eg.ConfirmedAttendence)
                               .ToList();
            return query.Select(x => new GuestShortInfoDTO().InjectFrom(x.gege.geg.g) as GuestShortInfoDTO);
        }

        public void Query13()
        {
            var query = Context.Locations
                             .Join(Context.Events, l => l.Id, e => e.LocationId, (l, e) => new { l, e })
                             .Where(x => x.e.StartTime.Year == 2019 && (x.e.StartTime.Month < 5 || x.e.StartTime.Month > 9))
                             .ToList();

            query.ForEach(x => Console.WriteLine(x));
        }

        public void Query14()
        {
            var query = Context.Events
                             .GroupBy(e => e.StartTime.Month)
                             .Select(g => new { month = g.Key, count = g.Count() })
                             .OrderByDescending(rez => rez.count)
                             .ToList();

            query.ForEach(x => Console.WriteLine(x));
        }

        public void Query15()
        {
            var query = Context.Staffs
                             .Join(Context.Locations, s => s.LocationId, l => l.Id, (s, l) => new { s, l })
                             .Join(Context.Events, sl => sl.l.Id, e => e.LocationId, (sl, e) => new { sl, e })
                             .Where(x => x.e.Description.Contains("Best wedding ever"))
                             .ToList();

            query.ForEach(x => Console.WriteLine(x));
        }

        public void Query16()
        {
            var query = Context.StaffRoles
                             .Join(Context.Staffs, sr => sr.Id, s => s.StaffRoleId, (sr, s) => new { sr, s })
                             .GroupBy(sr => new { sr.s.StaffRoleId, max = sr.s.Fee, min = sr.s.Fee })
                             .Select(x => new { x.Key.max }).Max(x => x.max);
        }
    }
}
