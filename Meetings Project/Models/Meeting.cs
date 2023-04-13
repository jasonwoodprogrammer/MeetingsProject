namespace Meetings_Project.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; } // In minutes

        public virtual Room Room { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
    }
}
