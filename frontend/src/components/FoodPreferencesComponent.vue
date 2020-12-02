<template>
  <v-card height="400px">
    <v-container>
      <v-btn @click="savePreferences()">Save</v-btn>
      <v-row justify="center">
        <v-col cols="6">
            <v-simple-table height="300px" dense>
              <tbody>
              <tr v-for="(cuisine, index) in cuisines" :key="index">
                <td>{{cuisine}}</td>
                <td>
                  <v-checkbox dense v-model="selectedCuisines" :value="cuisine"></v-checkbox>
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
export default {
  name: "FoodPreferencesComponent",
  created() {
    this.token =  this.$store.state.accessToken
    this.user = this.$store.state.user
    },
  data: function () {
    return {
      cuisines: [
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
      selectedCuisines: user.MealPreference.Cousines,
      selectedAllergies: user.MealPreference.Allergens
    }
  },
  methods: {
    savePreferences(){
        user.MealPreference.Cousines = this.selectedCuisines
        user.MealPreference.Allergens = this.Allergens
        axios.put(API_URL + '/user', user, {
        headers: {
            'Authorization': 'Bearer '+ this.token
        }})
    }
  }
}
</script>