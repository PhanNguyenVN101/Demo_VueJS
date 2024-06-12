using Demo_API_QL_NS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo_API_QL_NS.Services.PhongBan
{
    public interface IPhongBanService
    {
        Task<IEnumerable<PhongBanDTO>> getAllPhongBan();
        Task<bool> CreatePhongBan(PhongBanDTO model);
        Task<bool> UpdatePhongBan(PhongBanDTO model);
        Task<bool> DeletePhongBan(int id);
        Task<IEnumerable<PhongBanDTO>> SearchTenPhongBan(string Ten_PB);
        Task<PhongBanDTO> getPhongBanID(int Id_PB);
    }
}
