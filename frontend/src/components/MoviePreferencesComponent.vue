<template>
  <v-card>
    Add here movie preferences and action to add recently watched movies with rating
    <v-btn @click="example()">Check browser console</v-btn>
  </v-card>
</template>

<script>
export default {
name: "MoviePreferencesComponent",
  data: function (){
    return {
      //Add here properties like this
      someProp: 123,
      anotherProp: null
    }
  },
  methods: {
    //add here methods
    example(){
      console.log('hi')
    }
  }
}
</script>