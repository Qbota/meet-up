<template>
  <div class="fill-height">
      <PublicNavbarComponent/>
    <v-main>
      <v-row justify="center" align="center">
        <v-col cols="8">
          <v-carousel v-model="carousel" hide-delimiters>
            <v-carousel-item v-for="color in colors" :key="color">
              <v-sheet :color="color" height="100%" tile>
                <v-row class="fill-height" align="center" justify="center">
                  <div class="display-3">
                    Theme color: {{color}}
                  </div>
                </v-row>
              </v-sheet>
            </v-carousel-item>
          </v-carousel>
        </v-col>
      </v-row>
    </v-main>
<!--    <v-footer absolute>-->
<!--      <v-spacer/>-->
<!--      Some footer information-->
<!--      <v-spacer/>-->
<!--    </v-footer>-->
  </div>
</template>

<script>
import PublicNavbarComponent from "@/components/PublicNavbarComponent";
export default {
name: "LandingView",
  components: {PublicNavbarComponent},
  data: () => ({
    carousel: 0,
    colors: [
      'primary',
      'secondary',
      'accent',
      'error',
      'info',
      'success',
      'warning'
    ]
  })
}
</script>