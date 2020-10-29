<template>
  <div class="fill-height">
    <v-app-bar flat dense color="primary">
      <v-spacer/>
      <v-btn class="ml-5; mr-5" text to="/login">Login</v-btn>
      <v-btn class="ml-5; mr-5" text to="/register">Register</v-btn>
      <v-spacer/>
    </v-app-bar>
    <v-content>
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
    </v-content>
    <v-footer absolute>
      <v-spacer/>
      Some footer information
      <v-spacer/>
    </v-footer>
  </div>
</template>

<script>
export default {
name: "LandingView",
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