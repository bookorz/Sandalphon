import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

// TYPES
const MAIN_SET_COUNTER = 'MAIN_SET_COUNTER'
const MAIN_SET_FORM = 'MAIN_SET_FORM'
const MAIN_SET_ISLOGIN = 'MAIN_SET_ISLOGIN'
// STATE
const state = {
  counter: 1,
  isLogin: false,
  form: JSON.parse(window.localStorage.form ||
    JSON.stringify({
      ELPT: null,
      ILPT: null,
      Source: null,
      Destination: null,
      WithMapping: true,
      Path: '1',
      WHR_Access_ILPT: null,
      Position: '0',
      Direction: '0',
      align_angle: '',
      language: 'eng',
      foup_robot_speed: '',
      whr_speed: '',
      ctu_speed: '',
      ptz_speed: '',
      elpt_speed: '',
      aligner_speed: '',
      light_curtain: false
    }))
}
const getters = {
  form_get: state => state.form,
  isLogin_get: state => state.isLogin
}
// MUTATIONS
const mutations = {
  [MAIN_SET_COUNTER] (state, obj) {
    state.counter = obj.counter
  },
  [MAIN_SET_FORM] (form, obj) {
    state.form = obj.form
    // window.localStorage.setItem('form', JSON.stringify(obj.form))
  },
  [MAIN_SET_ISLOGIN] (form, obj) {
    state.isLogin = obj.isLogin
    // window.localStorage.setItem('form', JSON.stringify(obj.form))
  }
}
// 使用 Vuex 中的 plugins 來與 localStorage 結合將狀態保存在瀏覽器
const localStoragePlugin = store => {
  store.subscribe((mutation, { form }) => {
    // 當執行 MAIN_SET_FORM 時才執行以下程式碼
    if (mutation.type === MAIN_SET_FORM) {
      window.localStorage.setItem('form', JSON.stringify(form))
    }
  })
}

// ACTIONS
const actions = ({
  setCounter ({ commit }, obj) {
    commit(MAIN_SET_COUNTER, obj)
  },
  setForm ({ commit }, obj) {
    commit(MAIN_SET_FORM, obj)
  },
  setIsLogin ({ commit }, obj) {
    commit(MAIN_SET_ISLOGIN, obj)
  }
})

export default new Vuex.Store({
  state,
  mutations,
  actions,
  getters,
  plugins: [localStoragePlugin]
})
