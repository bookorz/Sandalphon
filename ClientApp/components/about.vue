<template>

  <div>
    <b-card no-body>
      <b-tabs v-model="tabIndex" card>
        <b-tab title="SECS/GEM" :title-link-class="linkClass(0)">
          <div class="secslog">
            <div class="msg" v-for="log in secslog">
              <span style="white-space: pre;font-size:small">{{log.message}}</span>
            </div>
          </div>
        </b-tab>
        <b-tab title="Command" :title-link-class="linkClass(1)">
          <div class="cmdlog">
            <div class="msg" v-for="log in cmdlog">
              <span style="white-space: pre;font-size:small">{{log.message}}</span>
            </div>
          </div>
        </b-tab>
      </b-tabs>
    </b-card>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        secslog: [],
        cmdlog: [],
        msgCount: 0,
        tabIndex: 0
      }
    },
    created() {
      this.$signalrHub.$on('On_Message_Log', this.onDataRcv)
    },
    beforeDestroy() {
      // Make sure to cleanup SignalR event handlers when removing the component
      this.$signalrHub.$off('On_Message_Log', this.onDataRcv)

    },
    methods: {
      onDataRcv: function (data) {
        let newMsg = {
          type: data[0].type,
          message: data[0].message,
          timestamp: ''
        };
        if (data[0].type == 'CMD') {
          this.cmdlog.push(newMsg)
        } else if (data[0].type == 'SECS') {
          this.secslog.push(newMsg)
        }
      },
      linkClass(idx) {
        if (this.tabIndex === idx) {
          return ['bg-primary', 'text-light']
        } else {
          return ['bg-light', 'text-info']
        }
      },
      async GetLogData() {
        let response = await this.$http.get('/api/Transfer/GetLogData')
        this.secslog = response.data.secslog
        this.cmdlog = response.data.cmdlog
      }
    },
    updated() {
      let tempCount = document.querySelectorAll('.msg');
      this.msgCount = tempCount.length;
    },
    watch: {
      msgCount() {
        //scroll to bottom if msg added
        let secslog = document.querySelector('.secslog');
        secslog.scrollTo(0, secslog.scrollHeight);
        let cmdlog = document.querySelector('.cmdlog');
        cmdlog.scrollTo(0, cmdlog.scrollHeight);
      }
    },
    mounted() {
      this.GetLogData()
    }
  }
</script>

<style>
  .secslog, .cmdlog {
    height: 85vh;
    padding-left: 16px;
    overflow-y: auto;
    border: 1px solid #d7d9d5;
    background-color: #fff;
    outline: none;
  }

  .msg {
    margin: 8px 0;
  }

    .msg h3 {
      margin: 0;
      margin-right: 2px;
      color: #333399;
      display: inline-block;
    }

      .msg h3.self {
        color: #888;
      }
</style>
