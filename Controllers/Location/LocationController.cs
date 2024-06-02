using DATN_back_end.Dtos.User;
using DATN_back_end.Filters;
using DATN_back_end.Services.LocationService;
using DATN_back_end.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace DATN_back_end.Controllers.Location
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeFilter]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("provinces-or-cities")]
        public async Task<IActionResult> GetProvincesOrCitiesAsync()
        {
            var provincesOrCities = await _locationService.GetProvincesOrCitiesAsync();
            return Ok(provincesOrCities);
        }        
    }
}
