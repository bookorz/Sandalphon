<template>
  <div class="row">
    <div class="col-4" id="divSTK">
      <b-card title="ELPT Command" sub-title="">
        <b-form-group>
          <b-form-select id="input-3"
                         v-model="form.ELPT"
                         :options="ELPT_List"
                         required></b-form-select>
        </b-form-group>
        <b-form-group>
          <button type="button" class="btn btn-primary">Read RFID</button>
          <button type="button" class="btn btn-secondary">Clamp</button>
          <button type="button" class="btn btn-secondary">UnClamp</button>
        </b-form-group>
        <b-form-group>
          <button type="button" class="btn btn-success">Open Shutter</button>
          <button type="button" class="btn btn-success">Close Shutter</button>

        </b-form-group>
        <b-form-group>
          <button type="button" class="btn btn-info">Move In</button>
          <button type="button" class="btn btn-info">Move Out</button>
        </b-form-group>
        <b-form-group>
          <button type="button" class="btn btn-warning">Init</button>
          <button type="button" class="btn btn-warning">Reset</button>
        </b-form-group>
      </b-card>

      <b-card title="ILPT Command" sub-title="">
        <b-form-group>
          <b-form-select id="input-4"
                         v-model="form.ILPT"
                         :options="ILPT_List"
                         required></b-form-select>
        </b-form-group>

        <b-form-group>
          <button type="button" class="btn btn-primary">Dock</button>
          <button type="button" class="btn btn-primary">UnDock</button>
          <button type="button" class="btn btn-secondary">Clamp</button>
          <button type="button" class="btn btn-secondary">UnClamp</button>
        </b-form-group>
        <b-form-group>
          <button type="button" class="btn btn-success">Load</button>
          <button type="button" class="btn btn-success">UnLoad</button>
          <div class="custom-control custom-checkbox">
            <input type="checkbox" class="custom-control-input" id="withMap" v-model="form.WithMapping">
            <label class="custom-control-label" for="withMap">With Mapping</label>
          </div>
        </b-form-group>
        <b-form-group>
          <button type="button" class="btn btn-warning">Init</button>
          <button type="button" class="btn btn-warning">Reset</button>
        </b-form-group>
      </b-card>
      <b-card title="FOUP Robot Command" sub-title="">
        <b-form-group>
          <b-form-select id="input-5"
                         v-model="form.Source"
                         :options="Source_List"
                         required></b-form-select>
        </b-form-group>
        <b-form-group>
          <button type="button" class="btn btn-primary">Prepare Pick</button>
          <button type="button" class="btn btn-primary">Pick</button>
        </b-form-group>
        <b-form-group>
          <b-form-select id="input-6"
                         v-model="form.Destination"
                         :options="Destination_List"
                         required></b-form-select>
        </b-form-group>
        <b-form-group>
          <button type="button" class="btn btn-primary">Prepare Place</button>
          <button type="button" class="btn btn-primary">Place</button>
        </b-form-group>
      </b-card>
      <b-card header="DEBUG Area" sub-title="">
        {{form}}
      </b-card>
    </div>
    <div class="col-8" id="divWTS">
      <b-card-group deck>
        <b-card title="WHR Command" sub-title="" class=" col-9">
          <b-form-group>
            <button type="button" class="btn btn-warning" @click="RunTask('WHR_RETRACT')">WHR Retract</button>
            <button type="button" class="btn btn-warning">WHR Safty Home</button>
            <button type="button" class="btn btn-warning">WHR Reset</button>
          </b-form-group>
        </b-card>
        <b-card title="Path" sub-title="" class=" col-3">
          <b-form-radio-group id="radio-group-8"
                              v-model="form.Path"
                              :options="Paths"
                              name="radio-options4"></b-form-radio-group>
        </b-card>
      </b-card-group>
      <b-card title="WHR Access ILPT Command" sub-title="">
        <b-form-group>
          <b-form-select id="input-11"
                         v-model="form.WHR_Access_ILPT"
                         :options="ILPT_List"
                         required></b-form-select>
        </b-form-group>
        <b-form-group>
          <button type="button" class="btn btn-success">Prepare Pick</button>
          <button type="button" class="btn btn-success">Pick</button>
          <button type="button" class="btn btn-info">Prepare Place</button>
          <button type="button" class="btn btn-info">Place</button>
        </b-form-group>
      </b-card>
      <b-card title="WHR Access CTU Command" sub-title="">
        <b-form-group>
          <button type="button" class="btn btn-success">CTU Prepare Pick</button><!-- From WHR -->
          <button type="button" class="btn btn-success">WHR Prepare Place</button><!-- to CTU -->
          <button type="button" class="btn btn-success">WHR Extend</button><!-- to CTU -->
          <button type="button" class="btn btn-success">CTU Hold</button><!-- From WHR -->
          <button type="button" class="btn btn-success">WHR Down</button>
          <button type="button" class="btn btn-success">WHR Retract</button>
        </b-form-group>
        <b-form-group>
          <button type="button" class="btn btn-info">CTU Prepare Place</button><!-- To WHR -->
          <button type="button" class="btn btn-info">WHR Prepare Pick</button><!-- From CTU -->
          <button type="button" class="btn btn-info">WHR Extend</button><!-- From CTU -->
          <button type="button" class="btn btn-info">WHR Up</button>
          <button type="button" class="btn btn-info">CTU Release</button><!-- To WHR -->
          <button type="button" class="btn btn-info">WHR Retract</button>
        </b-form-group>
      </b-card>
      <b-card-group deck>
        <b-card title="CTU Command" sub-title="">
          <b-form-group>
            <button type="button" class="btn btn-warning">CTU Home</button>
            <button type="button" class="btn btn-warning">CTU Reset</button>
            <button type="button" class="btn btn-warning">CTU Init</button>
          </b-form-group>
        </b-card>
        <b-card title="PTZ Command" sub-title="">
          <b-form-group>
            <button type="button" class="btn btn-warning">PTZ Home</button>
            <button type="button" class="btn btn-warning">PTZ Reset</button>
          </b-form-group>
        </b-card>
      </b-card-group>
      <b-card title="CTU Access PTZ Command" sub-title="">

        <b-form-group label="Position">
          <b-form-radio-group id="radio-group-1"
                              v-model="form.Position"
                              :options="Positions"
                              name="radio-options"></b-form-radio-group>
        </b-form-group>
        <b-form-group label="Direction">
          <b-form-radio-group id="radio-group-2"
                              v-model="form.Direction"
                              :options="Directions"
                              name="radio-options1"></b-form-radio-group>
        </b-form-group>

        <b-form-group>
          <button type="button" class="btn btn-info">CTU Prepare Place</button><!-- To PTZ -->
          <button type="button" class="btn btn-info">PTZ Transfer</button><!-- Get from CTU -->
          <button type="button" class="btn btn-info">CTU Place</button><!-- to CTU -->
          <button type="button" class="btn btn-info">PTZ Home</button><!-- From WHR -->
        </b-form-group>
        <b-form-group>
          <button type="button" class="btn btn-success">CTU Prepare Pick</button><!-- From PTZ -->
          <button type="button" class="btn btn-success">PTZ Transfer</button><!-- Get from CTU -->
          <button type="button" class="btn btn-success">CTU Pick</button><!-- to CTU -->
          <button type="button" class="btn btn-success">PTZ Home</button><!-- From WHR -->
        </b-form-group>

      </b-card>

    </div>
    <!-- 使用data變數spinnerStatus來控制顯示與否 -->
    <!-- 使用data變數msg來動態設定顯示字串 -->
    <BlockUI :message="msg"
             v-show="spinnerStatus">
      <b-spinner variant="info" label="Loading..."></b-spinner>
    </BlockUI>
  </div>
