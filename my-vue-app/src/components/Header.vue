<template>
  <v-app-bar app color="#42b983" dark>
    <v-toolbar-title>
      <router-link to="/" class="router-link-style">
        <v-avatar color="white">
          <img src="../assets/logo.png" alt="Vue Logo" class="logo" />
        </v-avatar>
        VUEJS DEMO
      </router-link>
    </v-toolbar-title>
    <v-spacer></v-spacer>
    <AccountComponent />
  </v-app-bar>
</template>

<script>
import AccountComponent from "./Account.vue";
export default {
  name: "HeaderComponent",
  components: {
    AccountComponent,
  },
};
</script>

<style scoped>
.logo {
  width: 30px;
  height: 30px;
}
</style>
