import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/private/HomeView.vue'
import GroupsView from "@/views/private/GroupsView";
import MeetingsView from "@/views/private/MeetingsView";
import AccountView from "@/views/private/AccountView";
import LoginView from "@/views/public/LoginView";
import RegisterView from "@/views/public/RegisterView";
import LandingView from "@/views/public/LandingView";

Vue.use(VueRouter)

const routes = [
  {
    path: '/home',
    name: 'Home',
    component: Home,
    // beforeEnter: (to, from, next) => {
    //   if(store.state.user == null) next({name: 'Login'})
    //   else next()
    // },
    children: [
      {
        path: 'groups',
        name: 'groups',
        component: GroupsView,
      },
      {
        path: 'meetings',
        name: 'meetings',
        component: MeetingsView,
      },
      {
        path: 'account',
        name: 'account',
        component: AccountView,
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

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
