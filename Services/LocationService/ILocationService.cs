using DATN_back_end.Dtos;
using DATN_back_end.Entities;

namespace DATN_back_end.Services.LocationService
{
    public interface ILocationService
    {
        Task<CustomResponse<List<ProvinceOrCity>>> GetProvincesOrCitiesAsync();
    }
}
