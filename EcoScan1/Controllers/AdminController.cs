using EcoScan1.DTOs;
using EcoScan1.Interfaces;
using EcoScan1.Model;
using EcoScan1.Repositories.admin;
using EcoScan1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EcoScan1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        public readonly IAdminService _adminService;
        //public readonly JwtService _jwtService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
            //_jwtService = jwtService;
        }

        // =========================
        // REGISTER
        // =========================
        //[HttpPost("Register")]
        //public async Task<IActionResult> Register(RegisterDto dto)
        //{
        //    try
        //    {
        //        var admin = await _adminRepository.AddAdmin(dto);
        //        return Ok(new
        //        {
        //            message = "Admin registered  successfully",
        //            adminId = admin.AdminId,
        //            adminName = admin.AdminName,
        //            email = admin.Email
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //// =========================
        //// LOGIN
        //// =========================
        //[HttpPost("login")]
        //public async Task<IActionResult> Login(LoginDto dto)
        //{
        //    // 1) find user by email
        //    var admin = await _adminRepository.FindAdmin(dto.Email);

        //    // 2) verify password
        //    if (admin == null || !BCrypt.Net.BCrypt.Verify(dto.Password, admin.PasswordHash))
        //        return Unauthorized("Invalid email or password.");

        //    // 3) generate and return token
        //    var token = _jwtService.GenerateToken(admin);
        //    return Ok(new
        //    {
        //        Token = token,
        //        adminId = admin.AdminId,
        //        adminName = admin.AdminName,
        //        email = admin.Email
        //    });
        //}

        // =========================
        // GET ALL ADMINS
        // =========================
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _adminService.GetAllAdmins();

            return Ok(new
            {
                success = true,
                data = result
            });
        }


        // =========================
        // GET ADMIN BY ID
        // =========================
        // [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _adminService.GetAdminById(id);

                return Ok(new
                {
                    success = true,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }



        // =========================
        // UPDATE ADMIN
        // =========================
        // [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAdminDto dto)
        {
            try
            {
                await _adminService.UpdateAdmin(id, dto);

                return Ok(new
                {
                    success = true,
                    message = "Admin updated successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // =========================
        // DELETE ADMIN
        // =========================
        // [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adminService.DeleteAdmin(id);

                return Ok(new
                {
                    success = true,
                    message = "Admin deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}