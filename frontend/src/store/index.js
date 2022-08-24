import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  plugins: [createPersistedState()],
  state: {
    token: "",
    userInfo: {},
    isGaveALike: [],
    menu: []
  },
  getters: {
  },
  mutations: {
    setToken(state, newToken) {
      state.token = newToken
    },
    updataUserInfo(state, payload) {
      state.userInfo = payload
    },
    updateisGaveALike(state, isGaveALike) {
      state.isGaveALike = isGaveALike
    },
    setMenu(state,menu){
      state.menu = menu
    },
    clearMenu(state) {
      state.menu = []
      // Cookie.remove('menu')
    },
    addMenu(state, router) {
      // const menu = JSON.parse(Cookie.get('menu'))
      const menu = JSON.parse(this.Store.state.menu)
      console.log(menu);
      // state.menu = menu
      // const menuArray = []
      // menu.forEach(item => {
      //   if (item.children) {
      //     item.children = item.children.map(item => {
      //       item.component = () => import(`../views/${item.url}`)
      //       return
      //     })
      //     menuArray.push(...item.children)
      //   } else {
      //     item.component = () => import(`../views/${item.url}`)
      //     menuArray.push(item)
      //   }
      //   // console.log(menu);
      // })
      // menuArray.forEach(item => {
      //   router.addRoute('Main', item)
      // })
    }
  },
  modules: {
  }
})
