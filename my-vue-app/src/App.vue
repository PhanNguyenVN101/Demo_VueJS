<template>
  <v-app>
    <HeaderComponent />
    <DashboardComponent />
    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
import HeaderComponent from "./components/Header.vue";
import DashboardComponent from "./components/Dashboard.vue";
export default {
  name: "App",
  components: {
    HeaderComponent,
    DashboardComponent,
  },
};
</script>
