import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '@/views/Login/Login.vue'
import Reg from '@/views/Reg/Reg.vue'
import Main from '@/views/Main/Main.vue'
import Home from '@/views/Menus/Home/Home.vue'
import UserInfo from '@/views/Menus/User/UserInfo.vue'
import ArtCate from '@/views/Menus/Article/ArtCate.vue'
import ArtList from '@/views/Menus/Article/ArtList.vue'
import Release from '@/views/Release/Release.vue'
import BlogArtile from '@/views/BlogArtile/BlogArtile.vue'
import NotFound from '@/views/NotFound/NotFound.vue'
import CmsMain from '@/views/CmsMain/CmsMain.vue'
import PersonalCenter from '@/views/PersonalCenter/PersonalCenter.vue'
import Users from '@/views/Users/Users.vue'

Vue.use(VueRouter)
const originalPush = VueRouter.prototype.push
VueRouter.prototype.push = function push(location) {
  return originalPush.call(this, location).catch((err) => err)
}

const routes = [
  {
    path: '/cmsmain',
    name: 'CmsMain',
    component: CmsMain,
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
  },
  {
    path: '/reg',
    name: 'Reg',
    component: Reg,
  },
  {
    path: '/article',
    name: 'Article',
    component: BlogArtile,
  },
  {
    path: '/release',
    name: 'Release',
    component: Release,
  },
  {
    path: '/userInfo',
    name: 'UserInfo',
    component: UserInfo,
  },
  {
    path: '/personalCenter',
    name: 'PersonalCenter',
    component: PersonalCenter,
  },
  {
    path: '/',
    name: 'Main',
    component: Main,
    redirect: '/cmsmain',
    children: [
      {
        path: 'home',
        name: 'Home',
        component: Home,
      },
      {
        path: '/users',
        name: 'users',
        component: Users,
      },
      {
        path: 'user-info',
        name: 'UserInfo',
        component: UserInfo,
      },
      {
        path: 'art-cate',
        name: 'ArtCate',
        component: ArtCate,
      },
      {
        path: 'art-list',
        name: 'ArtList',
        component: ArtList,
      },
    ],
  },
  {
    path: '*',
    name: 'NotFound',
    component: NotFound,
  },
]

const router = new VueRouter({
  routes,
  // mode: 'history',
})

export default router
