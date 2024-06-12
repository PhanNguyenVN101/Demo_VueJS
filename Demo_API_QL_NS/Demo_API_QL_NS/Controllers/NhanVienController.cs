using Demo_API_QL_NS.Models;
using Demo_API_QL_NS.Services.NhanVien;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Reflection;
using Demo_API_QL_NS.Services.PhongBan;
using Demo_API_QL_NS.Services.ChucVu;
using Demo_API_QL_NS.Classes;
using Demo_API_QL_NS.Services.TaiKhoan;

namespace Demo_API_QL_NS.Controllers
{
    [Route("api/nhanvien")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienService _service;
        private readonly IPhongBanService _servicePB;
        private readonly IChucVuService _serviceCV;
        private readonly ITaiKhoanService _serviceTK;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public NhanVienController(INhanVienService nhanvienService, IPhongBanService phongbanService, IChucVuService chucvuService, ITaiKhoanService taikhoanService, IConfiguration configuration)
        {
            _service = nhanvienService;
            _servicePB = phongbanService;
            _serviceCV = chucvuService;
            _serviceTK = taikhoanService;
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        [HttpGet("getAllNhanVien")]
        public async Task<object> getAllNhanVien()
        {
            try
            {
                var results = await _service.getAllNhanVien();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        
        [HttpGet("getAllChucVu_IdPB")]
        public async Task<object> getAllChucVu_IdPB(int Id_PB)
        {
            try
            {
                var results = await _serviceCV.getAllChucVu_IdPB(Id_PB);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("CheckId_PB_NhanVien")]
        public async Task<object> CheckId_PB_NhanVien(int Id_PB)
        {
            try
            {
                var result = await _service.CheckId_PB_NhanVien(Id_PB);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("CheckId_CV_NhanVien")]
        public async Task<object> CheckId_CV_NhanVien(int Id_CV)
        {
            try
            {
                var result = await _service.CheckId_CV_NhanVien(Id_CV);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("CreateNhanVien")]
        public async Task<object> CreateNhanVien(NhanVienDTO model)
        {
            try
            {

                var result = await _service.CreateNhanVien(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("UpdateNhanVien")]
        public async Task<object> UpdateNhanVien(NhanVienDTO model)
        {
            try
            {

                var result = await _service.UpdateNhanVien(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteNhanVien")]
        public async Task<object> DeleteNhanVien(int id)
        {
            try
            {

                var result = await _service.DeleteNhanVien(id);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteNhanViens")]
        public async Task<object> DeleteNhanViens(List<int> ids)
        {
            try
            {
                int dem = 0;
                List<int> listerror = new List<int>();
                foreach(var i in ids)
                {
                    var hastaikhoan = await _serviceTK.CheckId_NV_TaiKhoan(i);
                    if (hastaikhoan > 0)
                    {      
                        listerror.Add(i);
                        dem++;
                    }
                    
                }
                if(dem==0)
                {
                    var result = await _service.DeleteNhanViens(ids);
                    return Ok(result);
                }
                string errorMessage = string.Join(", ", listerror);
                return BadRequest($"Không thể xóa nhân viên có mã {errorMessage} vì tài khoản còn hoạt động");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("SearchNhanVien")]
        public async Task<object> SearchNhanVien(string hoTen)
        {
            try
            {

                var results = await _service.SearchNhanVienHT(hoTen);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getNhanVienID")]
        public async Task<object> getNhanVienID(int Id_NV)
        {
            try
            {

                var results = await _service.getNhanVienID(Id_NV);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
