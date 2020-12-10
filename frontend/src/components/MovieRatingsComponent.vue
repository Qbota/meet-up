<template>
  <v-card height="500px" width="800px">
    <v-card-title>
      Rate movies
    </v-card-title>
    <v-container>
      <v-btn color="primary"
             text @click="savePreferences()">Save
      </v-btn>
      <v-simple-table height="350px" width="700px">
        <template v-slot:default>
          <thead>
          <tr>
            <th class="text-left">
              Title
            </th>
            <th class="text-left">
              Rating
            </th>
            <th>
              Edit rating
            </th>

          </tr>
          </thead>
          <tbody>
          <tr
              v-for="movie in movies"
              :key="movie.title"
          >
            <td>{{ movie.title }}</td>
            <td>{{ ratings[movie.id] }}</td>
            <td>
              <v-btn
                  color="primary"
                  dark
                  @click="editMovie(movie)"
              >
                Edit rate
              </v-btn>

            </td>
          </tr>
          </tbody>
        </template>
      </v-simple-table>
    </v-container>
    <v-dialog
        v-model="dialog"
        max-width="600"
    >
      <v-card>
        <v-card-title class="headline">
          Change movie rate
        </v-card-title>

        <v-card-text>
          Rate movie again in range 1-10
        </v-card-text>

        <v-card-actions>

          <v-rating v-model="newRating" clearable dense size="25" length="10" hover/>

          <v-spacer></v-spacer>

          <v-btn
              color="green darken-1"
              text
              @click="dialog = false"
          >
            Cancel
          </v-btn>

          <v-btn
              color="green darken-1"
              text
              @click="updateRank(newRating)"
          >
            Confirm
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-card>
</template>

<script>
import {API_URL} from "@/config/consts";
import axios from 'axios'

export default {
  name: "MoviePreferencesComponent",
  created() {
    this.token = this.$store.state.accessToken
    this.user = this.$store.state.user
    this.movies = this.user.moviePreference.movies
    this.ratings = this.user.moviePreference.ratings
  },
  data: function () {
    return {
      movies: [],
      ratings: [],
      dialog: false,
      selectedMovie: null,
      newRating: 5
    }
  },
  methods: {
    editMovie(movie) {
      console.log(movie.title)
      this.selectedMovie = movie
      this.dialog = true
    },
    updateRank(newRating) {
      this.ratings[this.selectedMovie.id] = newRating
      this.dialog = false
    },
    savePreferences() {
      this.user.moviePreference.ratings = this.ratings
      axios.put(API_URL + '/user', this.user, {
        headers: {
          'Authorization': 'Bearer ' + this.token
        }
      })
    }
  }
}
</script>