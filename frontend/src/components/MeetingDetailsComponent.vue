<template>
  <v-card class="px-5">
    <v-container>
    <v-card-title>
      {{ meeting.name }}
    </v-card-title>
    <v-card-subtitle>
      {{ meeting.Description }}
    </v-card-subtitle>
    <v-row justify="center">
      Meeting will take place on: {{ new Date(meeting.start).toDateString() }}<br>
      If you have any questions, ask organizer: {{ meeting.organizerName }}
    </v-row>
    <v-divider class="mt-2 mb-2"/>
    <v-row justify="center">
      <h3>Invited Guests:</h3>
    </v-row>
    <v-row justify="center">
      <v-list>
        <v-list-item v-for="(member, index) in members" :key="index">
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

import {API_URL, COUSINE_LIST, MOVIE_GENRES} from "@/config/consts";
import axios from "axios";

export default {
  name: "MeetingDetailsComponent",
  props: {
    meeting: {
    }
  },
  data: function () {
    return {
      moviesGenres: MOVIE_GENRES,
      foodTypes: COUSINE_LIST,
      organizerName: '',
      idToNames: [],
      members: [],
      preferences: {
        meetingId: this.meeting.id,
        moviePreference: '',
        foodPreference: ''
      }
    }
  },
  created() {
    console.log(this.meeting)
    this.initializeComponent()
  },
  methods: {
    async initializeComponent(){
      axios.get(API_URL + '/user/names', {
        headers: {
          'Authorization': 'Bearer ' + this.$store.state.accessToken
        }
      })
          .then(res => {
            this.idToNames = res.data
            this.idToNames.push({
              id: this.$store.state.user.id,
              name: this.$store.state.user.name
            })
            this.meeting.organizerName = this.idToNames.filter(it => it.id === this.meeting.organiserID)[0].name
            this.fetchMeetingMembers()
          })
    },
    async fetchMeetingMembers(){
      axios.get( API_URL + '/group/' + this.meeting.groupID, {
        headers: {
          'Authorization': 'Bearer ' + this.$store.state.accessToken
        }
      }).then(res => {
        let memberIds = res.data.memberIDs
        memberIds.forEach(id => this.members.push(this.idToNames.filter(pair => pair.id === id)[0].name))
      })
    },
    raiseCloseEvent() {
      this.$emit('closeEvent')
    },
    confirmMeeting() {
      console.log(this.preferences)
    },
    loggedUserIsOrganizer() {
      return this.$store.state.user.login === this.meeting.organizer
    }
  }
}
</script>