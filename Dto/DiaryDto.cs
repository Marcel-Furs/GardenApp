namespace GardenApp.API.Dto
{
    public class DiaryDto
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        public List<int> PlantIds { get; set; }
        public List<int> CalendarIds { get; set; }
    }
}
