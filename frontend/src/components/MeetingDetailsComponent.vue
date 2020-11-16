<template>
  <v-card>
    <v-card-title>
      {{meeting.name}}
    </v-card-title>
    <v-btn @click="raiseCloseEvent">Close</v-btn>
  </v-card>
</template>

<script>
export default {
name: "MeetingDetailsComponent",
  props: {
    meeting: Object
  },
  methods: {
    raiseCloseEvent(){
      this.$emit('closeEvent')
    }
  }
}
</script>