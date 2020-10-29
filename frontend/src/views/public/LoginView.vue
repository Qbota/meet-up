<template>
  <div>
    <PublicNavbarComponent/>
    <v-main>
      <v-container>
        <v-row justify="center">
            <img @click="goToLanding()" src="../../assets/logo.jpg" style="height: 30%; width: 30%" alt="Logo">
        </v-row>
        <v-row justify="center">
          <h1><i>Welcome to MeetUP!</i></h1>
        </v-row>
        <v-row justify="center">
          <v-form>
            <v-row>
              <v-text-field label="Username of e-mail" v-model="login"></v-text-field>
            </v-row>
            <v-row>
              <v-text-field label="Password" type="password" v-model="password"></v-text-field>
            </v-row>
            <v-row justify="center" class="mb-5">
              <v-btn color="primary" large @click="loginAction()">
                <div class="px-5">
                  Login
                </div>
              </v-btn>
            </v-row>
            <v-row justify="center">
              <v-btn color="primary">
                <div class="px-1">
                  Register
                </div>
              </v-btn>
            </v-row>
          </v-form>
        </v-row>
      </v-container>
    </v-main>
  </div>
</template>

<script>
import PublicNavbarComponent from "@/components/PublicNavbarComponent";
export default {
name: "LoginView",
  components: {PublicNavbarComponent},
  data: () => ({
    login: '',
    password: ''
  }),
  methods: {
    loginAction() {
      console.log(this.login)
      console.log(this.password)
    },
    goToLanding() {
      this.$router.push('/')
    }
  }
}
</script>