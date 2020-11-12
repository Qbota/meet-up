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
      <v-calendar ref="calendar" v-model="focus" type="month" :events="meetings">

      </v-calendar>
      </v-sheet>
    </v-container>
  </v-main>
</template>

<script>
export default {
name: "MeetingsView",
  data: () => ({
    focus: '',
    meetings: [{
      name: 'Test Meeting',
      start: new Date()
    }]
  }),
  methods: {
    setToday () {
      this.focus = ''
    }
  }
}
</script>
