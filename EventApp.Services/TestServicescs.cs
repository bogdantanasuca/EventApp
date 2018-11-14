using EventApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventApp.Services
{
    public class TestServicescs
    {
        public static EventAppContext Context = new EventAppContext();

        public void Query3()
        {
            var query = Context.Guests
                               .Where(s => s.Age >= 18)
                               .ToList();

            query.ForEach(x => Console.WriteLine(x));

        }
        public void Query4()
        {
            var query = Context.Guests
                               .Where(s => s.Age >= 18 && s.Age < 35)
                               .Select(x => new { x.FirstName, x.LastName })
                               .OrderBy(s => s.FirstName)
                               .ToList();

            query.ForEach(x => Console.WriteLine(x));

        }
        public void Query5()
        {

            var query = Context.Staffs
                               .Where(x => x.LocationId == null)
                               .Select(x => new { x.FirstName, x.LastName, x.Email, x.Phone })
                               .ToList();

            query.ForEach(x => Console.WriteLine(x));

        }

        public void Query6()
        {

            var query = Context.Locations
                               .Where(x => x.Address.Contains("Iasi"))
                               .Select(x => new { x.Name })
                               .ToList();

            query.ForEach(x => Console.WriteLine(x));

        }

        public void Query7()
        {
            var query = Context.Staffs
                               .Join(Context.StaffRoles, s => s.StaffRoleId, sf => sf.Id, (s, sf) => new { s, sf })
                               .Where(x => (x.sf.Name.Contains("DJ") || x.sf.Name.Contains("Photographer") || x.sf.Name.Contains("Performer")) && x.s.Fee >= 1500)
                               .Select(x => new { x.s.LastName, x.s.FirstName })
                               .ToList();

            query.ForEach(x => Console.WriteLine(x));

        }
    }
}
