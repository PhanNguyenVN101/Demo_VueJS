using Demo_API_QL_NS.Models;
using Demo_API_QL_NS.Repositories.PhongBan;
using Demo_API_QL_NS.Repositories.TaiKhoan;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;

namespace Demo_API_QL_NS.Services.TaiKhoan
{
    public class TaiKhoanService:ITaiKhoanService
    {
        private ITaiKhoanRepository _reposiory;

        public TaiKhoanService(ITaiKhoanRepository TaiKhoanRepository)
        {
            _reposiory = TaiKhoanRepository;
        }
        public async Task<IEnumerable<TaiKhoanDTO>> getAllTaiKhoan()
        {
            return await _reposiory.getAllTaiKhoan();

        }
        
        public async Task<bool> CreateTaiKhoan(TaiKhoanDTO model)
        {
            return await _reposiory.CreateTaiKhoan(model);
        }
        public async Task<bool> UpdateTaiKhoan(TaiKhoanDTO model)
        {
            return await _reposiory.UpdateTaiKhoan(model) ;
        }
        public async Task<bool> DeleteTaiKhoan(TaiKhoanDTO model)
        {
            return await _reposiory.DeleteTaiKhoan(model );
        }

        public async Task<TaiKhoanDTO> getIdTaiKhoan(int Id_TK)
        {
            return await _reposiory.getIdTaiKhoan(Id_TK);
        }
        public async Task<int> CheckId_NQ_TaiKhoan(int Id_NQ)
        {
            return await _reposiory.CheckId_NQ_TaiKhoan(Id_NQ);
        }
        public async Task<int> CheckId_NV_TaiKhoan(int Id_NV)
        {
            return await _reposiory.CheckId_NV_TaiKhoan(Id_NV);
        }
    }
}
