using TrainApp.DTOs;

namespace TrainApp.Services
{
    public interface IStationService
    {
        Task<StationDto> CreateStationAsync(CreateStationDto dto);
        Task<List<StationDto>> GetAllStationsAsync();

        Task<StationDto> GetStationByIdAsync(int ind);

        Task<bool> UpdateStationAsync(int id, CreateStationDto dto);

        Task<bool> DeleteStationAsync(int id);
    }
}
