<template>
  <div class="edit-phongban">
    <v-container>
      <h2>{{ editMode ? capnhat : themmoi }}</h2>
      <v-form ref="form" @submit.prevent="submitForm">
        <v-row>
          <v-col cols="12">
            <v-text-field
              v-model.trim="phongban.ten_PB"
              label="Tên phòng ban"
              :rules="[(v) => !!v || 'Tên phòng ban không được để trống']"
              required
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12">
            <v-text-field
              v-model="phongban.viTri_PB"
              label="Vị trí phòng ban"
              :rules="[(v) => !!v || 'Vị trí phòng ban không được để trống']"
              required
            ></v-text-field>
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
import { ref, reactive, onMounted } from "vue";
import PhongBanApi from "../../api/PhongBanService";
import PhongBan from "../../models/PhongBan";

export default {
  name: "EditPhongBan",
  props: {
    phongbanId: {
      type: Number,
      default: null,
    },
  },
  setup(props, { emit }) {
    // Reactive References
    const phongban = reactive({
      id_PB: 0,
      ten_PB: "",
      viTri_PB: "",
    });

    const editMode = ref(false);
    const capnhat = "Cập nhật";
    const themmoi = "Thêm mới";

    // Template Refs
    const form = ref(null);

    // Lifecycle Functions
    onMounted(async () => {
      if (props.phongbanId) await fetchPhongBanDetails(props.phongbanId);
    });

    // Methods
    const fetchPhongBanDetails = async (id_PB) => {
      try {
        const response = await PhongBanApi.getPhongBanID(id_PB);
        Object.assign(phongban, response.data);
        editMode.value = true;
      } catch (error) {
        console.error("Error fetching phong ban details:", error);
      }
    };

    const submitForm = async () => {
      const isValid = await form.value.validate();
      if (!isValid) return;
      try {
        let response;
        const data = new PhongBan(phongban);
        if (editMode.value) {
          response = await PhongBanApi.updatePhongBan(data);
          emit("successMessageEdit", "Sửa phòng ban thành công");
        } else {
          response = await PhongBanApi.createPhongBan(data);
          emit("successMessageEdit", "Thêm phòng ban thành công");
        }
        console.log("phong ban saved:", response.data);
        emit("close");
      } catch (error) {
        console.error("Error saving phong ban:", error);
        editMode.value
          ? emit("errorMessageEdit", "Sửa phòng ban thất bại")
          : emit("errorMessageEdit", "Thêm phòng ban thất bại");
      }
    };

    const closeForm = () => {
      emit("close");
    };

    return {
      phongban,
      editMode,
      capnhat,
      themmoi,
      form,
      submitForm,
      closeForm,
    };
  },
};
</script>

<style scoped>
.edit-phongban {
  background-color: white;
  border-radius: 10px;
}
</style>
