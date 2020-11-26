<template>
  <v-card class="px-5">
    <v-container>
      <v-card-title>
        Create meeting
      </v-card-title>
      <v-row justify="center">
        <v-text-field label="Meeting title" v-model="meeting.Name"/>
      </v-row>
      <v-row justify="center">
        <v-text-field label="Meeting description" v-model="meeting.Description"/>
      </v-row>
      <v-row justify="center">
        <v-menu ref="menu" v-model="menu" :close-on-content-click="false" :return-value.sync="meeting.DatePropositions" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-combobox v-model="meeting.DatePropositions" multiple chips small-chips label="Choose DatePropositions" readonly v-bind="attrs" v-on="on"/>
          </template>
          <v-date-picker v-model="meeting.DatePropositions" multiple no-title scrollable>
            <v-spacer/>
            <v-btn text color="primary" @click="menu = false">
              Cancel
            </v-btn>
            <v-btn text color="primary" @click="$refs.menu.save(meeting.DatePropositions)">
              Ok
            </v-btn>
          </v-date-picker>
        </v-menu>
      </v-row>
      <v-row justify="center">
        <v-autocomplete label="Invite guests" chips deletable-chips multiple v-model="meeting.Members" :items="users" item-text="name" item-value="id"/>
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
    this.fetchUsers()
  },
  data: function () {
    return {
      menu: false,
      users: [],
      groups: [],
      meeting: {
        Name: '',
        Description: '',
        DatePropositions: [],
        Members: [],
        Organizer: this.$store.state.user.name
      }
    }
  },
  methods: {
    raiseCloseEvent() {
      this.meeting = {
        Name: '',
        Description: '',
        DatePropositions: [],
        Members: [],
        Organizer: this.$store.state.user.name
      }
      this.$emit('closeEvent')
    },
    async fetchUsers(){
      axios.create({
        headers: {
          'Authorization': 'Bearer ' + this.token
        }
      }).get(API_URL + '/user/names')
          .then(res => this.users = res.data)
    },
    async fetchGroups() {
      axios.create({
        headers: {
          'Authorization': 'Bearer ' + this.token
        }
      })
          .get(API_URL + '/group')
          .then(res => this.groups = res.data)
    },
    async registerMeeting(){
      axios.post(API_URL + '/meeting', this.meeting, {})
        .then(() => this.raiseMeetingRegisteredEvent())
    },
    raiseMeetingRegisteredEvent(){
      this.meeting = {
        Name: '',
        Description: '',
        DatePropositions: [],
        Members: [],
        Organizer: this.$store.state.user.name
      }
      this.$emit('registeredEvent')
    }
  }
}
</script>