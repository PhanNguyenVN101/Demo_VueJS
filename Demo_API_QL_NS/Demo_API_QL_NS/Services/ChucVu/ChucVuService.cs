using Demo_API_QL_NS.Models;
using Demo_API_QL_NS.Repositories.ChucVu;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;

namespace Demo_API_QL_NS.Services.ChucVu
{
    public class ChucVuService:IChucVuService
    {
        private IChucVuRepository _reposiory;

        public ChucVuService(IChucVuRepository ChucVuRepository)
        {
            _reposiory = ChucVuRepository;
        }
        public async Task<IEnumerable<ChucVuDTO>> getAllChucVu()
        {
            return await _reposiory.getAllChucVu();

        }
        public async Task<bool> CreateChucVu(ChucVuDTO model)
        {
            return await _reposiory.CreateChucVu(model);
        }
        public async Task<bool> UpdateChucVu(ChucVuDTO model)
        {
            return await _reposiory.UpdateChucVu(model);
        }
        public async Task<bool> DeleteChucVu(int id)
        {
            return await _reposiory.DeleteChucVu(id);
        }
        public async Task<IEnumerable<ChucVuDTO>> SearchTenChucVu(string Ten_CV)
        {
            return await _reposiory.SearchTenChucVu(Ten_CV);
        }
        public async Task<ChucVuDTO> getChucVuID(int Id_CV)
        {
            return await _reposiory.getChucVuID(Id_CV);
        }
        public async Task<int> CheckId_PB_ChucVu(int Id_PB)
        {
            return await _reposiory.CheckId_PB_ChucVu(Id_PB);
        }
        public async Task<IEnumerable<ChucVuDTO>> getAllChucVu_IdPB(int Id_PB)
        {
            return await _reposiory.getAllChucVu_IdPB(Id_PB);
        }
    }
}
