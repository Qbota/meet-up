<template>
  <div>
    <PublicNavbarComponent/>
    <v-row justify="center">
      <img @click="goToLanding()" src="../../assets/logo.jpg" style="height: 30%; width: 30%" alt="Logo">
    </v-row>
    <v-row justify="center">
      <h1><i>Welcome to MeetUP!</i></h1>
    </v-row>
    <v-main>
      <v-row align="center" justify="center">
        <v-card>
          Register Form
        </v-card>
      </v-row>
    </v-main>
  </div>
</template>

<script>
import PublicNavbarComponent from "@/components/PublicNavbarComponent";

export default {
  name: "RegisterView",
  components: {PublicNavbarComponent},
  methods: {
    goToLanding() {
      this.$router.push('/')
    }
  }
}
</script>