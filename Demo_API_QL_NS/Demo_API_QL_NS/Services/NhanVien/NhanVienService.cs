using Demo_API_QL_NS.Models;
using Demo_API_QL_NS.Repositories.NhanVien;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Collections;

namespace Demo_API_QL_NS.Services.NhanVien
{
    public class NhanVienService : INhanVienService
    {
        private INhanVienRepository _reposiory;

        public NhanVienService(INhanVienRepository NhanVienRepository)
        {
            _reposiory = NhanVienRepository;
        }
        public async Task<IEnumerable<NhanVienDTO>> getAllNhanVien()
        {
            return await _reposiory.getAllNhanVien();

        }
        
        public async Task<bool> CreateNhanVien(NhanVienDTO model)
        {
            return await _reposiory.CreateNhanVien(model);
        }
        public async Task<bool> UpdateNhanVien(NhanVienDTO model)
        {
            return await _reposiory.UpdateNhanVien(model) ;
        }
        public async Task<bool> DeleteNhanVien(int id)
        {
            return await _reposiory.DeleteNhanVien(id);
        }
        public async Task<bool> DeleteNhanViens(List<int> ids)
        {
            return await _reposiory.DeleteNhanViens(ids);
        }
        public async Task<int> CheckId_PB_NhanVien(int Id_PB)
        {
            return await _reposiory.CheckId_PB_NhanVien(Id_PB);
        }
        public async Task<int> CheckId_CV_NhanVien(int Id_CV)
        {
            return await _reposiory.CheckId_CV_NhanVien(Id_CV);
        }
        public async Task<IEnumerable<NhanVienDTO>> SearchNhanVienHT(string HoTen)
        {
            return await _reposiory.SearchNhanVienHT(HoTen);
        }
        public async Task<NhanVienDTO> getNhanVienID(int Id_NV)
        {
            return await _reposiory.getNhanVienID(Id_NV);
        }
    }
}
