<template>
  <v-main>
    <v-container>
      <v-row justify="center">
        <template v-for="group in groups">
          <v-card v-bind:key="group.name" class="pl-3 pr-3 ms-5 mb-10 d-flex flex-column" height="200pt" width="150pt">
            <v-card-title>
              {{group.name}}
            </v-card-title>
            <v-card-subtitle>
              {{group.description}}
            </v-card-subtitle>
            <v-row justify="center">
              <v-icon x-large class="mt-5 mb-5">
                {{group.icon}}
              </v-icon>
            </v-row>
            <v-card-actions class="mt-6">
              <v-spacer/>
              <v-btn @click="showInfoDialog(group)" color="primary">Info</v-btn>
            </v-card-actions>
          </v-card>
        </template>
        <v-card class="pl-3 pr-3 ms-5 mb-10 d-flex flex-column" height="200pt" width="150pt">
          <v-card-title>
            Create group
          </v-card-title>
          <v-row justify="center" align="center">
            <v-btn fab color="primary" @click="showCreatDialog()">
            <v-icon x-large>
              mdi-plus
            </v-icon>
            </v-btn>
          </v-row>
        </v-card>
      </v-row>
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
    </v-container>
    <v-dialog v-model="createDialog" max-width="400pt">
      <v-card class="px-5">
        <v-card-title>
          Create Group
        </v-card-title>
        <v-row justify="center">
          <v-col cols="6">
            <v-text-field label="Group name" v-model="createdGroup.name"></v-text-field>
          </v-col>
          <v-col cols="6">
            <v-text-field label="Group Description" v-model="createdGroup.description"></v-text-field>
          </v-col>
        </v-row>
        <v-row justify="center">
          <h2>Choose icon</h2>
        </v-row>
        <v-row class="mb-5" justify="center">
          <template v-for="icon in icons">
            <v-btn @click="createdGroup.icon = icon" :disabled="createdGroup.icon === icon" large icon v-bind:key="icon">
              <v-icon>{{icon}}</v-icon>
            </v-btn>
          </template>
        </v-row>
        <v-row justify="center">
          <h2>Choose group members</h2>
        </v-row>
        <v-row justify="center">
          <v-col cols="12">
            <v-autocomplete chips deletable-chips multiple v-model="createdGroup.members" :items="users"/>
          </v-col>
        </v-row>
        <v-card-actions>
          <v-btn @click="closeCreateDialog()">Close</v-btn>
          <v-spacer/>
          <v-btn @click="createGroup()">Create</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
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
                  User {{invite.sender}} invited you to group {{invite.groupName}}!
                </v-list-item-content>
                <v-btn icon @click="acceptInvite(invite)"><v-icon>fas fa-check</v-icon></v-btn>
                <v-btn icon @click="denyInvite(invite)"><v-icon>fas fa-times</v-icon></v-btn>
              </v-list-item>
            </template>
          </v-list>
        <v-card-actions>
          <v-btn @click="closeInboxDialog()">Close</v-btn>
          <v-spacer/>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="infoDialog" max-width="400pt">
      <v-card class="px-5">
        <v-card-title>
          {{ selectedGroup.name }}
        </v-card-title>
        <v-card-subtitle>
          {{ selectedGroup.description }}
        </v-card-subtitle>
        <v-row justify="center">
          <v-icon x-large>{{selectedGroup.icon}}</v-icon>
        </v-row>
        <v-row justify="center">
          <h2>Group members</h2>
        </v-row>
        <v-row justify="center">
          <v-list>
            <template v-for="member in selectedGroup.members">
              <v-list-item v-bind:key="member">
                {{member}}
              </v-list-item>
            </template>
          </v-list>
        </v-row>
        <v-card-actions>
          <v-btn @click="closeInfoDialog()">Close</v-btn>
          <v-spacer/>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-main>
</template>

<script>
import axios from "axios";
import {API_URL} from "@/config/consts";

export default {
  name: "GroupSelectionView",
  created() {
    this.fetchGroups()
    this.fetchInvites()
    this.fetchNames()
  },
  data: () => ({
    groups: [],
    users: [],
    icons: [
        'fas fa-bicycle',
        'fas fa-baseball-ball',
        'fas fa-bone',
        'fas fa-bolt',
        'fas fa-dollar-sign'
    ],
    createdGroup: {
      name: '',
      description: '',
      icon: '',
      members: []
    },
    selectedGroup: {
      name: '',
      description: '',
      icon: '',
      members: []
    },
    invites: [],
    createDialog: false,
    infoDialog: false,
    inboxDialog: false
  }),
  methods: {
    showCreatDialog(){
      this.createDialog = true
    },
    showInfoDialog(group){
      this.selectedGroup = group
      this.infoDialog = true
    },
    closeCreateDialog(){
      this.createdGroup = {
        name: '',
        description: '',
        icon: '',
        members: []
      }
      this.createDialog = false
    },
    fetchGroups(){
      axios.get( API_URL + '/group')
        .then(res => this.groups = res.data)
    },
    createGroup(){
      axios.post(API_URL + '/group', this.createdGroup,{})
        .then(res => {
          console.log(res.data.createdGroup)
          this.groups.push(res.data.createdGroup)
        })
      this.closeCreateDialog()
    },
    acceptInvite(invite){
      invite.accepted = true
      axios.post(API_URL + '/invites', invite, {})
    },
    denyInvite(invite){
      invite.accepted = false
      axios.post(API_URL + '/invites', invite, {})
    },
    fetchInvites(){
      axios.get(API_URL + '/invites')
        .then(res => this.invites = res.data)
    },
    fetchNames(){
      axios.get(API_URL + '/names')
        .then(res => this.users = res.data)
    },
    closeInfoDialog(){
      this.infoDialog = false
    },
    showInboxDialog(){
      this.inboxDialog = true
    },
    closeInboxDialog(){
      this.inboxDialog = false
    }
  }
}
</script>