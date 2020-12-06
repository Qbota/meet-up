<template>
  <v-card class="px-5">
    <v-container>
      <v-card-title>
        Create meeting
      </v-card-title>
      <v-row justify="center">
        <v-text-field label="Meeting title" v-model="meeting.Title"/>
      </v-row>
      <v-row justify="center">
        <v-text-field label="Meeting description" v-model="meeting.Description"/>
      </v-row>
      <v-row justify="center">
        <v-menu ref="menu" v-model="menu" :close-on-content-click="false" :return-value.sync="meeting.Dates" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-combobox v-model="meeting.Dates" multiple chips small-chips label="Choose dates" readonly v-bind="attrs" v-on="on"/>
          </template>
          <v-date-picker v-model="meeting.Dates" multiple no-title scrollable>
            <v-spacer/>
            <v-btn text color="primary" @click="menu = false">
              Cancel
            </v-btn>
            <v-btn text color="primary" @click="$refs.menu.save(meeting.Dates)">
              Ok
            </v-btn>
          </v-date-picker>
        </v-menu>
      </v-row>
      <v-row justify="center">
        <v-autocomplete label="Choose group" v-model="meeting.GroupId" :items="Groups" item-text="name" item-value="id"/>
      </v-row>
      <v-card-actions>
        <v-btn @click="raiseCloseEvent">Close</v-btn>
        <v-spacer/>
        <v-btn @click="registerMeeting()">Create Meeting</v-btn>
      </v-card-actions>
    </v-container>
  </v-card>
</template>

<script>
import axios from 'axios'
import {API_URL} from "@/config/consts";
export default {
  name: "CreateMeetingComponent",
  created() {
    this.token =  this.$store.state.accessToken
    this.user = this.$store.state.user
    this.fetchGroups()
  },
  data: function () {
    return {
      menu: false,
      Groups: [],
      meeting: {
        Title: '',
        Description: '',
        Dates: [],
        GroupId: ''
      }
    }
  },
  methods: {
    raiseCloseEvent() {
      this.meeting = {
        title: '',
        description: '',
        dates: [],
        members: [],
      }
      this.$emit('closeEvent')
    },
    async fetchGroups() {
      axios.get(API_URL + '/group', {
        headers: {
          'Authorization': 'Bearer ' + this.token
        }
      })
          .then(res => {
            this.Groups = res.data
          })
    }
  ,
    async registerMeeting(){
       axios.post(API_URL + '/meeting', this.meeting, {
        headers: {
            'Authorization': 'Bearer '+ this.token
        }
      })
      .then(() => this.raiseMeetingRegisteredEvent())
    },
    raiseMeetingRegisteredEvent(){
      this.meeting = {
        title: '',
        description: '',
        dates: [],
        members: [],
      }
      this.$emit('registeredEvent')
    }
  }
}
</script>