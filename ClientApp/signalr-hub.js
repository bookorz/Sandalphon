import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'
export default {
  install (Vue) {
    const connection = new HubConnectionBuilder()
      .withUrl('/message')
      .configureLogging(LogLevel.Information)
      .build()
    let startedPromise = null
    function start () {
      startedPromise = connection.start().catch(err => {
        console.error('Failed to connect with hub', err)
        return new Promise((resolve, reject) =>
          setTimeout(() => start().then(resolve).catch(reject), 5000))
      })
      return startedPromise
    }
    connection.onclose(() => start())
    start()
    // use new Vue instance as an event bus
    const signalrHub = new Vue()
    signalrHub.questionOpened = (questionId) => {
      return startedPromise
        .then(() => connection.invoke('JoinQuestionGroup', questionId))
        .catch(console.error)
    }
    signalrHub.questionClosed = (questionId) => {
      return startedPromise
        .then(() => connection.invoke('LeaveQuestionGroup', questionId))
        .catch(console.error)
    }
    // every component will use this.$questionHub to access the event bus
    Vue.prototype.$signalrHub = signalrHub
    // Forward server side SignalR events through $questionHub, where components will listen to them
    connection.on('On_Node_Connection_Changed', (data) => {
      signalrHub.$emit('On_Node_Connection_Changed', data)
    })
    connection.on('UserConnected', (data) => {
      signalrHub.$emit('UserConnected', data)
    })
    connection.on('On_Message_Log', (data) => {
      signalrHub.$emit('On_Message_Log', data)
    })
  }
}
