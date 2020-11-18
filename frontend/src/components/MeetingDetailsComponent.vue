<template>
  <v-card class="px-5">
    <v-container>
    <v-card-title>
      {{ meeting.name }}
    </v-card-title>
    <v-card-subtitle>
      {{ meeting.description }}
    </v-card-subtitle>
    <v-row justify="center">
      Meeting will take place on: {{ new Date(meeting.start).toDateString() }}<br>
      If you have any questions, ask organizer: {{ meeting.organizer }}
    </v-row>
    <v-divider class="mt-2 mb-2"/>
    <v-row justify="center">
      <h3>Invited Guests:</h3>
    </v-row>
    <v-row justify="center">
      <v-list>
        <v-list-item v-for="(member, index) in meeting.members" :key="index">
          {{ member }}
        </v-list-item>
      </v-list>
    </v-row>
    <v-divider class="mt-2 mb-2"/>
    <v-row justify="center">
      <v-autocomplete label="Choose movie genre" v-model="preferences.moviePreference" :items="moviesGenres"/>
    </v-row>
    <v-row justify="center">
      <v-autocomplete label="Choose food type" v-model="preferences.foodPreference" :items="foodTypes"/>
    </v-row>
    <v-card-actions>
      <v-btn @click="raiseCloseEvent">Close</v-btn>
      <v-spacer/>
      <v-btn>Calculate suggestions</v-btn>
      <v-spacer/>
      <v-btn @click="confirmMeeting">Confirm</v-btn>
    </v-card-actions>
      <v-row justify="center">
      </v-row>
    </v-container>
  </v-card>
</template>

<script>
import axios from 'axios'
import {API_URL} from "@/config/consts";

export default {
  name: "MeetingDetailsComponent",
  props: {
    meeting: {
      name: '',
      start: Date,
      organizer: '',
      description: '',
      members: []
    }
  },
  data: function () {
    return {
      moviesGenres: [],
      foodTypes: [],
      preferences: {
        meetingId: this.meeting.id,
        moviePreference: '',
        foodPreference: ''
      }
    }
  },
  created() {
    this.fetchFoodTypes()
    this.fetchMovieGenres()
  },
  methods: {
    raiseCloseEvent() {
      this.$emit('closeEvent')
    },
    confirmMeeting() {
      console.log(this.preferences)
    },
    loggedUserIsOrganizer() {
      return this.$store.state.user.login === this.meeting.organizer
    },
    fetchMovieGenres() {
      axios.get(API_URL + '/movie/genre')
          .then(res => this.moviesGenres = res.data)
    },
    fetchFoodTypes() {
      axios.get(API_URL + '/food/type')
          .then(res => this.foodTypes = res.data)
    }
  }
}
</script>