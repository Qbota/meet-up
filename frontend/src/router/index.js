import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/private/HomeView.vue'
import GroupsView from "@/views/private/GroupsView";
import MeetingsView from "@/views/private/MeetingsView";
import AccountView from "@/views/private/AccountView";
import LoginView from "@/views/public/LoginView";
import RegisterView from "@/views/public/RegisterView";
import LandingView from "@/views/public/LandingView";
import axios from "axios";
import {API_URL} from "@/config/consts";
import store from '@/store/index'

Vue.use(VueRouter)



const routes = [
  {
    path: '/home',
    name: 'Home',
    component: Home,
    beforeEnter: (to, from, next) => refreshToken(to, from, next),
    children: [
      {
        path: 'groups',
        name: 'groups',
        component: GroupsView,
        beforeEnter: (to, from, next) => refreshToken(to, from, next)
      },
      {
        path: 'meetings',
        name: 'meetings',
        component: MeetingsView,
        beforeEnter: (to, from, next) => refreshToken(to, from, next)
      },
      {
        path: 'account',
        name: 'account',
        component: AccountView,
        beforeEnter: (to, from, next) => refreshToken(to, from, next)
      }
    ]
  },
  {
    path: '/login',
    name: 'Login',
    component: LoginView,
  },
  {
    path: '/register',
    name: 'Register',
    component: RegisterView,
  },
  {
    path: '/',
    name: 'Landing',
    component: LandingView
  }
]

function refreshToken(to, from, next){
  console.log('refreshing token')
  const token = store.state.accessToken
  const refreshToken = store.state.refreshToken
  if(token != null && refreshToken != null)
  {
    let refreshCommand =  {
      accessToken: token,
      refreshToken: refreshToken
    };
    axios.post(API_URL + '/refresh', refreshCommand, {})
        .then(res => {
          store.state.accessToken = res.data.accessToken
          store.state.refreshToken = res.data.refreshToken.tokenString
        })
        .catch(() => {
          //localStorage.clear();
          //log user out
        })
  }
  next()
}

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
