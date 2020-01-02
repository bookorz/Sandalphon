import Vue from 'vue'
import VueRouter from 'vue-router'
import ManualControl from 'components/manual-control'
import AlarmHistory from 'components/alarm-history'
// import HomePage from 'components/home-page'
import About from 'components/about'
import Login from 'components/login'
import ioList from 'components/io-list'

Vue.use(VueRouter)

let routes = [
  { name: 'login', path: '/login', component: Login },
  { name: 'home', path: '/', component: About },
  { name: 'about', path: '/about', component: About },
  {
    name: 'manual-control',
    path: '/manual-control',
    component: ManualControl,
    meta: {
      requiresAuth: true
    }
  },
  { name: 'io-list', path: '/io-list', component: ioList },
  { name: 'alarm-history', path: '/alarm-history', component: AlarmHistory }
]

let router = new VueRouter({
  mode: 'history',
  routes
})

router.beforeEach((to, from, next) => {
  // matched的陣列中包含$route物件的檢查元欄位
  // arr.some() 表示判斷該陣列是否有元素符合相應的條件, 返回布林值
  if (to.matched.some(record => record.meta.requiresAuth)) {
    // 判斷當前是否有登入的許可權

    if (!router.app.$options.store.getters.isLogin_get) {
      next({
        path: '/login',
        query: { redirect: to.fullPath }
      })
    } else {
      next()
    }
  } else {
    next() // 確保一定要呼叫 next()
  }
})

export default router
