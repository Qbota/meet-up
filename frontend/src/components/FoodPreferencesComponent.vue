<template>
  <v-card height="400px">
    <v-container>
      <v-btn @click="savePreferences()">Save</v-btn>
      <v-row justify="center">
        <v-col cols="6">
            <v-simple-table height="300px" dense>
              <tbody>
              <tr v-for="(cousine, index) in cousines" :key="index">
                <td>{{cousine}}</td>
                <td>
                  <v-checkbox dense v-model="selectedCousines" :value="cousine"></v-checkbox>
                </td>
              </tr>
              </tbody>
            </v-simple-table>
        </v-col>
        <v-col cols="6">
          <v-simple-table height="300px" dense>
            <tbody>
            <tr v-for="(allergy, index) in allergies" :key="index">
              <td>{{allergy}}</td>
              <td>
                <v-checkbox dense v-model="selectedAllergies" :value="allergy"></v-checkbox>
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
import {API_URL} from "@/config/consts";

export default {
  name: "FoodPreferencesComponent",
  created() {
    this.token =  this.$store.state.accessToken
    this.user = this.$store.state.user
    this.selectedCousines = this.user.mealPreference.cousines
    this.selectedAllergies = this.user.mealPreference.allergens
    },
  data: function () {
    return {
      cousines: [
        'Tunisian',
        'British',
        'Moroccan',
        'Canadian',
        'Vietnamese',
        'Chinese',
        'Greek',
        'French',
        'American',
        'Spanish',
        'Italian',
        'Egyptian',
        'Indian',
        'Jamaican',
        'Turkish',
        'Dutch',
        'Irish',
        'Mexican',
        'Thai',
        'Unknown',
        'Polish',
        'Japanese',
        'Kenyan',
        'Malaysian',
        'Russian'
      ],
      allergies: [
        'dairy',
        'eggs',
        'seaFood',
        'nuts',
        'soy',
        'wheat',
        'meat'
      ],
      selectedCousines: [],
      selectedAllergies: []
    }
  },
  methods: {
    savePreferences(){
        this.user.mealPreference.cousines = this.selectedCousines
        this.user.mealPreference.allergens = this.selectedAllergies
        axios.put(API_URL + '/user', this.user, {
        headers: {
            'Authorization': 'Bearer '+ this.token
        }})
    }
  }
}
</script>