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
        <v-btn @click="setToday()">Today</v-btn>
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
    <v-dialog v-model="inboxDialog" max-width="400pt">
      <v-card class="px-5">
        <v-card-title>
          Group Invites
        </v-card-title>
        <v-list dense>
          <template v-for="invite in invites">
            <v-list-item v-bind:key="invite.sender">
              <v-list-item-icon><v-icon>fas fa-user-plus</v-icon></v-list-item-icon>
              <v-list-item-content>
                User {{invite.senderName}} invited you to group: {{invite.groupName}}!
              </v-list-item-content>
              <v-btn icon ><v-icon>fas fa-check</v-icon></v-btn>
              <v-btn icon ><v-icon>fas fa-times</v-icon></v-btn>
            </v-list-item>
          </template>
        </v-list>
        <v-card-actions>
          <v-btn @click="closeInboxDialog()">Close</v-btn>
          <v-spacer/>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="createDialog" max-width="400pt">
      <CreateMeetingComponent @closeEvent="closeCreateDialog()" @registeredEvent="fetchMeetings(); closeCreateDialog()"/>
    </v-dialog>
    <v-dialog v-model="detailsDialog" max-width="400pt">
      <MeetingDetailsComponent :meeting="selectedEvent" @closeEvent="closeDetailsDialog"/>
    </v-dialog>
    <v-btn
        fab
        color="primary"
        top
        right
        absolute
        style="margin-top: 20pt"
        @click="showInboxDialog()"
    >
      <v-icon>mdi-message</v-icon>
    </v-btn>
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
    this.fetchInvites()
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
    invites: [
    ],
    meetings: []
  }),
  methods: {
    async fetchMeetings(){
      axios.get(API_URL + '/meeting', {
       headers: {
            'Authorization': 'Bearer '+ this.token
        } 
      })
        .then(res => this.meetings = res.data)
    },
    async fetchInvites(){
      axios.get(API_URL + '/invitation/' + this.user.id, {
        headers: {
            'Authorization': 'Bearer '+ this.token
        }
      })
        .then(res => this.invites = res.data)
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
      .then(() => {
          this.invites.pop(invite)
          this.updateUser()
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
      .then(() => {
          this.invites.pop(invite)
          this.updateUser()
        })
    },
    async updateUser(){
      axios.get(API_URL + '/user/' + this.user.id, {
        headers: {
            'Authorization': 'Bearer '+ this.token
        }
      })
      .then(res => {
        this.user = res.data
        this.$store.state.user = res.data
        this.fetchMeetings()
        })
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
/*
1. Okno meetingu (in progress)
  - podanie preferencji żywieniowych
  - podanie preferencji filmowych
  - wypisanie
  - klepnięcie (tylko organizator)
2. Okno meetingu (read only)

1. Skrzynka zaproszeń analogicznie do grup
 */
</script>
