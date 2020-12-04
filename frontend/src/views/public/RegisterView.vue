<template>
  <div>
    <PublicNavbarComponent/>
    <v-row justify="center">
      <img @click="goToLanding()" src="../../assets/logo.jpg" style="height: 30%; width: 30%" alt="Logo">
    </v-row>
    <v-main class="mb-16">
      <v-row align="center" justify="center">
        <v-col cols="8">
          <v-stepper v-model="stepper">
            <v-stepper-header>
              <v-stepper-step :complete="stepper > 1" step="1">Login data</v-stepper-step>
              <v-divider/>
              <v-stepper-step :complete="stepper > 2" step="2">Movie preferences</v-stepper-step>
              <v-divider/>
              <v-stepper-step :complete="stepper > 3" step="3">Food preferences</v-stepper-step>
            </v-stepper-header>
            <v-stepper-items>
              <v-stepper-content step="1">
                <v-card class="pa-7" style="margin-left: 20%; margin-right: 20%; margin-bottom: 5%">
                  <v-form v-model="loginDataValid">
                    <v-text-field id="100" label="Login" v-model="registerCommand.Login" :rules="loginRules"></v-text-field>
                    <v-text-field id="101" label="Name" v-model="registerCommand.Name" :rules="nameRules"></v-text-field>
                    <v-text-field id="102" label="Password" v-model="registerCommand.Password" :rules="passwordRules"
                                  type="password"></v-text-field>
                    <v-text-field id="103" label="Confirm password" v-model="passwordConfirmation" :rules="passwordConfirmRules"
                                  type="password"></v-text-field>
                  </v-form>
                  <v-card-actions>
                    <v-spacer/>
                    <v-btn id="200" @click="stepper++" :disabled="!loginDataValid">Next</v-btn>
                  </v-card-actions>
                </v-card>
              </v-stepper-content>
              <v-stepper-content step="2">
                <v-card>
                  <v-row justify="center">
                    <template v-for="movie in movies">
                      <v-card v-bind:key="movie.id" height="450px" width="350px" class="ma-3 pa-4">
                        <v-row justify="center">
                          <v-card-title>{{ movie.title }}</v-card-title>
                        </v-row>
                        <v-row justify="center">
                          <img style="height: 300px" :src="movie.poster"/>
                        </v-row>
                        <v-row justify="center" class="pb-5 pt-2">
                          {{ translateNumberToRating(movie.rating) }} {{ movie.rating }}
                          <v-icon color="primary">mdi-star</v-icon>
                          <v-rating v-model="movie.rating" clearable dense size="25" length="10" hover/>
                        </v-row>
                      </v-card>
                    </template>
                  </v-row>
                  <v-card-actions>
                    <v-btn @click="stepper--">Back</v-btn>
                    <v-spacer/>
                    <v-btn @click="stepper++; saveMoviePreferences()">Next</v-btn>
                  </v-card-actions>
                </v-card>
              </v-stepper-content>
              <v-stepper-content step="3">
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
                <v-card class="pa-7" style="margin-left: 20%; margin-right: 20%; margin-bottom: 5%">
                  <v-card-title>
                    Food allergens
                  </v-card-title>
                  <v-row justify="center">
                    <v-list flat>
                      <template v-for="(preference,i) in registerCommand.Allergens">
                        <v-list-item v-bind:key="i">
                          <v-list-item-content>
                            {{ preference }}
                          </v-list-item-content>
                          <v-list-item-action>
                            <v-btn icon @click="removeFromPreferences(preference)">
                              <v-icon>mdi-food-variant-off</v-icon>
                            </v-btn>
                          </v-list-item-action>
                        </v-list-item>
                      </template>
                      <v-list-item>
                        <v-list-item-action>
                          <v-btn @click="popup = true">Add allergy</v-btn>
                        </v-list-item-action>
                      </v-list-item>
                    </v-list>
                  </v-row>
                  <v-card-actions>
                    <v-btn @click="stepper--">Back</v-btn>
                    <v-spacer/>
                    <v-btn @click="registerInApi()" :loading="isLoading">Confirm</v-btn>
                  </v-card-actions>
                </v-card>
              </v-stepper-content>
            </v-stepper-items>
          </v-stepper>
        </v-col>
      </v-row>
    </v-main>
    <v-dialog v-model="popup" max-width="400">
      <v-card>
        <v-card-title>
          Choose allergy from list
        </v-card-title>
        <v-list>
          <template v-for="(allergen,i) in allergens">
            <v-list-item v-bind:key="i">
              <v-checkbox v-model="registerCommand.Allergens" :label="allergen" :value="allergen"/>
            </v-list-item>
          </template>
        </v-list>
        <v-card-actions>
          <v-spacer/>
          <v-btn @click="popup = false">Close</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import PublicNavbarComponent from "@/components/PublicNavbarComponent";
