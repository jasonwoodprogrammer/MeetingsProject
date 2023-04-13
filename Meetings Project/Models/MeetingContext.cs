using Microsoft.EntityFrameworkCore;

namespace Meetings_Project.Models
{
    public class MeetingContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        public string DbPath { get; }

        public MeetingContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "meetings.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

    }
}
