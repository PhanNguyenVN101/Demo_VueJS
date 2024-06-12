import axiosInstance from "./axios";
const BASE_API = "api/chucvu/";
const handleError = (error) => {
  console.error("API call error:", error);
  throw error;
};
export default {
  getAllChucVu() {
    return axiosInstance.get(`${BASE_API}getAllChucVu`).catch(handleError);
  },

  getChucVuID(id) {
    return axiosInstance
      .get(`${BASE_API}getChucVuID?Id_CV=${id}`)
      .catch(handleError);
  },

  createChucVu(data) {
    return axiosInstance
      .post(`${BASE_API}CreateChucVu`, data)
      .catch(handleError);
  },

  updateChucVu(data) {
    return axiosInstance
      .put(`${BASE_API}UpdateChucVu`, data)
      .catch(handleError);
  },

  deleteChucVu(id) {
    return axiosInstance
      .delete(`${BASE_API}DeleteChucVu?id=${id}`)
      .catch(handleError);
  },
};
