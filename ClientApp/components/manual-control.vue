<template>
  <div>
    <div class="card">
      <div class="card-header">
        <select class="form-control" v-model="selected" @change="onSelectedNode($event)">
          <option v-for="(node, index) in nodeList">{{node.name}}</option>
        </select>
      </div>
      <div class="card-body">
        <component :is="showType" />
      </div>
    </div>
  </div>
</template>

<script>
  import ROBOT from './manual/robot-manual'
  import LOADPORT from './manual/ctu-manual'
  export default {
    components: { ROBOT, LOADPORT },
    data() {
      return {
        nodeList: null,
        selected: null,
        showType: "LOADPORT"
      }
    },

    computed: {

    },

    methods: {
      async GetNodeList() {
        let response = await this.$http.get('/api/Transfer/NodeInfoList')
        this.nodeList = response.data.msg
      },
      onSelectedNode(event) {
        this.showType = event.target.value
      }
    },

    mounted() {
      this.GetNodeList()
    },

    created() {

    },
    beforeDestroy() {

    }
  }
</script>

<style>
</style>
