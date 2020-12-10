<template>
  <v-card height="450px">
    <v-card-title>
      Pick your food preference
    </v-card-title>
    <v-container>
      <v-btn color="primary"
             text @click="savePreferences()">Save
      </v-btn>
      <v-row justify="center">
        <v-col cols="6">
          <v-simple-table height="300px" dense>
            <thead>
            <tr>
              <th class="text-left">
                Cuisine
              </th>
              <th class="text-left">
                Do you like that?
              </th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="(cousine, index) in cousines" :key="index">
              <td>{{ cousine }}</td>
              <td>
                <v-checkbox dense v-model="selectedCousines" :value="cousine"></v-checkbox>
              </td>
            </tr>
            </tbody>
          </v-simple-table>
        </v-col>
        <v-col cols="6">
          <v-simple-table fixed-header height="300px" dense>
            <thead>
            <tr>
              <th class="text-left">
                Food
              </th>
              <th class="text-left">
                Can you eat that?
              </th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="(allergy, index) in allergies" :key="index">
              <td>{{ allergy }}</td>
              <td>
                <v-checkbox dense on-icon="fas fa-times" color="error" v-model="selectedAllergies" :value="allergy"></v-checkbox>
              </td>
            </tr>
            </tbody>
          </v-simple-table>
        </v-col>
      </v-row>
    </v-container>
  </v-card>
</template>

<script>
import axios from "axios";
import {ALLERGIES, API_URL, COUSINE_LIST} from "@/config/consts";

export default {
  name: "FoodPreferencesComponent",
  created() {
    this.token = this.$store.state.accessToken
    this.user = this.$store.state.user
    this.selectedCousines = this.user.mealPreference.cousines
    this.selectedAllergies = this.user.mealPreference.allergens
  },
  data: function () {
    return {
      cousines: COUSINE_LIST,
      allergies: ALLERGIES,
      selectedCousines: [],
      selectedAllergies: []
    }
  },
  methods: {
    savePreferences() {
      this.user.mealPreference.cousines = this.selectedCousines
      this.user.mealPreference.allergens = this.selectedAllergies
      axios.put(API_URL + '/user', this.user, {
        headers: {
          'Authorization': 'Bearer ' + this.token
        }
      })
    }
  }
}
</script>