import {API_URL} from "@/config/consts";
import axios from 'axios'

export default {
  name: "RegisterView",
  components: {PublicNavbarComponent},
  created() {
    this.fetchMoviesToRate()
  },
  data: function () {
    return {
      stepper: 1,
      isLoading: false,
      loginDataValid: false,
      movies: [],
      registerCommand: {
        Login: '',
        Name: '',
        Password: '',
        Movies: {},
        Allergens: []
      },
      allergens: [
        'dairy',
        'eggs',
        'seaFood',
        'nuts',
        'soy',
        'wheat',
        'meat'
      ],
      popup: false,
      snackbar: false,
      passwordConfirmation: '',
      nameRules: [
        v => !!v || 'Name is required',
      ],
      loginRules: [
        v => !!v || 'Login is required',
        v => (v && v.length <= 15) || 'Login must be less than 15 characters'
      ],
      passwordRules: [
        v => !!v || 'Password is required',
        v => (v && this.testPasswordForSpecialCharacters(v)) || 'Password must contain special characters',
        v => (v && /\d/.test(v)) || 'Password must contain a number',
        v => (v && v.length >= 8) || 'Password must be longer than 8 characters'
      ],
      passwordConfirmRules: [
        v => !!v || 'Please confirm password',
        v => (v && v === this.registerCommand.Password) || 'Passwords are not matching'
      ]
    }
  },
  methods: {
    goToLanding() {
      this.$router.push('/')
    },
    testEmailAgainstRegex(email) {
      let emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
      return emailPattern.test(email);
    },
    testPasswordForSpecialCharacters(password) {
      let passwordPattern = new RegExp("(?=.*[!@#$%^&*])")
      return passwordPattern.test(password)
    },
    async fetchMoviesToRate() {
      axios.get(API_URL + '/movies')
          .then(res => this.movies = res.data)
    },
    removeFromPreferences(item) {
      this.registerCommand.Allergens = this.registerCommand.Allergens.filter(
          x => x !== item
      )
    },
    saveMoviePreferences() {
      this.movies.forEach(movie => this.registerCommand.Movies[movie.id.toString()] = movie.rating)
    },
    async registerInApi() {
      this.isLoading = true
      axios.post(API_URL + '/user', this.registerCommand, {})
          .then(res => {
            this.handleRegisterSuccess(res);
          })
          .catch(err => {
            this.handleRegisterFailure(err);
          })
    },
    handleRegisterFailure: function (err) {
      console.log(err)
      this.isLoading = false
    },
    handleRegisterSuccess: function (res) {
      console.log(res)
      this.$store.state.accessToken = res.data.accessToken
      this.$store.state.refreshToken = res.data.refreshToken.tokenString
      this.$store.state.user = res.data.user
      this.isLoading = false
      this.$router.push('/home')
    },
    translateNumberToRating(number) {
      switch (number) {
        case 1:
          return 'Disappointment'
        case 2:
          return 'Really bad'
        case 3:
          return 'Bad'
        case 4:
          return 'Can be'
        case 5:
          return 'Average'
        case 6:
          return 'Not bad'
        case 7:
          return 'Good'
        case 8:
          return 'Very good'
        case 9:
          return 'Outstanding'
        case 10:
          return 'Masterpiece!'
        default:
          return 'Haven\' seen this'
      }
    }
  }
}
</script>