using Demo_API_QL_NS.Classes;
using Demo_API_QL_NS.Models;
using Demo_API_QL_NS.Services.ChucVu;
using Demo_API_QL_NS.Services.NhanVien;
using Demo_API_QL_NS.Services.PhongBan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo_API_QL_NS.Controllers
{
    [Route("api/phongban")]
    [ApiController]
    public class PhongBanController : ControllerBase
    {
        private readonly IPhongBanService _service;
        private readonly INhanVienService _servicenv;
        private readonly IChucVuService _servicecv;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;


        public PhongBanController(IPhongBanService PhongBanService, INhanVienService NhanVienService, IChucVuService ChucVuService, IConfiguration configuration)
        {
            _service = PhongBanService;
            _servicenv = NhanVienService;
            _servicecv = ChucVuService;
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }
        [HttpGet("getAllPhongBan")]
        public async Task<object> getAllPhongBan()
        {
            try
            {

                var results = await _service.getAllPhongBan();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("CreatePhongBan")]
        public async Task<object> CreatePhongBan(PhongBanDTO model)
        {
            try
            {

                var result = await _service.CreatePhongBan(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("UpdatePhongBan")]
        public async Task<object> UpdatePhongBan(PhongBanDTO model)
        {
            try
            {

                var result = await _service.UpdatePhongBan(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeletePhongBan")]
        public async Task<object> DeletePhongBan(int id)
        {
            try
            {

                var hasNhanVien = await _servicenv.CheckId_PB_NhanVien(id);
                var hasChucVu = await _servicecv.CheckId_PB_ChucVu(id);
                if (hasNhanVien > 0 || hasChucVu>0)
                {
                    return BadRequest("Không thể xóa phòng ban đang dùng");
                }
                else
                {
                    var result = await _service.DeletePhongBan(id);
                    return Ok(result);
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("SearchTenPhongBan")]
        public async Task<object> SearchTenPhongBan(string Ten_PB)
        {
            try
            {

                var results = await _service.SearchTenPhongBan(Ten_PB);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getPhongBanID")]
        public async Task<object> getPhongBanID(int Id_PB)
        {
            try
            {

                var results = await _service.getPhongBanID(Id_PB);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
