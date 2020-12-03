<template>
  <v-content>
    <v-row justify="center">
      <v-col cols="6">
        <FoodPreferencesComponent/>
      </v-col>
      <v-col cols="6">
<!--        <MoviePreferencesComponent/>-->
      </v-col>
    </v-row>
    <v-row justify="center">
      <CalendarPreferencesComponent/>
    </v-row>
  </v-content>
</template>

<script>
import CalendarPreferencesComponent from "@/components/CalendarPreferencesComponent";
import FoodPreferencesComponent from "@/components/FoodPreferencesComponent";
export default {
name: "AccountView",
  components: {FoodPreferencesComponent, CalendarPreferencesComponent}
}
</script>