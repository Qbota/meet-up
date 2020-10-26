<template>
  <v-content>
    <v-navigation-drawer
        expand-on-hover
        absolute
    >
      <v-list
          nav
          dense
      >
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
      </v-list>
    </v-navigation-drawer>
    <v-container>
      <router-view/>
    </v-container>
  </v-content>
</template>

<script>

export default {
  name: 'Home',
}
</script>