</template>

<script>
  import { mapActions, mapState } from 'vuex'
  export default {
    data() {
      return {
        msg: "Loading...",
        spinnerStatus: false,
        ELPT_List: [{ text: 'Select ELPT', value: null }, 'ELPT1', 'ELPT2'],
        ILPT_List: [{ text: 'Select ILPT', value: null }, 'ILPT1', 'ILPT2'],
        Source_List: [{ text: 'Select Source', value: null }, 'ILPT1', 'ILPT2', 'ELPT1', 'ELPT2', 'SHELF1', 'SHELF2', 'SHELF3', 'SHELF4', 'SHELF5', 'SHELF6', 'SHELF7', 'SHELF8', 'SHELF9', 'SHELF10', 'SHELF11', 'SHELF12', 'SHELF13', 'SHELF14', 'SHELF15', 'SHELF16'],
        Destination_List: [{ text: 'Select Destination', value: null }, 'ILPT1', 'ILPT2', 'ELPT1', 'ELPT2', 'SHELF1', 'SHELF2', 'SHELF3', 'SHELF4', 'SHELF5', 'SHELF6', 'SHELF7', 'SHELF8', 'SHELF9', 'SHELF10', 'SHELF11', 'SHELF12', 'SHELF13', 'SHELF14', 'SHELF15', 'SHELF16'],
        Paths: ['Clean', 'Dirty'],
        Positions: [
          { text: 'Odd', value: 'Odd' },
          { text: 'Even', value: 'Even' },
          { text: 'Home', value: 'Home' }
        ],
        Directions: [
          { text: 'Face To Face', value: 'Face_To_Face' },
          { text: 'Back to Back', value: 'Back_to_Back' },
          { text: 'Face To Back', value: 'Face_To_Back' },
          { text: 'Back To Face', value: 'Back_To_Face' }
        ]
      }
    },

    computed: {
      ...mapState({
        form: state => state.form
      })
    },

    methods: {
      ...mapActions(['setForm']),
      async GetNodeList() {
        let response = await this.$http.get('/api/Transfer/NodeInfoList')
        this.nodeList = response.data.msg
      },
      onSelectedNode(event) {
        this.showType = event.target.value
      },
      RunTask(TaskName) {
        this.msg = 'Sending...'
        this.spinnerStatus = true
        var target = ''

        switch (TaskName) {
          case 'WHR_RETRACT':

            break
        }
        this.$http.post('/api/Transfer/TaskRun', {name : TaskName, target: target})
      
      },
      On_TaskJob_Ack(data) {
        this.msg = 'Moving...'
      },
      On_TaskJob_Finished(data) {
        this.spinnerStatus = false
      }
    },

    mounted() {
      this.GetNodeList()
    },

    created() {
      this.$signalrHub.$on('On_TaskJob_Ack', this.On_TaskJob_Ack)
      this.$signalrHub.$on('On_TaskJob_Finished', this.On_TaskJob_Finished)
    },
    beforeDestroy() {
      this.setForm({ form: form })
      // Make sure to cleanup SignalR event handlers when removing the component
      this.$signalrHub.$off('On_TaskJob_Ack', this.On_TaskJob_Ack)
      this.$signalrHub.$off('On_TaskJob_Finished', this.On_TaskJob_Finished)
    }
  }
</script>

<style>

  .card {
    margin-top: 5px;
    margin-bottom: 5px;
  }

  .card-body {
    padding-bottom: 0px;
  }
</style>
