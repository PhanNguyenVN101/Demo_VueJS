using Demo_API_QL_NS.Models;
using Demo_API_QL_NS.Services.NhanVien;
using Demo_API_QL_NS.Services.ChucVu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo_API_QL_NS.Classes;

namespace Demo_API_QL_NS.Controllers
{
    [Route("api/chucvu")]
    [ApiController]
    public class ChucVuController : ControllerBase
    {
        private readonly IChucVuService _service;
        private readonly INhanVienService _servicenv;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ChucVuController(IChucVuService ChucVuService, INhanVienService NhanVienService, IConfiguration configuration)
        {
            _service = ChucVuService;
            _servicenv = NhanVienService;
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }
        [HttpGet("getAllChucVu")]
        public async Task<object> getAllChucVu()
        {
            try
            {


                var results = await _service.getAllChucVu();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("CreateChucVu")]
        public async Task<object> CreateChucVu(ChucVuDTO model)
        {
            try
            {

                var result = await _service.CreateChucVu(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("CheckId_PB_ChucVu")]
        public async Task<object> CheckId_PB_ChucVu(int Id_PB)
        {
            try
            {

                var result = await _service.CheckId_PB_ChucVu(Id_PB);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("UpdateChucVu")]
        public async Task<object> UpdateChucVu(ChucVuDTO model)
        {
            try
            {

                var result = await _service.UpdateChucVu(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteChucVu")]
        public async Task<object> DeleteChucVu(int id)
        {
            try
            {

                var hasNhanVien = await _servicenv.CheckId_CV_NhanVien(id);
                if (hasNhanVien > 0)
                {
                    return BadRequest("Không thể xóa chức vụ đang dùng");
                }
                else
                {
                    var result = await _service.DeleteChucVu(id);
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("SearchTenChucVu")]
        public async Task<object> SearchTenChucVu(string Ten_CV)
        {
            try
            {

                var results = await _service.SearchTenChucVu(Ten_CV);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getChucVuID")]
        public async Task<object> getChucVuID(int Id_CV)
        {
            try
            {

                var results = await _service.getChucVuID(Id_CV);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getAllChucVu_IdPB")]
        public async Task<object> getAllChucVu_IdPB(int IdPB)
        {
            try
            {

                var results = await _service.getAllChucVu_IdPB(IdPB);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
