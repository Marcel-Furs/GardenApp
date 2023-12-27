using GardenApp.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static System.Collections.Specialized.BitVector32;

namespace GardenApp.API.Data.Repositories
{
    public class CalendarService : BaseCrudRepository<Calendar, int>
    {
        public CalendarService(DataContext context): base(context)
        {

        }
        //public override async Task<List<Calendar>> GetAll()
        //{
        //    return await context.Calendars.FindAll(x => x.User.Id == userId).ToList();
        //}
    }
}
