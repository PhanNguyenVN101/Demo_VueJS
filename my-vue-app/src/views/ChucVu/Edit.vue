<template>
  <div class="edit-chucvu">
    <v-container>
      <h2>{{ editMode ? capnhat : themmoi }}</h2>
      <v-form ref="form" @submit.prevent="submitForm">
        <v-row>
          <v-col cols="12">
            <v-text-field
              v-model.trim="chucvu.ten_CV"
              label="Tên chức vụ"
              :rules="[(v) => !!v || 'Tên chức vụ không được để trống']"
              required
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12">
            <v-select
              v-model="chucvu.id_PB"
              :items="phongBanOptions"
              label="Phòng ban"
              item-value="id_PB"
              item-title="ten_PB"
              :rules="[(v) => !!v || 'Phòng ban không được để trống']"
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
import ChucVuApi from "../../api/ChucVuService";
import ChucVu from "../../models/ChucVu";
import PhongBanApi from "../../api/PhongBanService";
export default {
  name: "EditChucVu",
  props: {
    chucvuId: {
      type: Number,
      default: null,
    },
  },
  data() {
    return {
      chucvu: {
        id_CV: 0,
        ten_CV: "",
        id_PB: null,
      },
      phongBanOptions: [],
      editMode: false,
      capnhat: "Cập nhật",
      themmoi: "Thêm mới",
    };
  },

  methods: {
    async fetchChucVuDetails(id_CV) {
      try {
        const response = await ChucVuApi.getChucVuID(id_CV);
        this.chucvu = response.data;
        console.log(id_CV);
        console.log(this.chucvu);

        this.editMode = true;
      } catch (error) {
        console.error("Error fetching chuc vu details:", error);
      }
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
    async submitForm() {
      const isValid = await this.$refs.form.validate();
      if (!isValid) return;
      if (!this.$refs.form.errors.length) {
        try {
          let response;

          const data = new ChucVu(this.chucvu);
          console.log(data);
          if (this.editMode) {
            response = await ChucVuApi.updateChucVu(data);

            this.$emit("successMessageEdit", "Sửa chức vụ thành công");
          } else {
            response = await ChucVuApi.createChucVu(data);
            this.$emit("successMessageEdit", "Thêm chức vụ thành công");
          }
          console.log("Chuc vu saved:", response.data);
          this.$emit("close");
        } catch (error) {
          console.error("Error saving chuc vu:", error);
          this.editMode
            ? this.$emit("errorMessageEdit", "Sửa chức vụ thất bại")
            : this.$emit("errorMessageEdit", "Thêm chức vụ thất bại");
        }
      }
    },
    closeForm() {
      this.$emit("close");
    },
  },
  async mounted() {
    if (this.chucvuId) await this.fetchChucVuDetails(this.chucvuId);
    await this.fetchPhongBanOptions();
  },
};
</script>

<style scoped>
.edit-chucvu {
  background-color: white;
  border-radius: 10px;
}
</style>
