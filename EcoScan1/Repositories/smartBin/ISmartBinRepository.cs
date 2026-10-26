using EcoScan1.DTOs;
using EcoScan1.Model;

namespace EcoScan1.Repositories.smartBin
{
    public interface ISmartBinRepository
    {
        Task<SmartBin> AddSmartBin(SmartBinDto smartBin);
        Task<SmartBin?> FindBin(int id);
        Task<IEnumerable<SmartBin>> GetAllBins();
        Task<IEnumerable<SmartBin>> GetBinsByAdminId(int adminId);
        Task UpdateSmartBin(SmartBin bin);
        Task DeleteSmartBin(int id);
        Task<bool> Exists(int id);

    }
}
