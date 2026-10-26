using EcoScan1.DTOs;
using EcoScan1.Model;

namespace EcoScan1.Repositories.admin
{
    public interface IAdminRepository
    {
        Task<Admin?> FindAdmin(int id);
        Task<Admin?> FindAdmin(string email);
        Task<Admin> AddAdmin(Admin admin);
        Task<IEnumerable<Admin>> GetAllAdmins();
        Task UpdateAdmin(Admin admin);
        Task DeleteAdmin(int id);
        Task<bool> EmailExists(string email);
    }
}
