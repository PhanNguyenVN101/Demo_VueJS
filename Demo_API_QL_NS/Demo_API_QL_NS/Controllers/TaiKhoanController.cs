using Demo_API_QL_NS.Models;
using Demo_API_QL_NS.Services.ChucVu;
using Demo_API_QL_NS.Services.NhanVien;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using Demo_API_QL_NS.Services.TaiKhoan;

namespace Demo_API_QL_NS.Controllers
{
    [Route("api/taikhoan")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly ITaiKhoanService _service;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public TaiKhoanController(ITaiKhoanService TaiKhoanService, IConfiguration configuration)
        {
            _service = TaiKhoanService;
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        [HttpGet("getAllTaiKhoan")]
        public async Task<object> getAllTaiKhoan()
        {
            try
            {
                var results = await _service.getAllTaiKhoan();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("CreateTaiKhoan")]
        public async Task<IActionResult> CreateTaiKhoan(TaiKhoanDTO model)
        {
            try
            {
                var result = await _service.CreateTaiKhoan(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("CheckId_NV_TaiKhoan")]
        public async Task<IActionResult> CheckId_NV_TaiKhoan(int Id_NV)
        {
            try
            {
                var result = await _service.CheckId_NV_TaiKhoan(Id_NV);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpPut("UpdateTaiKhoan")]
        public async Task<IActionResult> UpdateTaiKhoan(TaiKhoanDTO model)
        {
            try
            {
                var result = await _service.UpdateTaiKhoan(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteTaiKhoan")]
        public async Task<IActionResult> DeleteTaiKhoan(TaiKhoanDTO model)
        {
            try
            {

                var result = await _service.DeleteTaiKhoan(model);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getIdTaiKhoan")]
        public async Task<object> getIdTaiKhoan(int Id_TK)
        {
            try
            {
                var result = await _service.getIdTaiKhoan(Id_TK);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
    }
}
