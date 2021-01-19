<template>
  <v-main>
    <v-navigation-drawer
        expand-on-hover
        permanent
        app
        color="primary"
    >
      <v-list nav dense>
        <v-list-item id="groupsButton" to="/home/groups">
          <v-list-item-icon>
            <v-icon>fas fa-user-friends</v-icon>
          </v-list-item-icon>
          <v-list-item-title>Groups</v-list-item-title>
        </v-list-item>
        <v-list-item id="300" to="/home/meetings">
          <v-list-item-icon>
            <v-icon>fas fa-handshake</v-icon>
          </v-list-item-icon>
          <v-list-item-title>Meetings</v-list-item-title>
        </v-list-item>
        <v-list-item id="299" to="/home/account">
          <v-list-item-icon>
            <v-icon>fas fa-user-edit</v-icon>
          </v-list-item-icon>
          <v-list-item-title>Preferences</v-list-item-title>
        </v-list-item>
        <v-list-item @click="logout()">
          <v-list-item-icon>
            <v-icon>fas fa-sign-out-alt</v-icon>
          </v-list-item-icon>
          <v-list-item-title>Logout</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-container>
      <router-view/>
    </v-container>
  </v-main>
</template>

<script>

export default {
  name: 'Home',
  methods: {
    logout() {
      this.$store.state.user = null
      this.$store.state.refreshToken = ''
      this.$store.state.accessToken = ''
      this.$router.push('/')
    }
  }
}
</script>