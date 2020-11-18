<template>
  <v-card>
    <p>Date preferences</p>
              <v-btn
              color="green darken-1"
              text
              @click="logPick()"
          >
            Pick date
          </v-btn>
    <v-row>
      <v-col>
        <v-sheet height="500" width="800">
          <v-calendar
            :now="today"
            :value="today"
            color="primary"
            @mouseenter="dosth()"
          >
            <v-date-picker v-model="date" />
            <template v-slot:day="{ past, date }">
              <v-row
                class="fill-height"
              >
                <template v-if="past && tracked[date]">
                  <v-sheet
                    v-for="(percent, i) in tracked[date]"
                    :key="i"
                    :title="category[i]"
                    :color="colors[i]"
                    :width="`${percent}%`"
                    height="100%"
                    tile
                  ></v-sheet>
                </template>
              </v-row>
            </template>
          </v-calendar>
        </v-sheet>
      </v-col>
    </v-row>
  </v-card>
  


</template>

<script>
  export default {
    name: "CalendarPreferencesComponent",
    data: () => ({
      today: new Date().Date,
      date: null,
      tracked: {},
      colors: ['#1867c0', '#fb8c00', '#000000'],
      category: ['Good', 'Maybe', 'Bad'],
    }),
    methods: {
      dosth() {
        console.log("hello")
      },
      logPick() {
        console.log(this.date)
      }
    }
  }
</script>
