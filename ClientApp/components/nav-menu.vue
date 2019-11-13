<template>
  <div class="main-nav">
    <nav class="navbar navbar-expand-md navbar-dark">
      <button class="navbar-toggler" type="button" @click="toggleCollapsed">
        <span class="navbar-toggler-icon"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </button>

      <router-link class="navbar-brand" to="/"><icon :icon="['fab', 'vuejs']" /> Wafer Transfer System</router-link>

      <transition name="slide">
        <div :class="'collapse navbar-collapse' + (!collapsed ? ' show':'')" v-show="!collapsed">
          <ul class="navbar-nav">
            <li class="nav-item">
              <router-link to="/" exact-active-class="active">
                <icon icon="desktop" class="mr-2 menu-icon" /><span>{{$t('__nav_dashboard')}}</span>
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/about" exact-active-class="active">
                <icon icon="info" class="mr-2 menu-icon" /><span>{{$t('__nav_showlog')}}</span>
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/manual-control" exact-active-class="active">
                <icon icon="user-cog" class="mr-2 menu-icon" /><span>{{$t('__nav_manualcontrol')}}</span>
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/fetch-data" exact-active-class="active">
                <icon icon="list" class="mr-2 menu-icon" /><span>{{$t('__nav_data')}}</span>
              </router-link>
            </li>
            <li class="nav-item sidebar_button">
              <b-form-group style="color:white" label="Language">

                <b-form-select v-model="form.language" :options="lang_options" @change="switchLang(form.language)"></b-form-select>
              </b-form-group>
            </li>

          </ul>
        </div>
      </transition>
    </nav>
  </div>
</template>

<script>
  import { mapActions, mapState } from 'vuex'
  import ManualControl from 'components/manual-control'
  import FetchData from 'components/fetch-data'
  import HomePage from 'components/home-page'
  import About from 'components/about'
  export default {
    data() {
      return {
        collapsed: true,
        lang_options: [
          { value: 'eng', text: 'English' },
          { value: 'cht', text: '繁體中文' },
          { value: 'chs', text: '简体中文' }
        ]
      }
    },
    computed: {
      ...mapState({
        form: state => state.form
      })
    },
    methods: {
      toggleCollapsed: function (event) {
        this.collapsed = !this.collapsed
      },
      switchLang(newLang) {
        this.$root.$i18n.locale = newLang
      }
    }
  }
</script>

<style scoped>

  .slide-enter-active, .slide-leave-active {
    transition: max-height .35s
  }

  .slide-enter, .slide-leave-to {
    max-height: 0px;
  }

  .slide-enter-to, .slide-leave {
    max-height: 20em;
  }

  @media (min-width: 768px) {
    .sidebar_button {
      position: absolute;
      bottom: 0;
      width: 150px;
    }
  }
</style>
