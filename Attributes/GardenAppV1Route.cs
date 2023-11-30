using Microsoft.AspNetCore.Mvc;

namespace GardenApp.API.Attributes
{
    public class GardenAppV1Route : RouteAttribute
    {
        public GardenAppV1Route() : base("api/v1/[controller]")
        { }
    }
}
