using System;
using System.Security.Policy;

namespace Demo_API_QL_NS.Models
{
    public class NhanVienDTO
    {
        public int Id_NV { get; set; }
        public string? HoTen_NV { get;set; }
        public string? GioiTinh_NV { get; set; }
        public string? DiaChi_NV { get; set; }
        public string? Email_NV { get; set; }
        public string? NgaySinh_NV { get; set; }
        public string? DeletedDate { get; set; }
        public string? CreatedDate { get;set; }
        public int? Id_PB { get;set; }
        public string? Ten_PB { get; set; }
        public int? Id_CV { get; set; }
        public string? Ten_CV { get; set; }
    }
}
