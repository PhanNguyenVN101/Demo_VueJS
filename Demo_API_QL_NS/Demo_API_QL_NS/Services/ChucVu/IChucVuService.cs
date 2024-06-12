using Demo_API_QL_NS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo_API_QL_NS.Services.ChucVu
{
    public interface IChucVuService
    {
        Task<IEnumerable<ChucVuDTO>> getAllChucVu();
        Task<bool> CreateChucVu(ChucVuDTO model);
        Task<bool> UpdateChucVu(ChucVuDTO model);
        Task<bool> DeleteChucVu(int id);
        Task<IEnumerable<ChucVuDTO>> SearchTenChucVu(string Ten_CV);
        Task<ChucVuDTO> getChucVuID(int Id_CV);
        Task<int> CheckId_PB_ChucVu(int Id_PB);
        Task<IEnumerable<ChucVuDTO>> getAllChucVu_IdPB(int Id_PB);
    }
}
