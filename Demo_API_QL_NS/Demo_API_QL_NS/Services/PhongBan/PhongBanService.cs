using Demo_API_QL_NS.Models;
using Demo_API_QL_NS.Repositories.PhongBan;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace Demo_API_QL_NS.Services.PhongBan
{
    public class PhongBanService:IPhongBanService
    {
        private IPhongBanRepository _reposiory;

        public PhongBanService(IPhongBanRepository PhongBanRepository)
        {
            _reposiory = PhongBanRepository;
        }
        public async Task<IEnumerable<PhongBanDTO>> getAllPhongBan()
        {
           return await _reposiory.getAllPhongBan();

        }
        
        public async Task<bool> CreatePhongBan(PhongBanDTO model)
        {
            return await _reposiory.CreatePhongBan(model);
        }
        public async Task<bool> UpdatePhongBan(PhongBanDTO model)
        {
            return await _reposiory.UpdatePhongBan(model);
        }
        public async Task<bool> DeletePhongBan(int id)
        {
            return await _reposiory.DeletePhongBan(id);
        }
        public async Task<IEnumerable<PhongBanDTO>> SearchTenPhongBan(string Ten_PB)
        {
            return await _reposiory.SearchTenPhongBan(Ten_PB);
        }
        public async Task<PhongBanDTO> getPhongBanID(int Id_PB)
        {
            return await _reposiory.getPhongBanID(Id_PB);
        }
    }
}
