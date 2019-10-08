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
    <p>
      Connection status: <strong>{{ messages }}</strong>
    </p>


    <button type="button" class="btn btn-primary" @click="incrementCounter()">Increment</button>
    <button type="button" class="btn btn-secondary" @click="resetCounter()">Reset</button>
    <button type="button" class="btn btn-primary" @click="Home('ROBOT01')">Home</button>
    <button type="button" class="btn btn-primary" @click="Reset('ROBOT01')">Reset</button>
    <button type="button" class="btn btn-primary" @click="GetNode('ROBOT01')">Node Information</button>
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
      },
      Home: function (target) {
        this.$http.post('/api/weather/TaskRun', {name : 'ROBOT_HOME', target: target})
      },
      Reset: function (target) {
        this.$http.post('/api/weather/TaskRun', {name : 'ROBOT_RESET', target: target})
      },
      async GetNode(target) {
        let response = await this.$http.post('/api/weather/NodeInfo', {name : target})
        this.messages = response.data.msg
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
