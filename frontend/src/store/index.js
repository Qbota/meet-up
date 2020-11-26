import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    user: null,
    accessToken: '',
    refreshToken: ''
  },
  mutations: {
    setUser(state, user){
      state.user = user
    },
    setAccessToken(state, accessToken){
      state.accessToken = accessToken
    },
    setRefreshToken(state, refreshToken){
      state.refreshToken = refreshToken
    }
  },
  actions: {
  },
  modules: {
  }
})
