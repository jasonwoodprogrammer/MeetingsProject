using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Meetings_Project.Models
{
    public class NewMeeting
    {
        public NewMeeting()
        {
            //to start the user on the same time and day
            MeetingStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
        }

        [Required]
        public int MeetingRoomId { get; set; }

        [Required]
        [MinLength(2)]
        public List<int> PersonIds { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime MeetingStart { get; set; }

        [Required]
        public int Duration { get; set; } // In Minutes
    }
}
