<template>
  <v-main>
    <v-container>
      <v-row>
        <v-btn
            icon
            class="ma-2"
            @click="$refs.calendar.prev()"
        >
          <v-icon>mdi-chevron-left</v-icon>
        </v-btn>
        <v-btn text color="primary" @click="setToday()">Today</v-btn>
        <v-btn
            icon
            class="ma-2"
            @click="$refs.calendar.next()"
        >
          <v-icon>mdi-chevron-right</v-icon>
        </v-btn>
      </v-row>
      <v-sheet height="600">
        <v-calendar ref="calendar" v-model="focus" type="month" :events="meetings" @click:event="showDetailsDialog"/>
      </v-sheet>
      <v-row class="mx-3" justify="end">
        <v-btn @click="showCreateDialog()">Create meeting</v-btn>
      </v-row>
    </v-container>
    <v-dialog v-model="createDialog" max-width="400pt">
      <CreateMeetingComponent @closeEvent="closeCreateDialog()" @registeredEvent="fetchMeetings(); closeCreateDialog()"/>
    </v-dialog>
    <v-dialog v-model="detailsDialog" max-width="800pt">
      <MeetingDetailsComponent :meeting="selectedEvent" @closeEvent="closeDetailsDialog"/>
    </v-dialog>
  </v-main>
</template>

<script>
import axios from "axios";
import {API_URL} from "@/config/consts";
import CreateMeetingComponent from "@/components/CreateMeetingComponent";
import MeetingDetailsComponent from "@/components/MeetingDetailsComponent";

export default {
  name: "MeetingsView",
  components: {MeetingDetailsComponent, CreateMeetingComponent},
  created() {
    this.token = this.$store.state.accessToken
    this.user = this.$store.state.user
    this.fetchMeetings()
  },
  data: () => ({
    focus: '',
    inboxDialog: false,
    createDialog: false,
    detailsDialog: false,
    selectedEvent: {
      name: '',
      start: new Date()
    },
    meetings: []
  }),
  methods: {
    async fetchMeetings(){
      axios.get(API_URL + '/meeting', {
       headers: {
            'Authorization': 'Bearer '+ this.token
        } 
      })
        .then(res => this.meetings = this.formatMeetings(res.data))
    },
    setToday() {
      this.focus = ''
    },
    acceptInvite(invite){
      let command = {
        invitationId: invite.id,
        decision: true
      }
      axios.put(API_URL + '/invitation', command, {
        headers: {
            'Authorization': 'Bearer '+ this.token
        }
      })
    },
    denyInvite(invite){
      let command = {
        invitationId: invite.id,
        decision: false
      }
      axios.put(API_URL + '/invitation', command, {
        headers: {
            'Authorization': 'Bearer '+ this.token
        }
      })
    },
    formatMeetings(meetings){
      let formatted =  meetings.map(meeting => this.mapToEvent(meeting))
      return formatted
    },
    mapToEvent(meeting){
      if(meeting.dateProposition.includes('T')){
        meeting.start = meeting.dateProposition.split('T')[0]
      }
      meeting.name = meeting.title
      return meeting
    },
    showInboxDialog(){
      this.inboxDialog = true
    },
    closeInboxDialog(){
      this.inboxDialog = false
    },
    showDetailsDialog({event}){
      this.selectedEvent = event
      this.detailsDialog = true
    },
    closeDetailsDialog(){
      this.detailsDialog = false
    },
    showCreateDialog(){
      this.createDialog = true
    },
    closeCreateDialog(){
      this.createDialog = false
    },
  }
}
</script>
