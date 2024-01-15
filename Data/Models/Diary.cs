namespace GardenApp.API.Data.Models
{
    public class Diary
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        // Relacje
        public virtual User User { get; set; }
        public virtual ICollection<Plant> Plants { get; set; }
        public virtual ICollection<Calendar> Calendars { get; set; }
    }
}