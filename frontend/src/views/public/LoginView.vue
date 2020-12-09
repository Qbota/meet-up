<template>
  <div>
    <PublicNavbarComponent/>
    <v-main>
      <v-container v-on:keyup.enter="loginAction()">
        <v-row justify="center">
          <img @click="goToLanding()" src="../../assets/logo.jpg" style="height: 30%; width: 30%" alt="Logo">
        </v-row>
        <v-row justify="center">
          <h1><i>Welcome to MeetUP!</i></h1>
        </v-row>
        <v-row justify="center">
          <v-snackbar
              absolute
              v-model="snackbar"
              color="error"
          >
            Invalid username or password
            <template v-slot:action="{ attrs }">
              <v-btn
                  text
                  v-bind="attrs"
                  @click="snackbar = false"
              >
                Close
              </v-btn>
            </template>
          </v-snackbar>
          <v-form>
            <v-row>
              <v-text-field label="Username of e-mail" v-model="user.login"></v-text-field>
            </v-row>
            <v-row>
              <v-text-field label="Password" type="password" v-model="user.password"></v-text-field>
            </v-row>
            <v-row justify="center" class="mb-5">
              <v-btn color="primary" large @click="loginAction()" :loading="isLoading">
                <div class="px-5">
                  Login
                </div>
              </v-btn>
            </v-row>
            <v-row justify="center">
              <v-btn color="primary" @click="goToRegister()">
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
import axios from 'axios';
import {API_URL} from '@/config/consts'

export default {
  name: "LoginView",
  components: {PublicNavbarComponent},
  data: () => ({
    user: {
      login: '',
      password: ''
    },
    snackbar: false,
    isLoading: false
  }),
  methods: {
    async loginAction() {
      this.isLoading = true
      axios.post(API_URL + '/login', this.user, {})
          .then(res => this.handleLoginSuccess(res))
          .catch(() => this.handleLoginFailure())
    },
    handleLoginSuccess(res) {
      this.$store.state.accessToken = res.data.accessToken
      this.$store.state.refreshToken = res.data.refreshToken.tokenString
      this.$store.state.user = res.data.user
      this.isLoading = false
      this.$router.push('/home')
    },
    handleLoginFailure() {
      this.isLoading = false
      this.snackbar = true
    },
    goToLanding() {
      this.$router.push('/')
    },
    goToRegister() {
      this.$router.push('/register')
    },
  }
}
</script>