using EcoScan1.DTOs;

namespace EcoScan1.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<object>> GetAllUsers();
        Task<object> GetUserById(int id);
        Task UpdateUser(int id, UpdateUserDto dto);
        Task DeleteUser(int id);
    }
}
