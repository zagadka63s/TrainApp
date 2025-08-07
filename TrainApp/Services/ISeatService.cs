using TrainApp.DTOs;

namespace TrainApp.Services
{
    /// <summary>
    /// Service for work with seat
    /// </summary>
    public interface ISeatService
    {
        /// <summary>
        /// Create a new place
        /// </summary>
        Task<SeatDto> CreateSeatAsync(CreateSeatDto dto);

        ///<summary>
        ///Get list all place
        /// </summary>
        Task<List<SeatDto>> GetAllSeatsAsync();

        ///<summary>
        ///Get place by id
        /// </summary>
        Task<SeatDto> GetSeatByIdAsync(int id);

        ///<summary>
        ///Update information about place ( number , status )
        /// </summary>
        Task<bool> UpdateSeatAsync(int id, CreateSeatDto dto);


        ///<summary>
        ///Delete place by id
        /// </summary>

        Task<bool> DeleteSeatAsync(int id);
        
        
    }
}
