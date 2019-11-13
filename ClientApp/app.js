import Vue from 'vue'
import axios from 'axios'
import router from './router/index'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root'
import { FontAwesomeIcon } from './icons'
import signalrHub from './signalr-hub'
import BlockUI from 'vue-blockui'
import BootstrapVue from 'bootstrap-vue'
import VueI18n from 'vue-i18n'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
// 自訂語言檔
import eng from './i18n/en-us'
import cht from './i18n/zh-tw'
Vue.use(BootstrapVue)
Vue.use(BlockUI)
Vue.use(VueI18n)
// Registration of global components
Vue.component('icon', FontAwesomeIcon)

Vue.prototype.$http = axios

sync(store, router)
Vue.use(signalrHub)

// 取得預設語系
const locale = window.localStorage.getItem('locale') || 'eng'
const i18n = new VueI18n({
  locale: locale,
  messages: { eng, cht }
})

const app = new Vue({
  i18n,
  store,
  router,
  ...App
})

export {
  app,
  router,
  store
}
