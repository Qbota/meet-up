<template>
  <v-card height="450px">
    <v-card-title>
      Pick you favourite foods
    </v-card-title>
    <v-container>
      <v-btn color="primary"
             text @click="savePreferences()">Save
      </v-btn>
      <v-simple-table height="300px" dense>
        <thead>
        <tr>
          <th class="text-left">
            Movie Genre
          </th>
          <th class="text-left">
            Do you like that?
          </th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="(genre, index) in genres" :key="index">
          <td>{{ genre }}</td>
          <td>
            <v-checkbox dense v-model="selectedGenres" :value="genre"></v-checkbox>
          </td>
        </tr>
        </tbody>
      </v-simple-table>
    </v-container>
  </v-card>
</template>


<script>
import {API_URL, MOVIE_GENRES} from "@/config/consts";
import axios from 'axios'

export default {
  name: "MoviePreferencesComponent",
  created() {
    this.token = this.$store.state.accessToken
    this.user = this.$store.state.user
    this.selectedGenres = this.user.moviePreference.movieGenres
  },
  data: function () {
    return {
      genres: MOVIE_GENRES,
      selectedGenres: null
    }
  },
  methods: {
    savePreferences() {
      this.user.moviePreference.movieGenres = this.selectedGenres
      axios.put(API_URL + '/user', this.user, {
        headers: {
          'Authorization': 'Bearer ' + this.token
        }
      })
    }
  }
}
</script>