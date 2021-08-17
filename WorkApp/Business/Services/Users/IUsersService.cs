using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.Business.DTO;

namespace WorkApp.Business.Services.Users
{
    public interface IUsersService
    {
        Task CreateNewUserAsync(UserDTO user);
        void CreateNewUser(UserDTO user);
        Task<List<UserDTO>> GetStudents();
        Task<UserDTO> GetUserById(int id);
        Task UpdateUser(UserDTO user);
        Task DeleteUserById(int id);
    }
}
