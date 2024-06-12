import axiosInstance from "./axios";

const BASE_API = "api/nhanvien/";

const handleError = (error) => {
  console.error("API call error:", error);
  throw error;
};

const NhanVienApi = {
  getAllNhanVien() {
    return axiosInstance.get(`${BASE_API}getAllNhanVien`).catch(handleError);
  },
  getNhanVienID(id) {
    return axiosInstance
      .get(`${BASE_API}getNhanVienID?Id_NV=${id}`)
      .catch(handleError);
  },
  getAllChucVu_IdPB(id) {
    return axiosInstance
      .get(`${BASE_API}getAllChucVu_IdPB?Id_PB=${id}`)
      .catch(handleError);
  },
  createNhanVien(data) {
    return axiosInstance
      .post(`${BASE_API}CreateNhanVien`, data)
      .catch(handleError);
  },
  updateNhanVien(data) {
    return axiosInstance
      .put(`${BASE_API}UpdateNhanVien`, data)
      .catch(handleError);
  },
  deleteNhanVien(id) {
    return axiosInstance
      .delete(`${BASE_API}DeleteNhanVien?id=${id}`)
      .catch(handleError);
  },
};

export default NhanVienApi;
