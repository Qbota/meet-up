<template>
  <v-main>
    <v-container>
      <v-row justify="center">
        <template v-for="i in range">
          <v-card v-bind:key="i" class="pl-3 pr-3 ms-5 mb-10 d-flex flex-column" height="200pt" width="150pt">
            <v-card-title>
              Group Name
            </v-card-title>
            <v-card-subtitle>
              Group additional info
            </v-card-subtitle>
            <v-row justify="center">
              <v-icon x-large class="mt-5 mb-5">
                fas fa-baseball-ball
              </v-icon>
            </v-row>
            <v-card-actions class="mt-6">
              <v-spacer/>
              <v-btn color="primary">Info</v-btn>
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
      >
        <v-icon>mdi-message</v-icon>
      </v-btn>
    </v-container>
    <v-dialog v-model="createDialog" max-width="400pt">
      <v-card class="px-5">
        <v-card-title>
          Dynamic group name here
        </v-card-title>
        <v-card-subtitle>
          Dynamic group description here
        </v-card-subtitle>
        <v-row justify="center">
          Icon picker to add
        </v-row>
        <v-row justify="center">
          <v-col cols="6">
            <v-text-field label="Group name"></v-text-field>
          </v-col>
          <v-col cols="6">
            <v-text-field label="Group Description"></v-text-field>
          </v-col>
        </v-row>
        <v-row justify="center">
          <v-col cols="12">
            <v-autocomplete></v-autocomplete>
            <v-list>
              <v-list-item>
                Member
              </v-list-item>to <v-list-item>
                Member
              </v-list-item>
            </v-list>
          </v-col>
        </v-row>
        <v-card-actions>
          <v-btn @click="closeCreateDialog()">Close</v-btn>
          <v-spacer/>
          <v-btn>Create</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-main>
</template>

<script>
export default {
  name: "GroupSelectionView",
  data: () => ({
    range: [1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1],
    createDialog: false,
    infoDialog: false,
    inboxDialog: false
  }),
  methods: {
    showCreatDialog(){
      this.createDialog = true
    },
    showInfoDialog(){
      this.infoDialog = true
    },
    closeCreateDialog(){
      this.createDialog = false
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