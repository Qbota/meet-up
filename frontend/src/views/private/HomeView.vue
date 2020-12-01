<template>
  <v-main>
    <v-navigation-drawer
        expand-on-hover
<<<<<<< HEAD
        absolute
        permanent
=======
        permanent
        app
>>>>>>> 623f8a4ba0c404dc30aa3d61079037349db856e7
        color="primary"
    >
      <v-list nav dense>
        <v-list-item to="/home/groups">
          <v-list-item-icon>
            <v-icon>mdi-folder</v-icon>
          </v-list-item-icon>
          <v-list-item-title>Groups</v-list-item-title>
        </v-list-item>
        <v-list-item to="/home/meetings">
          <v-list-item-icon>
            <v-icon>mdi-folder</v-icon>
          </v-list-item-icon>
          <v-list-item-title>Meetings</v-list-item-title>
        </v-list-item>
        <v-list-item to="/home/account">
          <v-list-item-icon>
            <v-icon>mdi-account</v-icon>
          </v-list-item-icon>
          <v-list-item-title>Account</v-list-item-title>
        </v-list-item>
        <v-list-item @click="logout()">
          <v-list-item-icon>
            <v-icon>mdi-logout</v-icon>
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