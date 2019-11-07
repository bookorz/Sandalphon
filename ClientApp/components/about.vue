<template>
  <div>
    <h1 class="mt-4">
      SECS Log
    </h1>
    <div class="chatbox">
      <div class="msg" v-for="chat in livechat">
        <span style="white-space: pre">{{chat.content}}</span>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        livechat: [],
        msgCount: 0
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
          username: data[0].type, 
          content: data[0].message,
          timestamp: ''
        };
        this.livechat.push(newMsg)
      }
    },
    updated() {
      let tempCount = document.querySelectorAll('.msg');
      this.msgCount = tempCount.length;
    },
    watch: {
        msgCount() {
        //scroll to bottom if msg added
        let chatbox = document.querySelector('.chatbox');
        chatbox.scrollTo(0,chatbox.scrollHeight);
      }
    }
  }
</script>

<style>
  .chatbox {
    height: 90vh;
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
