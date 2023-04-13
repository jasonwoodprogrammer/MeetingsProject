using Meetings_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meetings_Project.Controllers
{
    public class MeetingController : Controller
    {
        private readonly ILogger<MeetingController> _logger;
        private readonly MeetingContext _db;

        public MeetingController(ILogger<MeetingController> logger, MeetingContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Calendar()
        {
            var meetings = 
                _db.Meetings
                .Include(x => x.Persons)
                .Include(x => x.Room)
                .OrderBy(x => x.StartTime)
                .ToList();

            return View(meetings);
        }

        public IActionResult NewMeeting()
        {
            var newMeeting = new NewMeeting();

            PopulateNewMeetingViewData();
            return View(newMeeting);
        }

        [HttpPost]
        public IActionResult NewMeeting(NewMeeting newMeeting)
        {
            PopulateNewMeetingViewData();

            if (ModelState.IsValid)
            {

                //check for overlapping rooms

                var overlappingMeetings = _db.Meetings.Where(x =>
                    x.StartTime < newMeeting.MeetingStart.AddMinutes(newMeeting.Duration) &&
                    newMeeting.MeetingStart < x.StartTime.AddMinutes(x.Duration)).ToList();

                // Meeting room cannot be in use at this time
                if (overlappingMeetings.Where(x => x.Room.Id == newMeeting.MeetingRoomId).Any())
                {
                    var errorMeeting = overlappingMeetings.Where(x => x.Room.Id == newMeeting.MeetingRoomId).First();

                    //error
                    ModelState.AddModelError(string.Empty,
                        $"This meeting room is already in use at this time - {errorMeeting.StartTime} to {errorMeeting.StartTime.AddMinutes(errorMeeting.Duration)}");

                    return View(newMeeting);
                }

                //check for overlapping persons

                var overlappingPersons = overlappingMeetings
                    .SelectMany(x => x.Persons)
                    .Where(att => newMeeting.PersonIds.Contains(att.Id))
                    .Distinct().ToList();

                if (overlappingPersons.Any())
                {
                    foreach (var person in overlappingPersons)
                    {
                        //error
                        ModelState.AddModelError(string.Empty,
                         $"{person.FirstName} {person.LastName} is already in a meeting during this time.");

                    }

                    return View(newMeeting);
                }

                //create meeting

                var meeting = new Meeting
                {
                    StartTime = newMeeting.MeetingStart,
                    Duration = newMeeting.Duration,
                    Room = _db.Rooms.Where(x => x.Id == newMeeting.MeetingRoomId).First(),
                    Persons = _db.Persons.Where(x => newMeeting.PersonIds.Contains(x.Id)).ToList()
                };

                _db.Add(meeting);
                _db.SaveChanges();

                //return a success string, this will make the page show a success message
                ViewData["successMessage"] = $"Meeting set up at {meeting.StartTime} for {meeting.Duration} minutes";

            }

            return View(newMeeting);
        }

        private void PopulateNewMeetingViewData()
        {
            ViewData["persons"] = _db.Persons.ToList();
            ViewData["rooms"] = _db.Rooms.ToList();
        }
    }
}
