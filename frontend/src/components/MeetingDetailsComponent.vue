<template>
  <v-card>
    <v-card-title>
      {{meeting.name}}
      {{meeting.start}}
      {{meeting.description}}
      {{meeting.members}}
    </v-card-title>
    <v-btn @click="raiseCloseEvent">Close</v-btn>
  </v-card>
</template>

<script>
export default {
name: "MeetingDetailsComponent",
  props: {
    meeting: {
      name: '',
      start: Date,
      members: []
    }
  },
  methods: {
    raiseCloseEvent(){
      this.$emit('closeEvent')
    }
  }
}
</script>