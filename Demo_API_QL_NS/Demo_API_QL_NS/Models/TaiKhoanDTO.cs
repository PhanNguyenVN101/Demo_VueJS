using System;

namespace Demo_API_QL_NS.Models
{
    public class TaiKhoanDTO
    {
        public string Ten_TK { get;set; }
        public string MK_TK { get; set; }
        public string? DeletedDate { get; set; }
        public string? CreatedDate { get; set; }
        public int? Id_NV { get; set; }
        public string? HoTen_NV { get; set; }
        public int? Id_NQ { get; set; }
        public string? Ten_NQ { get; set; }
    }
}
