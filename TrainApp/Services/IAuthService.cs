using TrainApp.DTOs;
using TrainApp.Entities;

namespace TrainApp.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Registration new user
        /// </summary>
        Task <bool> RegisterUserAsync(RegisterUserDto dto);

        /// <summary>
        /// Autorization new user and back jwt token
        /// </summary>
        Task <string> LoginUserAsync(LoginDto dto); 
        

        Task<User> GetUserByIdAsync(int userId);
    }
}
