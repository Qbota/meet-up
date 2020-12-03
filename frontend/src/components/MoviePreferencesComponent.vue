<template>
  <v-card height="400px">
  <v-simple-table height="350px">
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
          v-for="movie in userMoviePrefs"
          :key="movie.title"
      >
        <td>{{ movie.title }}</td>
        <td>{{ movie.rating }}</td>
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

          <v-spacer>          </v-spacer>

          <v-btn
              color="green darken-1"
              text
              @click="dialog = false"
          >
            Disagree
          </v-btn>

          <v-btn
              color="green darken-1"
              text
              @click="updateRank(newRating)"
          >
            Agree
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-card>
</template>


<script>


export default {
  name: "MoviePreferencesComponent",
  created() {
    this.token =  this.$store.state.accessToken
    this.user = this.$store.state.user
    this.fetchPrefs()
  },
  data: function () {
    return {
      userMoviePrefs: this.user.MoviePreference.Ratings,
      dialog: false,
      selectedMovie: null,
      newRating: 5
    }
  },
  methods: {
    //add here methods
    example() {
      console.log('hi')
    },
    editMovie(movie){
      console.log(movie.title)
      this.selectedMovie = movie
      this.dialog = true
    },
    updateRank(newRating) {
      console.log(this.$store.state.user)
      this.selectedMovie.rating = newRating
      this.dialog = false
    }

  }
}
</script>