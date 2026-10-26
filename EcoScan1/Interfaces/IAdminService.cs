using EcoScan1.DTOs;

namespace EcoScan1.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<object>> GetAllAdmins();
        Task<object> GetAdminById(int id);
        Task UpdateAdmin(int id, UpdateAdminDto dto);
        Task DeleteAdmin(int id);
    }
}
