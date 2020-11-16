<template>
  <v-card class="px-5">
    <v-container>
      <v-card-title>
        Create group
      </v-card-title>
      <v-row justify="center">
        <v-text-field label="Meeting title" v-model="meeting.title"/>
      </v-row>
      <v-row justify="center">
        <v-text-field label="Meeting description" v-model="meeting.description"/>
      </v-row>
      <v-row justify="center">
        <v-menu ref="menu" v-model="menu" :close-on-content-click="false" :return-value.sync="meeting.dates" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-combobox v-model="meeting.dates" multiple chips small-chips label="Choose dates" readonly v-bind="attrs" v-on="on"/>
          </template>
          <v-date-picker v-model="meeting.dates" multiple no-title scrollable>
            <v-spacer/>
            <v-btn text color="primary" @click="menu = false">
              Cancel
            </v-btn>
            <v-btn text color="primary" @click="$refs.menu.save(meeting.dates)">
              Ok
            </v-btn>
          </v-date-picker>
        </v-menu>
      </v-row>
      <v-row justify="center">
        <v-autocomplete label="Invite guests" chips deletable-chips multiple v-model="meeting.members" :items="users"/>
      </v-row>
      <v-card-actions>
        <v-btn @click="raiseCloseEvent">Close</v-btn>
        <v-spacer/>
        <v-btn>Create Meeting</v-btn>
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
      users: [],
      meeting: {
        title: '',
        description: '',
        dates: [],
        members: [],
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
    async fetchUsers(){
      axios.get(API_URL + '/names')
          .then(res => this.users = res.data)
    }
  }
}
</script>