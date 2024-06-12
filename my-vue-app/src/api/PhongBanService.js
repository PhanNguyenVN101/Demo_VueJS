import axiosInstance from "./axios";
const BASE_API = "api/phongban/";
const handleError = (error) => {
  console.error("API call error:", error);
  throw error;
};
export default {
  getAllPhongBan() {
    return axiosInstance.get(`${BASE_API}getAllPhongBan`).catch(handleError);
  },

  getPhongBanID(id) {
    return axiosInstance
      .get(`${BASE_API}getPhongBanID?Id_PB=${id}`)
      .catch(handleError);
  },

  createPhongBan(data) {
    return axiosInstance
      .post(`${BASE_API}CreatePhongBan`, data)
      .catch(handleError);
  },

  updatePhongBan(data) {
    return axiosInstance
      .put(`${BASE_API}UpdatePhongBan`, data)
      .catch(handleError);
  },

  deletePhongBan(id) {
    return axiosInstance
      .delete(`${BASE_API}DeletePhongBan?id=${id}`)
      .catch(handleError);
  },
};
