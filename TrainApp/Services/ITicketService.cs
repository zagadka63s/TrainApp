using TrainApp.DTOs;

namespace TrainApp.Services
{
    public interface ITicketService
    {
        /// <summary>
        /// Create a new ticket ( reservation / purchase ) 
        /// </summary>

        Task<TicketDto> CreateTicketAsync(CreateTicketDto dto);

        /// <summary>
        /// Get list all ticket ( for admin ) 
        /// </summary>
        Task<List<TicketDto>> GetAllTicketsAsync();


        ///<summary>
        ///Get ticket by id ( details )
        /// </summary>
        Task<TicketDto> GetTicketByIdAsync(int id);
        
        ///<summary>
        ///Update ticket by id ( status / place / price )
        /// </summary>
        /// 
        Task<bool> UpdateTicketAsync(int id, CreateTicketDto dto);

        ///<summary>
        ///Delete ticket by id ( canceled reservation )
        /// </summary>
        
        Task<bool> DeleteTicketAsync(int id);

    }
}
