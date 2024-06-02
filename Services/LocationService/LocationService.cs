using DATN_back_end.Dtos;
using DATN_back_end.Entities;
using Microsoft.EntityFrameworkCore;

namespace DATN_back_end.Services.LocationService
{
    public class LocationService : BaseService, ILocationService 
    {
        public LocationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        async Task<CustomResponse<List<ProvinceOrCity>>> ILocationService.GetProvincesOrCitiesAsync()
        {
            var provincesOrCities = await (await _unitOfWork.Queryable<ProvinceOrCity>()).ToListAsync();

            return new CustomResponse<List<ProvinceOrCity>>
            {
                Data = provincesOrCities
            };
        }
    }
}
