using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using EventApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Current(int id)
        {
            var user = new UserViewModel()
            {
                Id = id,
                Address = "Str. Barbu Lautaru nr. 11",
                FirstName = "Bogdan",
                LastName = "Tanasuca",
                BirthDay = new DateTime(1997, 8, 7),
                Email = "bogdan.tanasuca@expertnetwork.ro",
                Weight = 56.5,
                Height = 1.84
            };
            if (id != 1)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(user);
        }

        [Route("admin/user/getusers")]
        public IActionResult GetUsers()
        {
            var list = new List<UserViewModel>();
            var user = new UserViewModel()
            {
                Id = 1,
                Address = "Str. Barbu Lautaru nr. 11",
                FirstName = "Bogdan",
                LastName = "Tanasuca",
                BirthDay = new DateTime(1997, 8, 7),
                Email = "bogdan.tanasuca@expertnetwork.ro",
                Weight = 56.5,
                Height = 1.84
            };
            list.Add(user);
            for (int i = 1; i < 5; i++)
            {
                var newId = user.Id + i;
                var tempUser = new UserViewModel
                {
                    Id = newId,
                    Address = "Str. Barbu Lautaru nr. 11",
                    FirstName = "Bogdan",
                    LastName = "Tanasuca",
                    BirthDay = new DateTime(1997, 8, 7),
                    Email = "bogdan.tanasuca@expertnetwork.ro",
                    Weight = 56.5,
                    Height = 1.84
                };
                list.Add(tempUser);
            }
            return View(list);
        }

        [Route("GetMyIp")]
        public IActionResult GetMyIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            string userIP = null;
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return View(new IpViewModel() { Ip = ip.ToString() });
                }
            }

            return RedirectToAction("Error", "Home");

        }
    }
}