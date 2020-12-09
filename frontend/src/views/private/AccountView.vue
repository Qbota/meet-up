<template>
  <v-main>
    <v-row justify="center">
      <v-col cols="8">
        <FoodPreferencesComponent/>
      </v-col>
      <v-col cols="4">
        <MovieGenresComponent/>
      </v-col>
    </v-row>
    <v-row justify="center">
      <v-col cols="6">
        <CalendarPreferencesComponent/>
      </v-col>
      <v-col cols="6">
        <MovieRatingsComponent/>
      </v-col>
    </v-row>
  </v-main>
</template>

<script>
import CalendarPreferencesComponent from "@/components/CalendarPreferencesComponent";
import FoodPreferencesComponent from "@/components/FoodPreferencesComponent";
import MovieGenresComponent from "@/components/MovieGenresComponent";
import MovieRatingsComponent from "@/components/MovieRatingsComponent";

export default {
name: "AccountView",
  components: {MovieRatingsComponent, MovieGenresComponent, FoodPreferencesComponent, CalendarPreferencesComponent}
}
</script>