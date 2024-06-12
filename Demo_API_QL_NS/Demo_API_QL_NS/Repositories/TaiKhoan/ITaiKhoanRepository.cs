using Demo_API_QL_NS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo_API_QL_NS.Repositories.TaiKhoan
{
    public interface ITaiKhoanRepository
    {
        Task<IEnumerable<TaiKhoanDTO>> getAllTaiKhoan();
        Task<bool> CreateTaiKhoan(TaiKhoanDTO model);
        Task<bool> UpdateTaiKhoan(TaiKhoanDTO model);
        Task<bool> DeleteTaiKhoan(TaiKhoanDTO model);
        Task<TaiKhoanDTO> getIdTaiKhoan(int Id_TK);
        Task<int> CheckId_NV_TaiKhoan(int Id_NV);
        Task<int> CheckId_NQ_TaiKhoan(int Id_NQ);
    }
}
