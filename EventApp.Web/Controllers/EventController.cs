using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.DTOs;
using EventApp.Services.Events;
using EventApp.Services.Guests;
using EventApp.Web.Models.Event;
using EventApp.Web.Models.Guest;
using Microsoft.AspNetCore.Mvc;
using Omu.ValueInjecter;

namespace EventApp.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly IGuestService guestService;


        public EventController(IEventService eventService, IGuestService guestService)
        {
            this.eventService = eventService;
            this.guestService = guestService;
        }

        [HttpGet]
        public IActionResult Index(string eventName)
        {
            var eventModels = new List<EventViewModel>();
            if (eventName != null)
            {
                eventModels = eventService.GetEventsByName(eventName).Select(x => (EventViewModel)new EventViewModel().InjectFrom(x)).ToList();
            }
            else
            {
                eventModels = eventService.GetEvents().Select(x => (EventViewModel)new EventViewModel().InjectFrom(x)).ToList();
            }
            return View(eventModels);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (eventService.GetEventById(id) == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View((EventViewModel)new EventViewModel().InjectFrom(eventService.GetEventById(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind] EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                eventService.UpdateEvent((EventDTO)new EventDTO().InjectFrom(eventViewModel));
                return RedirectToAction("Index");
            }
            return View(eventViewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View((EventViewModel)new EventViewModel().InjectFrom(eventService.GetEventById(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind] EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                eventService.DeleteEventByID(eventViewModel.Id);
                return RedirectToAction("Index");
            }
            return View(eventViewModel);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            return View(new EventViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                var eventim = new EventDTO()
                {
                    LocationId = eventViewModel.LocationId,
                    EventTypeId = eventViewModel.EventTypeId,
                    Name = eventViewModel.Name,
                    StartTime = eventViewModel.StartTime,
                    EndTime = eventViewModel.EndTime,
                    EstimatedBudget = eventViewModel.EstimatedBudget,
                    Description = eventViewModel.Description
                };
                eventService.CreateEvent(eventim);
                return RedirectToAction("Index");
            }
            return View(eventViewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var listOfGuests = guestService.GetGuestsByEventId(id);
            var eventim = eventService.GetEventById(id);
            var eventdetails = new EventDetailsViewModel()
            {
                LocationId = eventim.LocationId,
                EventTypeId = eventim.EventTypeId,
                Name = eventim.Name,
                StartTime = eventim.StartTime,
                EndTime = eventim.EndTime,
                EstimatedBudget = eventim.EstimatedBudget,
                Description = eventim.Description,
                EventGuests = listOfGuests.Select(x => (GuestViewModel)new GuestViewModel().InjectFrom(x)).ToList()
            };
            return View(eventdetails);
        }
    }
}