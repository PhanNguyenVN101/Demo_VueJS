export default class NhanVien {
  constructor(data) {
    this.Id_NV = data.id_NV;
    this.HoTen_NV = data.hoTen_NV;
    this.GioiTinh_NV = data.gioiTinh_NV;
    this.DiaChi_NV = data.diaChi_NV;
    this.Email_NV = data.email_NV;
    this.NgaySinh_NV = this.formatDate(data.ngaySinh_NV);
    this.Id_PB = data.id_PB;
    this.Id_CV = data.id_CV;
  }
  formatDate(dateString) {
    const [year, month, day] = dateString.split("-");
    return `${day}/${month}/${year}`;
  }
}
