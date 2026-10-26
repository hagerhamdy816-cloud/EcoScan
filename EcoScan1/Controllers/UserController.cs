using EcoScan1.Data;
using EcoScan1.DTOs;
using EcoScan1.Interfaces;
using EcoScan1.Model;
using EcoScan1.Repositories.user;
using EcoScan1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcoScan1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // private readonly EcoScanDbContext _context;
        private readonly IUserService _userService;
        //private readonly JwtService _jwtService;
        public UserController(IUserService userService)
        {
            // _context = context;
            _userService = userService;
            //_jwtService = jwtService;
        }

        //[HttpPost("Create")]
        //public async Task<IActionResult> CreateUser([FromBody] RegisterDto user)
        //{

        //    if (user == null)
        //    {
        //        return BadRequest("User data is null");
        //    }
        //    await _userRepository.AddUser(user);

        //    return Ok("User created successfully");
        //}  

        //// =========================
        //// REGISTER
        //// =========================
        //[HttpPost("register")]
        //public async Task<IActionResult> Register(RegisterDto dto)
        //{
        //    try
        //    {
        //        var user = await _userRepository.AddUser(dto);
        //        return Ok(new
        //        {
        //            message = "User created successfully",
        //            userId = user.UserId,
        //            userName = user.UserName,
        //            email = user.Email
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
        //    var user = await _userRepository.FindUser(dto.Email);

        //    // 2) verify password
        //    if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        //        return Unauthorized("Invalid credentials");

        //    // 3) generate and return token
        //    var token = _jwtService.GenerateToken(user);
        //    return Ok(new
        //    {
        //        Token = token,
        //        UserId = user.UserId,
        //        UserName = user.UserName,
        //        Email = user.Email
        //    });
        //}

        // =========================
        // GET ALL USERS
        // =========================
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllUsers();

            return Ok(new
            {
                success = true,
                data = result
            });
        }


        // =========================
        // GET USER BY ID
        // =========================
        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _userService.GetUserById(id);

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
        // UPDATE USER
        // =========================
        [Authorize(Roles = "User, Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserDto dto)
        {
            try
            {
                await _userService.UpdateUser(id, dto);

                return Ok(new
                {
                    success = true,
                    message = "User updated successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // =========================
        // DELETE USER
        // =========================
        [Authorize (Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteUser(id);

                return Ok(new
                {
                    success = true,
                    message = "User deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}