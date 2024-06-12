<template>
  <div class="table-container">
    <div class="header-container">
      <div class="left-section">
        <h2>{{ title }}</h2>
      </div>
      <div class="right-section">
        <v-btn color="primary" @click="openDialogEdit(null)">Thêm mới</v-btn>
      </div>
    </div>

    <div class="table-wrapper">
      <table class="styled-table">
        <thead>
          <tr>
            <th>STT</th>
            <th>Mã</th>
            <th>Tên chức vụ</th>
            <th>Phòng ban</th>
            <th>Thao tác</th>
          </tr>
        </thead>

        <tbody>
          <tr v-for="(chucvu, index) in displayedChucVus" :key="chucvu.id_CV">
            <td>{{ startIndex + index }}</td>
            <td>{{ chucvu.id_CV }}</td>
            <td>{{ chucvu.ten_CV }}</td>
            <td>{{ chucvu.ten_PB }}</td>
            <td>
              <font-awesome-icon
                icon="pen"
                style="color: #74c0fc; margin-right: 20px; cursor: pointer"
                @click="openDialogEdit(chucvu.id_CV)"
              />
              <font-awesome-icon
                icon="trash"
                style="color: #ff0000; cursor: pointer"
                @click="deleteCV(chucvu.id_CV)"
              />
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <div class="table-info">
      <span>{{ startIndex }} - {{ endIndex }} of {{ totalItems }}</span>
      <div class="pagination-controls">
        <span>Chọn số dòng</span>
        <v-select
          v-model="pageSize"
          :items="itemsPerPageOptions"
          @change="updatePageSize"
          outlined
          dense
          class="row-select"
        ></v-select>
        <v-select
          v-model="currentPage"
          :items="pageOptions"
          label="Trang"
          @change="changePage"
          outlined
          dense
          class="page"
        ></v-select>
      </div>
    </div>
    <v-dialog v-model="dialogEdit" width="600px">
      <EditChucVu
        :chucvuId="selectedId_CV"
        @close="dialogEdit = false"
        @successMessageEdit="handleSuccessMessageEdit"
        @errorMessageEdit="handleErrorMessageEdit"
      />
    </v-dialog>
  </div>
</template>

<script>
import ChucVuApi from "../../api/ChucVuService";
import EditChucVu from "./Edit.vue";
import Swal from "sweetalert2";
export default {
  name: "ListChucVu",
  data() {
    return {
      title: "Danh sách chức vụ",
      chucvus: [],
      pageSize: 5,
      currentPage: 1,
      itemsPerPageOptions: [5, 10, 20, 40],
      dialogEdit: false,
      selectedId_CV: null,
    };
  },
  components: {
    EditChucVu,
  },
  computed: {
    totalItems() {
      return this.chucvus.length;
    },
    totalPages() {
      return Math.ceil(this.totalItems / this.pageSize);
    },
    startIndex() {
      return (this.currentPage - 1) * this.pageSize + 1;
    },
    endIndex() {
      const end = this.currentPage * this.pageSize;
      return end > this.totalItems ? this.totalItems : end;
    },
    displayedChucVus() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = start + this.pageSize;
      return this.chucvus.slice(start, end);
    },
    pageOptions() {
      const options = [];
      for (let i = 1; i <= this.totalPages; i++) {
        options.push(i);
      }
      return options;
    },
  },
  methods: {
    openDialogEdit(Id_CV) {
      this.selectedId_CV = Id_CV;
      this.dialogEdit = true;
    },
    async fetchChucVus() {
      try {
        const response = await ChucVuApi.getAllChucVu();
        console.log("Fetched data:", response.data);
        this.chucvus = response.data;
      } catch (error) {
        console.error("Error fetching chucvus:", error);
      }
    },
    paginate(page) {
      this.currentPage = page;
    },
    updatePageSize(size) {
      this.pageSize = size;
      this.paginate(1); // Reset to first page after changing page size
    },
    changePage(page) {
      this.currentPage = page;
    },
    async deleteCV(id) {
      try {
        const result = await Swal.fire({
          title: "Thông báo",
          text: "Bạn có chắc muốn xóa chức vụ này!",
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Yes",
        });

        if (result.isConfirmed) {
          await ChucVuApi.deleteChucVu(id);
          await this.fetchChucVus();
          Swal.fire("Thông báo", "Chức vụ đã được xóa!", "success");
        }
      } catch (error) {
        console.error("Error deleting phong ban:", error);
        Swal.fire("Thông báo", "Chức vụ này đang được dùng!", "error");
      }
    },
    handleSuccessMessageEdit(message) {
      console.log("Event received: successMessageEdit", message);
      Swal.fire({
        icon: "success",
        title: "Thành công",
        text: message,
        timer: 3000,
        showConfirmButton: false,
        position: "bottom-end",
        toast: true,
      });
      this.fetchChucVus();
    },
    handleErrorMessageEdit(message) {
      Swal.fire({
        icon: "error",
        title: "Thất bại",
        text: message,
        timer: 3000,
        showConfirmButton: false,
        position: "bottom-end",
        toast: true,
      });
    },
  },
  async mounted() {
    await this.fetchChucVus();
  },
};
</script>

<style scoped>
.table-container {
  max-width: 90%;
  margin: auto;
  overflow-x: auto;
}
.table-wrapper {
  height: 280px;
  overflow-y: scroll;
}

.styled-table {
  width: 100%;
  border-collapse: collapse;
  margin: 25px 0;
  font-size: 0.9em;
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  box-shadow: 0 0 20px rgba(0, 0, 0, 0.15);
}

.styled-table thead tr {
  background-color: #009879;
  color: #ffffff;
  text-align: left;
}

.styled-table th,
.styled-table td {
  padding: 12px 15px;
  text-align: center;
}

.styled-table tbody tr {
  border-bottom: 1px solid #dddddd;
}

.styled-table tbody tr:nth-of-type(even) {
  background-color: #f3f3f3;
}

.styled-table tbody tr:last-of-type {
  border-bottom: 2px solid #009879;
}

.styled-table tbody tr.active-row {
  font-weight: bold;
  color: #009879;
}

.page,
.row-select {
  margin-left: 10px;
}

.table-info {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 10px;
}
.table-info {
  margin-top: 20px;
}
.pagination-controls {
  display: flex;
  align-items: center;
}
.header-container {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 10px;
  margin-top: 10px;
}

.left-section {
  display: flex;
  align-items: center;
}

.right-section {
  display: flex;
  align-items: center;
}
</style>
