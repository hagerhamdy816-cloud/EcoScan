using EcoScan1.DTOs;
using EcoScan1.Model;

namespace EcoScan1.Repositories.user
{
    public interface IUserRepository
    {
        Task<User?> FindUser(int id);
        Task<User?> FindUser(string email);
        Task <User> AddUser(User user);
        Task <IEnumerable<User>> GetAllUsers();
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task <bool> EmailExists(string email);
    }
}
