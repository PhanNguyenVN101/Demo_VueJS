<template>
  <div class="edit-nhanvien">
    <v-container>
      <h2>{{ editMode ? capnhat : themmoi }}</h2>
      <v-form ref="form" @submit.prevent="submitForm">
        <v-row>
          <v-col cols="6">
            <v-text-field
              v-model.trim="nhanvien.hoTen_NV"
              label="Họ tên"
              :rules="[(v) => !!v || 'Họ tên không được để trống']"
              required
            ></v-text-field>
          </v-col>
          <v-col cols="6">
            <v-text-field
              v-model="nhanvien.email_NV"
              label="Email"
              :rules="[
                (v) => !!v || 'Email không được để trống',
                (v) => /.+@.+\..+/.test(v) || 'Email không hợp lệ',
              ]"
              required
            ></v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="6">
            <v-select
              v-model="nhanvien.gioiTinh_NV"
              :items="gioiTinhOptions"
              label="Giới tính"
              :rules="[(v) => !!v || 'Giới tính không được để trống']"
              required
            ></v-select>
          </v-col>
          <v-col cols="6">
            <v-text-field
              v-model="nhanvien.ngaySinh_NV"
              label="Ngày sinh"
              v-bind="attrs"
              v-on="on"
              :rules="[
                (v) =>
                  validateAge(parseDate(v)) ||
                  'Nhân viên phải từ 18 tuổi trở lên',
              ]"
              type="date"
              required
            >
            </v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12">
            <v-text-field
              v-model="nhanvien.diaChi_NV"
              label="Địa chỉ"
              :rules="[(v) => !!v || 'Địa chỉ không được để trống']"
              required
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
              :rules="[(v) => !!v || 'Phòng ban không được để trống']"
              required
            ></v-select>
          </v-col>
          <v-col cols="6">
            <v-select
              v-model="nhanvien.id_CV"
              :items="chucVuOptions"
              label="Chức vụ"
              item-value="id_CV"
              item-title="ten_CV"
              :rules="[(v) => !!v || 'Chức vụ không được để trống']"
              required
            ></v-select>
          </v-col>
        </v-row>
        <v-row class="justify-end">
          <v-btn color="primary" type="submit">
            {{ editMode ? capnhat : themmoi }}
          </v-btn>
          <v-btn color="secondary" @click="closeForm" class="ml-2">Đóng</v-btn>
        </v-row>
      </v-form>
    </v-container>
  </div>
</template>

<script>
import NhanVienApi from "../../api/NhanVienService";
import PhongBanApi from "../../api/PhongBanService";
import NhanVien from "../../models/NhanVien";
export default {
  name: "EditNhanVien",
  props: {
    nhanvienId: {
      type: Number,
      default: null,
    },
  },
  data() {
    return {
      nhanvien: {
        id_NV: 0,
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
      editMode: false,
      capnhat: "Cập nhật",
      themmoi: "Thêm mới",
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

    validateAge(value) {
      if (!value) return false;
      const today = new Date();
      const birthDate = new Date(value);
      let age = today.getFullYear() - birthDate.getFullYear();
      const m = today.getMonth() - birthDate.getMonth();
      if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age--;
      }
      return age >= 18;
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
    async submitForm() {
      const isValid = await this.$refs.form.validate();
      if (!isValid) return;
      if (!this.$refs.form.errors.length) {
        try {
          let response;

          const data = new NhanVien(this.nhanvien);
          console.log(data);
          if (this.editMode) {
            response = await NhanVienApi.updateNhanVien(data);
            this.$emit("successMessageEdit", "Sửa nhân viên thành công");
          } else {
            response = await NhanVienApi.createNhanVien(data);
            this.$emit("successMessageEdit", "Thêm nhân viên thành công");
          }
          console.log("Nhan vien saved:", response.data);
          this.$emit("close");
        } catch (error) {
          console.error("Error saving nhan vien:", error);
          this.editMode
            ? this.$emit("errorMessageEdit", "Sửa nhân viên thất bại")
            : this.$emit("errorMessageEdit", "Thêm nhân viên thất bại");
        }
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
.edit-nhanvien {
  background-color: white;
  border-radius: 10px;
}
</style>
