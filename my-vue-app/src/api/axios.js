import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "https://localhost:44395/",
  headers: {
    "Access-Control-Allow-Origin": "*",
  },
});

export default axiosInstance;
