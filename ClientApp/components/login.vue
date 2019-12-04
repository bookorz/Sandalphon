<template>
  <div>
    <form class="form-signin">

      <h1 class="h3 mb-3 font-weight-normal">{{$t('login.title')}}</h1>
      <label for="inputEmail" class="sr-only">{{$t('login.userPlaceHolder')}}</label>
      <input id="inputEmail" class="form-control" :placeholder="$t('login.userPlaceHolder')" v-model="user" required autofocus>
      <label for="inputPassword" class="sr-only">{{$t('login.passwordPlaceHolder')}}</label>
      <input type="password" id="inputPassword" class="form-control" :placeholder="$t('login.passwordPlaceHolder')" v-model="pwd" required>
      <!--<div class="checkbox mb-3">
      <label>
        <input type="checkbox" value="remember-me"> {{$t('login.rememberMeCheckbox')}}
      </label>
    </div>-->
      <button class="btn btn-lg btn-primary btn-block" @click="login">{{$t('login.loginBotton')}}</button>
      <p class="mt-5 mb-3 text-muted">&copy; 2019-2020</p>
    </form>
    <b-modal id="alarm_modal" hide-footer>
      <template v-slot:modal-title>
        {{$t('login.messageBoxTitle')}}
      </template>
      <div class="d-block text-center">
        {{alarm_message}}
      </div>
      <b-button class="mt-3" block @click="$bvModal.hide('alarm_modal')">Close Me</b-button>
    </b-modal>
  </div>
</template>

<script>
  import { mapActions, mapState } from 'vuex'
  export default {
    data() {
      return {
        user: '',
        pwd: '',
        alarm_message: null
      }
    },
    computed: {
      ...mapState({
        form: state => state.form,
        isLogin: state => state.isLogin
      })
    },
    methods: {
      ...mapActions(['setForm']),
      ...mapActions(['setIsLogin']),
      login() {
        if (this.user == 'sanwa' && this.pwd == '123') {
          this.setIsLogin({ isLogin: true })
          if (this.$route.query.redirect) {
            let redirect = this.$route.query.redirect
            this.$router.push(redirect)
          } else {
            this.$router.push('/')
          }
        } else {
          this.alarm_message = this.$t('login.messageBoxContent')
          this.$bvModal.show('alarm_modal')
        }
      }
    }
  }</script>

<style>

  html,
  body {
    height: 100%;
  }

  body {
    display: -ms-flexbox;
    display: -webkit-box;
    display: flex;
    -ms-flex-align: center;
    -ms-flex-pack: center;
    -webkit-box-align: center;
    align-items: center;
    -webkit-box-pack: center;
    justify-content: center;
    padding-top: 40px;
    padding-bottom: 40px;
    background-color: #f5f5f5;
  }

  .form-signin {
    width: 100%;
    max-width: 330px;
    padding: 15px;
    margin: 0 auto;
  }

    .form-signin .checkbox {
      font-weight: 400;
    }

    .form-signin .form-control {
      position: relative;
      box-sizing: border-box;
      height: auto;
      padding: 10px;
      font-size: 16px;
    }

      .form-signin .form-control:focus {
        z-index: 2;
      }

    .form-signin input[type="email"] {
      margin-bottom: -1px;
      border-bottom-right-radius: 0;
      border-bottom-left-radius: 0;
    }

    .form-signin input[type="password"] {
      margin-bottom: 10px;
      border-top-left-radius: 0;
      border-top-right-radius: 0;
    }
</style>
