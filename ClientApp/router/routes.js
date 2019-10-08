import ManualControl from 'components/manual-control'
import FetchData from 'components/fetch-data'
import HomePage from 'components/home-page'
import About from 'components/about'

export const routes = [
  { name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home' },
  { name: 'about', path: '/about', component: About, display: 'About Template', icon: 'info' },
  { name: 'manual-control', path: '/manual-control', component: ManualControl, display: 'Manual Control', icon: 'user-cog' },
  { name: 'fetch-data', path: '/fetch-data', component: FetchData, display: 'Data', icon: 'list' }
]
