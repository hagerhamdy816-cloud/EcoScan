using EcoScan1.DTOs;
using EcoScan1.Interfaces;
using EcoScan1.Repositories.admin;

namespace EcoScan1.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<IEnumerable<object>> GetAllAdmins()
        {
            var admins = await _adminRepository.GetAllAdmins();

            return admins.Select(a => new
            {
                a.AdminId,
                a.AdminName,
                a.Email,
                a.Phone
            });
        }

        public async Task<object> GetAdminById(int id)
        {
            var admin = await _adminRepository.FindAdmin(id);

            if (admin == null)
                throw new Exception("Admin not found");

            return new
            {
                admin.AdminId,
                admin.AdminName,
                admin.Email,
                admin.Phone
            };
        }

        public async Task UpdateAdmin(int id, UpdateAdminDto dto)
        {
            var admin = await _adminRepository.FindAdmin(id);

            if (admin == null)
                throw new Exception("Admin not found");

            if (!string.IsNullOrEmpty(dto.Email) && dto.Email != admin.Email)
            {
                if (await _adminRepository.EmailExists(dto.Email))
                    throw new Exception("Email already exists");

                admin.Email = dto.Email.ToLower();
            }

            admin.AdminName = dto.AdminName ?? admin.AdminName;
            admin.Phone = dto.Phone ?? admin.Phone;

            await _adminRepository.UpdateAdmin(admin);
        }

        public async Task DeleteAdmin(int id)
        {
            await _adminRepository.DeleteAdmin(id);
        }
    }
}
