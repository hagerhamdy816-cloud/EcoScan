using EcoScan1.DTOs;
using EcoScan1.Interfaces;
using EcoScan1.Model;
using EcoScan1.Repositories.admin;
using EcoScan1.Repositories.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoScan1.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IJwtService _jwtService;

        public AuthService(IUserRepository userRepository, IAdminRepository adminRepository ,IJwtService jwtService)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _jwtService = jwtService;
        }

        // ============== USER ==============
        
        // =========================
        // USER REGISTER
        // =========================
        public async Task<AuthResponseDto> RegisterUser(RegisterDto dto)
        {
            // Validation

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new Exception("Email is required");
            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new Exception("Password is required");

            var existing = await _userRepository.FindUser(dto.Email.ToLower());
            if (existing != null)
                throw new Exception("Email already exists");

            // create user
            var user = new User
            {
                Email = dto.Email.ToLower(),
                UserName = dto.Name,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Phone = dto.Phone,
                CreatedAt = DateTime.UtcNow,
            };

            await _userRepository.AddUser(user);

           // var token = _jwtService.GenerateUserToken(user);
            return new AuthResponseDto
            {
                Token = _jwtService.GenerateUserToken(user),
                Id = user.UserId,
                Name = user.UserName,
                Email = user.Email
            };
        }


        // =========================
        // USER LOGIN
        // =========================
        public async Task<AuthResponseDto> LoginUser(LoginDto loginDto)
        {
            var user = await _userRepository.FindUser(loginDto.Email.ToLower());

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                throw new Exception("Invalid email or password");

            var token = _jwtService.GenerateUserToken(user);
            return new AuthResponseDto
            {
                Token = _jwtService.GenerateUserToken(user),
                Id = user.UserId,
                Name = user.UserName,
                Email = user.Email
            }
            ;
        }

        // ============== ADMIN ==============

        // =========================
        // ADMIN REGISTER
        // =========================
        public async Task<AuthResponseDto> RegisterAdmin(RegisterDto dto)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new Exception("Email is required");

            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new Exception("Password is required");

            var existing = await _adminRepository.FindAdmin(dto.Email.ToLower());
            if (existing != null)
                throw new Exception("Email already exists");

            // create user
            var admin = new Admin
            {
                Email = dto.Email.ToLower(),
                AdminName = dto.Name,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            };

            await _adminRepository.AddAdmin(admin);

            //var token = _jwtService.GenerateAdminToken(admin);
            return new AuthResponseDto
            {
                Token = _jwtService.GenerateAdminToken(admin),
                Id = admin.AdminId,
                Name = admin.AdminName,
                Email = admin.Email
            };
        }


        // =========================
        // ADMIN LOGIN
        // =========================
        public async Task<AuthResponseDto> LoginAdmin(LoginDto loginDto)
        {
            var admin = await _adminRepository.FindAdmin(loginDto.Email.ToLower());

            if (admin == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, admin.PasswordHash))
                throw new Exception("Invalid email or password");

            var token = _jwtService.GenerateAdminToken(admin);
            return new AuthResponseDto
            {
                Token = _jwtService.GenerateAdminToken(admin),
                Id = admin.AdminId,
                Name = admin.AdminName,
                Email = admin.Email
            };
        }
    }
}
