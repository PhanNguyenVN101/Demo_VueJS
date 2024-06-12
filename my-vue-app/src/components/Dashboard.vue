<template>
  <v-navigation-drawer color="#42b983" dark>
    <v-list>
      <v-list-item>
        <router-link to="/" class="router-link-style"
          ><v-list-item-content>
            <v-list-item-title class="white--text">
              <font-awesome-icon icon="users" />
              <span class="text-icon">Nhân viên</span>
            </v-list-item-title>
          </v-list-item-content></router-link
        >
      </v-list-item>

      <v-list-item>
        <router-link to="/ListPhongBan" class="router-link-style"
          ><v-list-item-content>
            <v-list-item-title class="white--text">
              <font-awesome-icon icon="building" />
              <span class="text-icon">Phòng Ban</span>
            </v-list-item-title>
          </v-list-item-content></router-link
        >
      </v-list-item>

      <v-list-item>
        <router-link to="/ListChucVu" class="router-link-style"
          ><v-list-item-content>
            <v-list-item-title class="white--text">
              <font-awesome-icon icon="layer-group" />
              <span class="text-icon">Chức vụ</span>
            </v-list-item-title>
          </v-list-item-content></router-link
        >
      </v-list-item>

      <v-list-item>
        <router-link to="/About" class="router-link-style"
          ><v-list-item-content>
            <v-list-item-title class="white--text">
              <font-awesome-icon icon="newspaper" />
              <span class="text-icon">About</span>
            </v-list-item-title>
          </v-list-item-content></router-link
        >
      </v-list-item>
    </v-list>
  </v-navigation-drawer>
</template>

<script>
export default {
  name: "DashboardComponent",

  methods: {
    navigateTo(route) {
      this.$router.push(route);
    },
  },
};
</script>

<style scoped>
.text-icon {
  margin-left: 10px;
}
</style>
