<template>
  <div>
    <h1>Counter</h1>

    <p>This is a simple example of a Vue.js component & Vuex</p>

    <p>
      Current count (Vuex): <strong>{{ currentCount }}</strong>
    </p>
    <p>
      Auto count: <strong>{{ autoCount }}</strong>
    </p>
    <p>
      nodeName: <strong>{{ messages.nodeName }}</strong>
    </p>
    <p>
      Connection status: <strong>{{ messages.status }}</strong>
    </p>

    <button type="button" class="btn btn-primary" @click="incrementCounter()">Increment</button>
    <button type="button" class="btn btn-secondary" @click="resetCounter()">Reset</button>
  </div>
</template>

<script>
  import { mapActions, mapState } from 'vuex'

  export default {
    data() {
      return {
        autoCount: 0,
        messages: '77'
      }
    },

    computed: {
      ...mapState({
        currentCount: state => state.counter
      })
    },

    methods: {
      ...mapActions(['setCounter']),

      incrementCounter: function () {
        var counter = this.currentCount + 1
        this.setCounter({ counter: counter })
      },

      resetCounter: function () {
        this.setCounter({ counter: 0 })
        this.autoCount = 0
      },
      onDataRcv: function (data) {
        this.messages = data[0]
      }
    },

    created() {
      setInterval(() => {
        this.autoCount += 1
      }, 1000)
      this.$signalrHub.$on('On_Node_Connection_Changed', this.onDataRcv)
    },
    beforeDestroy() {
      // Make sure to cleanup SignalR event handlers when removing the component
      this.$signalrHub.$off('On_Node_Connection_Changed', this.onDataRcv)

    }
  }
</script>

<style>

</style>
