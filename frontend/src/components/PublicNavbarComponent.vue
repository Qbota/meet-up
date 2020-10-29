<template>
  <v-app-bar flat dense color="primary">
    <v-spacer/>
    <v-btn class="ml-5; mr-5" text to="/login">Login</v-btn>
    <v-btn class="ml-5; mr-5" text to="/register">Register</v-btn>
    <v-spacer/>
  </v-app-bar>
</template>

<script>
export default {
name: "PublicNavbarComponent"
}
</script>