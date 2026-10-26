using EcoScan1.DTOs;
using EcoScan1.Interfaces;
using EcoScan1.Model;
using EcoScan1.Repositories.scan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcoScan1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ScanController : ControllerBase
    {
        private readonly IScanService _scanService;
        public ScanController(IScanService scanService)
        {
            _scanService = scanService;
        }


        // =========================
        // CREATE SCAN
        // =========================
        [HttpPost]
        public async Task <IActionResult> Create([FromBody]CreateScannedItemDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            if (userId == null)
                return Unauthorized(new { success = false, message = "Invalid token" });

            dto.UserId = userId.Value;

            var result = await _scanService.CreateScan(dto);

            return Ok(new
            {
                success = true,
                message = "Scan created successfully",
                data = result
            });
        }

        // =========================
        // GET ALL SCANS (Admin)
        // =========================
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllScans()
        {
       
            var result = await _scanService.GetAllScans();

            return Ok(new
            {
                success = true,
                data = result
            });

        }


        // =========================
        // GET MY SCANS
        // =========================
        [Authorize]
        [HttpGet("my-scans")]
        public async Task<IActionResult> GetMyScans()
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized(new { success = false });

            var result = await _scanService.GetScanByUserId(userId.Value);

            return Ok(new
            {
                success = true,
                data = result
            });
        }

        // =========================
        // GET SCAN BY ID
        // =========================
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetScanById(int id)
        {
            try
            {
                var result = await _scanService.GetScanById(id);
                var userId = GetUserId();
                if (userId == null)
                    return Unauthorized(new { success = false });

                if (result.UserId != userId.Value && !User.IsInRole("Admin"))
                    return Forbid();

                return Ok(new
                {
                    success = true,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }

        }

        // =========================
        // UPDATE STATUS (Admin only)
        // =========================
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScan(int id, UpdateScanDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _scanService.UpdateScan(id, dto);

                return Ok(new
                {
                    success = true,
                    message = "Scan updated successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }

        }
        // =========================
        // DELETE SCAN
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _scanService.DeleteScan(id);

                return Ok(new
                {
                    success = true,
                    message = "Scan deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        // =========================
        // HELPER
        // =========================
        private int? GetUserId()
        {
            var claim = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(claim))
                return null;

            return int.Parse(claim);
        }
    }
}
    
