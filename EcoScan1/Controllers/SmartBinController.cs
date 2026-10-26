using EcoScan1.DTOs;
using EcoScan1.Model;
using EcoScan1.Repositories.smartBin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcoScan1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartBinController : ControllerBase
    {
        private readonly ISmartBinRepository _smartBinRepository;
        public SmartBinController(ISmartBinRepository smartBinRepository)
        {
            _smartBinRepository = smartBinRepository;
        }
        // =========================
        // CREATE BIN
        // =========================
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateBin(SmartBinDto smartBin)
        {
            try
            {
                var claim = User.FindFirst("id");
                if (claim == null) {
                    return Unauthorized(new
                    {
                        success = false,
                        message = "Invalid token"
                    });
                }
                var adminId = int.Parse(claim.Value);
                smartBin.AdminId = adminId;

                var bin = await _smartBinRepository.AddSmartBin(smartBin);
                return Ok(new
                {
                    success = true, // اتضاف
                    message = "Smart Bin created successfully",
                    data = new // اتنظم البيانات اللي هترجع
                    {
                        binId = bin.SmartBinId,
                        adminId = bin.AdminId,
                        location = bin.Location,
                        capacityKg = bin.CapacityKg,
                        currentFillKg = bin.CurrentFillKg,
                        hasCamera = bin.HasCamera,
                        status = bin.Status
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false, // مافيش
                    message = "Failed to create Smart Bin",
                    error = ex.Message
                });
            }
        }

        // =========================
        // GET ALL BINS
        // =========================
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllBins()
        {
            var bins = await _smartBinRepository.GetAllBins();
            var result = bins.Select(b => new
            {
                binId = b.SmartBinId,
                adminId = b.AdminId,
                location = b.Location,
                capacityKg = b.CapacityKg,
                currentFillKg = b.CurrentFillKg,
                hasCamera = b.HasCamera,
                status = b.Status
            });
            if (!result.Any())
                return Ok(new
                {
                    success = true,
                    message = "No Smart Bins found",
                    data = new List<object>()
                });
            return Ok(new
            {
                success = true,
                message = "Smart Bins retrieved successfully",
                data = result
            });
        }

        // =========================
        // GET BIN BY ID
        // =========================
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBinById(int id)
        {
            var bin = await _smartBinRepository.FindBin(id);
            if (bin == null)
                return NotFound(new
                {
                    success = false,
                    message = $"Smart Bin with ID {id} not found"
                });
            return Ok(new {
                success = true,
                message = "Smart Bin retrieved successfully",
                data = new
                {
                    binId = bin.SmartBinId,
                    adminId = bin.AdminId,
                    location = bin.Location,
                    capacityKg = bin.CapacityKg,
                    currentFillKg = bin.CurrentFillKg,
                    hasCamera = bin.HasCamera,
                    status = bin.Status
                }
            });
        }

        // =========================
        // GET MY BINS 
        // =========================
        [Authorize(Roles ="Admin")]
        [HttpGet("my-bins")]
        public async Task<IActionResult> GetMyBins()
        {
            var claim = User.FindFirst("id");
            if (claim == null)
                return Unauthorized(new
                {
                    success = false
                });
            var adminId = int.Parse(claim.Value);
            var bins = await _smartBinRepository.GetBinsByAdminId(adminId);
            var result = bins.Select(b => new
            {
                binId = b.SmartBinId,
                adminId = b.AdminId,
                location = b.Location,
                capacityKg = b.CapacityKg,
                currentFillKg = b.CurrentFillKg,
                hasCamera = b.HasCamera,
                status = b.Status
            });
            return Ok(new
            {
                success = true,
                message = "Your bins retrieved successfully",
                data = result
            });
        }

        // =========================
        // UPDATE BIN
        // =========================
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBin(int id, UpdateSmartBinDto dto)
        {
            var existingBin = await _smartBinRepository.FindBin(id);

            if (existingBin == null)
                return NotFound(new
                {
                    success = false,
                    message = $"Smart Bin with ID {id} not found"
                });

            var claim = User.FindFirst("id");
            if (claim == null)
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid token"
                });

            if (existingBin.AdminId != int.Parse(claim.Value))
                return Forbid();

            // ✅ Update safely
            existingBin.Location = dto.Location ?? existingBin.Location;

            if (dto.CapacityKg.HasValue)
                existingBin.CapacityKg = (decimal)dto.CapacityKg.Value;

            if (dto.CurrentFillLevelKg.HasValue)
                existingBin.CurrentFillKg = (decimal)dto.CurrentFillLevelKg.Value;

            if (dto.HasCamera.HasValue)
                existingBin.HasCamera = dto.HasCamera.Value;

            existingBin.Status = dto.Status ?? existingBin.Status;

            try
            {
                await _smartBinRepository.UpdateSmartBin(existingBin);

                return Ok(new
                {
                    success = true,
                    message = "Smart Bin updated successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Failed to update Smart Bin",
                    error = ex.Message
                });
            }
        }

        // =========================
        // DELETE BIN
        // =========================
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBin(int id)
        {
           var existingBin = await _smartBinRepository.FindBin(id);
            if (existingBin == null)
                return NotFound(new
                {
                    success = false,
                    message = $"Smart Bin with ID {id} not found"
                });
            var claim = User.FindFirst("id");
            if (claim == null)
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid token"
                });
            if (existingBin.AdminId != int.Parse(claim.Value))
                return Forbid();
                await _smartBinRepository.DeleteSmartBin(id);
            return Ok(new
            {
                success = true,
                message = $"Smart Bin with ID {id} deleted successfully."
            });
            //try
            //{
            //    await _smartBinRepository.DeleteSmartBin(id);
            //    return Ok(new
            //    {
            //        success = true,
            //        message = $"Smart Bin with ID {id} deleted successfully."
            //    });
            //}
            //catch (Exception ex)
            //{
            //    return NotFound(new
            //    {
            //        success = false,
            //        message = ex.Message
            //    });
            //}
        }

    }
}
