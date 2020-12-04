<template>
  <v-card height="400px">
    <v-container>
      <v-btn @click="savePreferences()">Save</v-btn>
            <v-simple-table height="300px" dense>
              <tbody>
              <tr v-for="(genre, index) in genres" :key="index">
                <td>{{genre}}</td>
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
import {API_URL} from "@/config/consts";
import axios from 'axios'

export default {
  name: "MoviePreferencesComponent",
  created() {
    this.token =  this.$store.state.accessToken
    this.user = this.$store.state.user
    this.selectedGenres = this.user.moviePreference.movieGenres
  },
  data: function () {
    return {
      genres: [
        'Animation', 
        'Comedy', 
        'Family',
        'Adventure', 
        'Fantasy',
        'Romance',
        'Drama',
        'Action', 
        'Crime', 
        'Thriller',
        'Horror',
        'History',
        'Documentary',
        'Mystery', 
        'Science Fiction',
        'War',
        'Western'
      ],
      selectedGenres: null
    }
  },
  methods: {
     savePreferences(){
        this.user.moviePreference.movieGenres = this.selectedGenres
        axios.put(API_URL + '/user', this.user, {
        headers: {
            'Authorization': 'Bearer '+ this.token
        }})
    }
  }
}
</script>