<template>
  <div class="fill-height">
      <PublicNavbarComponent/>
    <v-main>
      <v-row justify="center" align="center">
        <v-col cols="8">
          <v-carousel v-model="carousel" hide-delimiters>
            <v-carousel-item>
              <v-row justify="center">
                <v-img max-width="500pt" src="../../assets/logo.jpg"/>
              </v-row>
            </v-carousel-item>
            <v-carousel-item>
              <v-img src="../../assets/meeting.png"/>
            </v-carousel-item>
            <v-carousel-item>
              <v-img src="../../assets/groups.png"/>
            </v-carousel-item>
            <v-carousel-item>
              <v-img src="../../assets/meetings.png"/>
            </v-carousel-item>
            <v-carousel-item>
              <v-img src="../../assets/preferences.png"/>
            </v-carousel-item>
          </v-carousel>
        </v-col>
      </v-row>
    </v-main>
  </div>
</template>

<script>
import PublicNavbarComponent from "@/components/PublicNavbarComponent";
export default {
name: "LandingView",
  components: {PublicNavbarComponent},
  data: () => ({
    carousel: 0
  })
}
</script>