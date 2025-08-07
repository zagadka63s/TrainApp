using TrainApp.DTOs;

namespace TrainApp.Services
{
    
    /// <summary>
    /// Service for work with Coach
    /// </summary>
    public interface ICoachService
    {
        /// <summary>
        /// Create a new Coach
        /// </summary>

        Task<CoachDto> CreateCoachAsync(CreateCoachDto dto);

        /// <summary >
        /// Get list all Coachs ( train + coach for user / admin )
        /// </summary>
        
        Task<List<CoachDto>>GetAllCoachesAsync();

        /// <summary>
        /// Get Coach by id
        /// </summary>
        
        Task <CoachDto> GetCoachByIdAsync(int id);

        /// <summary>
        /// Update information about coach ( number ,type )
        /// </summary>
        
        Task<bool> UpdateCoachAsync(int id, CreateCoachDto dto);

        /// <summary>
        /// Delete coach by id
        /// </summary>
        /// 
        
        Task<bool> DeleteCoachAsync(int id);

    }
}
