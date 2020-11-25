import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify';
import {API_URL} from "@/config/consts";
import axios from 'axios';

//import {makeServer} from './mock/mockServer'

/*if (process.env.NODE_ENV === 'development') {
  makeServer()
}*/

router.beforeEach((to, from, next) => {
  var token = localStorage.getItem("token");
  var refreshToken = localStorage.getItem("refreshToken");
  if(token != null && refreshToken != null)
  {
    let refreshCommand =  {
      accessToken: token,
      refreshToken: refreshToken
    };
    axios.post(API_URL + '/refresh', refreshCommand, {})
            .then(res => {
              localStorage.setItem('token',res.data.accessToken)
              localStorage.setItem('refreshToken',res.data.refreshToken.tokenString);
            })
            .catch(() => {
              //localStorage.clear();
              //log user out
            })
  }
  next()
  }
)

Vue.config.productionTip = false

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
