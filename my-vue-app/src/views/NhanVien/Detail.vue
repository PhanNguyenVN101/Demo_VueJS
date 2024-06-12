<template>
  <div class="detail-nhanvien">
    <v-container>
      <h2>{{ title }}</h2>
      <v-form ref="form">
        <v-row>
          <v-col cols="6">
            <v-text-field
              v-model="nhanvien.hoTen_NV"
              label="Họ tên"
              readonly
            ></v-text-field>
          </v-col>
          <v-col cols="6">
            <v-text-field
              v-model="nhanvien.email_NV"
              label="Email"
              readonly
            ></v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="6">
            <v-select
              v-model="nhanvien.gioiTinh_NV"
              :items="gioiTinhOptions"
              label="Giới tính"
              readonly
            ></v-select>
          </v-col>
          <v-col cols="6">
            <v-text-field
              v-model="nhanvien.ngaySinh_NV"
              label="Ngày sinh"
              v-bind="attrs"
              v-on="on"
              type="date"
              readonly
            >
            </v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12">
            <v-text-field
              v-model="nhanvien.diaChi_NV"
              label="Địa chỉ"
              readonly
            ></v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="6">
            <v-select
              v-model="nhanvien.id_PB"
              :items="phongBanOptions"
              label="Phòng ban"
              item-value="id_PB"
              item-title="ten_PB"
              readonly
            ></v-select>
          </v-col>
          <v-col cols="6">
            <v-select
              v-model="nhanvien.id_CV"
              :items="chucVuOptions"
              label="Chức vụ"
              item-value="id_CV"
              item-title="ten_CV"
              readonly
            ></v-select>
          </v-col>
        </v-row>
        <v-row class="justify-end">
          <v-btn color="secondary" @click="closeForm" class="ml-2">Đóng</v-btn>
        </v-row>
      </v-form>
    </v-container>
  </div>
</template>

<script>
import NhanVienApi from "../../api/NhanVienService";
import PhongBanApi from "../../api/PhongBanService";
export default {
  name: "DetailNhanVien",
  props: {
    nhanvienId: {
      type: Number,
      default: null,
    },
  },
  data() {
    return {
      nhanvien: {
        id_NV: null,
        hoTen_NV: "",
        email_NV: "",
        gioiTinh_NV: null,
        ngaySinh_NV: new Date().toISOString().substr(0, 10),
        diaChi_NV: "",
        id_PB: null,
        id_CV: null,
      },
      gioiTinhOptions: ["Nam", "Nữ"],
      phongBanOptions: [],
      chucVuOptions: [],
      title: "Chi tiết nhân viên",
    };
  },

  watch: {
    "nhanvien.id_PB": {
      handler(newVal, oldVal) {
        if (newVal !== oldVal) {
          this.updateChucVu();
        }
      },
      immediate: true,
    },
  },
  methods: {
    async fetchNhanVienDetails(id_NV) {
      try {
        const response = await NhanVienApi.getNhanVienID(id_NV);
        this.nhanvien = response.data;
        console.log(id_NV);
        console.log(this.nhanvien);
        if (this.nhanvien.id_PB) {
          await this.updateChucVu();
        }
        this.nhanvien.ngaySinh_NV = this.parseDate(response.data.ngaySinh_NV);
        this.editMode = true;
      } catch (error) {
        console.error("Error fetching nhan vien details:", error);
      }
    },
    parseDate(dateString) {
      const [day, month, year] = dateString.split("/");
      return `${year}-${month}-${day}`;
    },
    formatDate(dateString) {
      const parts = dateString.split("/");
      // parts[2] là năm, parts[1] là tháng, parts[0] là ngày
      return `${parts[2]}-${parts[1]}-${parts[0]}`;
    },
    formatDateToString(dateString) {
      const parts = dateString.split("-");
      // parts[2] là năm, parts[1] là tháng, parts[0] là ngày
      return `${parts[2]}/${parts[1]}/${parts[0]}`;
    },

    dateFormatter(date) {
      const [year, month, day] = date.split("-");
      return `${day}/${month}/${year}`;
    },
    async fetchPhongBanOptions() {
      try {
        const response = await PhongBanApi.getAllPhongBan();
        this.phongBanOptions = response.data.map((item) => ({
          id_PB: item.id_PB,
          ten_PB: item.ten_PB,
        }));
        console.log(this.phongBanOptions);
      } catch (error) {
        console.error("Error fetching phong ban:", error);
      }
    },
    async fetchChucVuOptions(id_PB) {
      try {
        const response = await NhanVienApi.getAllChucVu_IdPB(id_PB);
        this.chucVuOptions = response.data.map((item) => ({
          id_CV: item.id_CV,
          ten_CV: item.ten_CV,
        }));
        console.log(this.chucVuOptions);
      } catch (error) {
        console.error("Error fetching chuc vu:", error);
      }
    },

    closeForm() {
      this.$emit("close");
    },
    async updateChucVu() {
      if (this.nhanvien.id_PB) {
        await this.fetchChucVuOptions(this.nhanvien.id_PB);
        if (
          !this.chucVuOptions.some((cv) => cv.id_CV === this.nhanvien.id_CV)
        ) {
          this.nhanvien.id_CV = null;
        }
      } else {
        this.nhanvien.id_CV = null;
        this.chucVuOptions = [];
      }
    },
  },
  async mounted() {
    if (this.nhanvienId) await this.fetchNhanVienDetails(this.nhanvienId);
    await this.fetchPhongBanOptions();
  },
};
</script>

<style scoped>
.detail-nhanvien {
  background-color: white;
  border-radius: 10px;
}
</style>
