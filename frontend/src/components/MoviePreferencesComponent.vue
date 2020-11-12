<template>
  <v-simple-table height="300px">
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
      @click.stop="dialog = true"
    >
      Edit rate
    </v-btn>

    <v-dialog
      v-model="dialog"
      max-width="290"
    >
      <v-card v-bind:key="movie.title">
        <v-card-title class="headline">
          Change movie rate
        </v-card-title>

        <v-card-text>
          Rate movie again in range 1-10
        </v-card-text>

        <v-card-actions>
            <v-btn
            color="green darken-1"
            text
            @click="updateRank(movie)"
          >
            0
          </v-btn>

          <v-spacer></v-spacer>

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
            @click="dialog = false"
          >
            Agree
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
          </td>
        </tr>
      </tbody>
    </template>
  </v-simple-table>


  
</template>



<script>
import {API_URL} from "@/config/consts";
import axios from 'axios'


export default {
name: "MoviePreferencesComponent",
  created() {
    this.fetchPrefs()
  },
  data: function (){
    return {
      //Add here properties like this
      userMoviePrefs: [],
      dialog: false
    }
  },
  methods: {
    //add here methods
    example(){
      console.log('hi')
    },
    async fetchPrefs() {
      axios.get(API_URL + '/userMoviePrefs')
          .then(res => this.userMoviePrefs = res.data)
    },
    updateRank(movie) {
      console.log(movie.title);
      movie.rating = 5;
      //for (i in this.userMoviePrefs) {
      //  if (i.title === title){
      //    i.rating = rank;
      //  }
      //}
    }

  }
}
</script>