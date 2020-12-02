<template>
  <v-card class="px-1">
    <v-row>
      <v-col>
        <v-btn
            icon
            class="ma-2"
            @click="$refs.calendar.prev()"
        >
          <v-icon>mdi-chevron-left</v-icon>
        </v-btn>
        <v-btn
            color="green darken-1"
            text
            @click="saveDatePreference()"
        >
          Save
        </v-btn>
        <v-btn
            icon
            class="ma-2"
            @click="$refs.calendar.next()"
        >
          <v-icon>mdi-chevron-right</v-icon>
        </v-btn>
        <v-sheet height="500" width="800">
          <v-calendar
              ref="calendar"
            v-model="today"
            color="primary"
              @click:day="changePreferenceOf"
          >
            <template v-slot:day="{ date }">
              <v-row
                justify="center"
              >
                <div v-if="pickedDates.includes(date)">
                  <v-icon>fas fa-check</v-icon>
                </div>
                <div v-else>
                  <v-icon>fas fa-times</v-icon>
                </div>
              </v-row>
            </template>
          </v-calendar>
        </v-sheet>
      </v-col>
    </v-row>
  </v-card>
  


</template>

<script>
import axios from "axios";
import {API_URL} from "@/config/consts";

  export default {
    name: "CalendarPreferencesComponent",
    created() {
    this.token =  this.$store.state.accessToken
    this.user = this.$store.state.user
    },
    data: () => ({
      today: new Date(),
      pickedDates: user.AvailableDates
    }),
    methods: {
      saveDatePreference(){
        user.AvailableDates = this.pickedDates
        axios.put(API_URL + '/user', user, {
        headers: {
            'Authorization': 'Bearer '+ this.token
        }})
      },
      changePreferenceOf(day){
        if(!this.pickedDates.includes(day.date)){
          this.pickedDates.push(day.date)
        }else{
          this.pickedDates = this.pickedDates.filter(date => date !== day.date)
        }
      }
    }
  }
</script>
