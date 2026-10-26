using EcoScan1.DTOs;
using EcoScan1.Interfaces;
using EcoScan1.Repositories.admin;
using EcoScan1.Repositories.user;
using EcoScan1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcoScan1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IUserRepository _userRepository;
        //private readonly IAdminRepository _adminRepository;
        //private readonly JwtService _jwtService;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // =========================
        // USER REGISTER
        // =========================
        [HttpPost("user/register")]
        public async Task<IActionResult> RegisterUser(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid input data."
                });
            try
            {
                var result = await _authService.RegisterUser(dto);
                return Ok(new
                {
                    success = true,
                    message = "User registered successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(409,new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        // =========================
        // USER LOGIN
        // =========================
        [HttpPost("user/login")]
        public async Task<IActionResult> LoginUser(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid input data."
                });
            try
            {
                var result = await _authService.LoginUser(dto);
                return Ok(new
                {
                    success = true,
                    message = "Login successful",
                    data = result
                });
            }
            catch(Exception ex)
            {
                return Unauthorized(new { success = false, message = ex.Message });
            }
            //var user = await _userRepository.FindUser(dto.Email.ToLower());

            //if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            //    return Unauthorized(new
            //    {
            //        success = false,
            //        message = "Invalid email or password."
            //    });

            //var token = _jwtService.GenerateToken(user);

            //return Ok(new
            //{
            //    success = true,
            //    message = "Login successful",
            //    data = new
            //    {
            //    token,
            //    userId = user.UserId,
            //    userName = user.UserName,
            //    email = user.Email,
            //    role = "User"
            //    }
            //});
        }

        // =========================
        // ADMIN REGISTER
        // =========================
        [HttpPost("admin/register")]
        public async Task<IActionResult> RegisterAdmin(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid input data"
                });
            try
            {
                var result = await _authService.RegisterAdmin(dto);
                return Ok(new
                {
                    success = true,
                    message = "Admin registered successfully",
                    data = result,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(409, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        // =========================
        // ADMIN LOGIN
        // =========================
        [HttpPost("admin/login")]
        public async Task<IActionResult> LoginAdmin(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid input data."
                });
            try
            {
                var result = await _authService.LoginAdmin(dto);
                return Ok(new
                {
                    success = true,
                    message = "Login successful",
                    data = result,
                });
            }
            catch(Exception ex)
            {
                return Unauthorized(new { success = false, message = ex.Message });
            }
            //var admin = await _adminRepository.FindAdmin(dto.Email.ToLower());

            //if (admin == null || !BCrypt.Net.BCrypt.Verify(dto.Password, admin.PasswordHash))
            //    return Unauthorized(new
            //    {
            //        success = false,
            //        message = "Invalid email or password."
            //    });

            //var token = _jwtService.GenerateToken(admin);

            //return Ok(new
            //{
            //    success = true,
            //    message = "Login successful",
            //    data = new
            //    {
            //    token,
            //    adminId = admin.AdminId,
            //    adminName = admin.AdminName,
            //    email = admin.Email,
            //    role = "Admin"
            //    }
            //});
        }
    }
}

