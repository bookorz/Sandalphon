import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

// TYPES
const MAIN_SET_COUNTER = 'MAIN_SET_COUNTER'
const MAIN_SET_FORM = 'MAIN_SET_FORM'

// STATE
const state = {
  counter: 1,
  form: {
    ELPT: null,
    ILPT: null,
    Source: null,
    Destination: null,
    WithMapping: true,
    Path: '1',
    WHR_Access_ILPT: null,
    Position: '0',
    Direction: '0'
  }
}

// MUTATIONS
const mutations = {
  [MAIN_SET_COUNTER] (state, obj) {
    state.counter = obj.counter
  },
  [MAIN_SET_FORM] (form, obj) {
    state.form = obj.form
  }
}

// ACTIONS
const actions = ({
  setCounter ({ commit }, obj) {
    commit(MAIN_SET_COUNTER, obj)
  },
  setForm ({ commit }, obj) {
    commit(MAIN_SET_FORM, obj)
  }
})

export default new Vuex.Store({
  state,
  mutations,
  actions
})
