using Demo_API_QL_NS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo_API_QL_NS.Services.NhanVien
{
    public interface INhanVienService
    {
        Task<IEnumerable<NhanVienDTO>> getAllNhanVien();
        Task<bool> CreateNhanVien(NhanVienDTO model);
        Task<bool> UpdateNhanVien(NhanVienDTO model);
        Task<bool> DeleteNhanVien(int id);
        Task<bool> DeleteNhanViens(List<int> ids);
        Task<IEnumerable<NhanVienDTO>> SearchNhanVienHT(string HoTen);
        Task<int> CheckId_PB_NhanVien(int Id_PB);
        Task<int> CheckId_CV_NhanVien(int Id_CV);
        Task<NhanVienDTO> getNhanVienID(int Id_NV);
    }
}
