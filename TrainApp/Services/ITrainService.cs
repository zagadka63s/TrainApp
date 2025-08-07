using TrainApp.DTOs;

namespace TrainApp.Services
{
    public interface ITrainService
    {
        Task<TrainDto> CreateTrainAsync(CreateTrainDto dto);
        Task<List<TrainDto>> GetAllTrainsAsync();
        Task<TrainDetailsDto> GetTrainByIdAsync(int id);
        Task<bool> UpdateTrainAsync(int id, UpdateTrainDto dto);
        Task<bool> DeleteTrainAsync(int id);

    }
}
