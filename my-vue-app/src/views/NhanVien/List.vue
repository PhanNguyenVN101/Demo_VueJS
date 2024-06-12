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
            <th>Họ tên</th>
            <th>Email</th>
            <th>Phòng ban</th>
            <th>Chức vụ</th>
            <th>Thao tác</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(nhanvien, index) in displayedNhanViens"
            :key="nhanvien.id_NV"
          >
            <td>{{ startIndex + index }}</td>
            <td>{{ nhanvien.id_NV }}</td>
            <td>
              <a href="#" @click.prevent="openDetailDialog(nhanvien.id_NV)">{{
                nhanvien.hoTen_NV
              }}</a>
            </td>

            <td>{{ nhanvien.email_NV }}</td>
            <td>{{ nhanvien.ten_PB }}</td>
            <td>{{ nhanvien.ten_CV }}</td>
            <td>
              <font-awesome-icon
                icon="pen"
                style="color: #74c0fc; margin-right: 20px; cursor: pointer"
                @click="openDialogEdit(nhanvien.id_NV)"
              />

              <font-awesome-icon
                icon="trash"
                style="color: #ff0000; cursor: pointer"
                @click="deleteNV(nhanvien.id_NV)"
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
      <EditNhanVien
        :nhanvienId="selectedId_NV"
        @close="dialogEdit = false"
        @successMessageEdit="handleSuccessMessageEdit"
        @errorMessageEdit="handleErrorMessageEdit"
      />
    </v-dialog>
    <v-dialog v-model="dialogDetail" width="600px">
      <DetailNhanVien
        :nhanvienId="selectedId_NV"
        @close="dialogDetail = false"
      />
    </v-dialog>
  </div>
</template>

<script>
import NhanVienApi from "../../api/NhanVienService";
import EditNhanVien from "./Edit.vue";
import DetailNhanVien from "./Detail.vue";
import Swal from "sweetalert2";
export default {
  name: "ListNhanVien",
  data() {
    return {
      title: "Danh sách nhân viên",
      nhanviens: [],
      pageSize: 5,
      currentPage: 1,
      itemsPerPageOptions: [5, 10, 20, 40],
      dialogEdit: false,
      dialogDetail: false,
      selectedId_NV: null,
    };
  },
  components: {
    EditNhanVien,
    DetailNhanVien,
  },
  computed: {
    totalItems() {
      return this.nhanviens.length;
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
    displayedNhanViens() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = start + this.pageSize;
      return this.nhanviens.slice(start, end);
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
    openDialogEdit(Id_NV) {
      this.selectedId_NV = Id_NV;
      this.dialogEdit = true;
    },
    async fetchNhanViens() {
      try {
        const response = await NhanVienApi.getAllNhanVien();
        console.log("Fetched data:", response.data);
        this.nhanviens = response.data;
      } catch (error) {
        console.error("Error fetching nhan viens:", error);
      }
    },
    async deleteNV(id) {
      try {
        const result = await Swal.fire({
          title: "Thông báo",
          text: "Bạn có chắc muốn xóa nhân viên này!",
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Yes",
        });

        if (result.isConfirmed) {
          await NhanVienApi.deleteNhanVien(id);
          await this.fetchNhanViens();
          Swal.fire("Thông báo", "Nhân viên đã được xóa!", "success");
        }
      } catch (error) {
        console.error("Error deleting nhan vien:", error);
        Swal.fire("Thông báo", "Không thể xóa được nhân viên này!", "error");
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
    openDetailDialog(id) {
      this.selectedId_NV = id;
      this.dialogDetail = true;
    },
    handleSuccessMessageEdit(message) {
      Swal.fire({
        icon: "success",
        title: "Thành công",
        text: message,
        timer: 3000,
        showConfirmButton: false,
        position: "bottom-end",
        toast: true,
      });
      this.fetchNhanViens();
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
    await this.fetchNhanViens();
  },
};
</script>

<style scoped>
.table-container {
  max-width: 90%;
  margin: auto;
  overflow-x: auto;
}
.table-info {
  margin-top: 20px;
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
.styled-table tbody {
  max-height: 200px;
  overflow-y: auto;
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
