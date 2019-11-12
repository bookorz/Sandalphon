<template>
  <div>
    
      <b-row>
        <b-col >
          <b-card header="ELPT Command" bg-variant="dark" text-variant="white" sub-title="">
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

          <b-card header="ILPT Command" bg-variant="dark" text-variant="white" sub-title="">
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
          <b-card header="FOUP Robot Command" bg-variant="dark" text-variant="white" sub-title="">
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
        </b-col>
        <b-col cols="8">
          <b-card-group deck>
            <b-card header="WHR Command" bg-variant="dark" text-variant="white" sub-title="" class=" col-9">
              <b-form-group>
                <button type="button" class="btn btn-warning" @click="RunTask('WHR_RETRACT')">WHR Retract</button>
                <button type="button" class="btn btn-warning" @click="RunTask('WHR_SHOME')">WHR Safty Home</button>
                <button type="button" class="btn btn-warning" @click="RunTask('WHR_RESET')">WHR Reset</button>
              </b-form-group>
            </b-card>
            <b-card header="Path" bg-variant="dark" text-variant="white" sub-title="" class=" col-3">
              <b-form-radio-group id="radio-group-8"
                                  v-model="form.Path"
                                  :options="Paths"
                                  name="radio-options4"></b-form-radio-group>
            </b-card>
          </b-card-group>



          <b-card header="WHR Access ILPT Command" bg-variant="dark" text-variant="white" sub-title="">
            <b-form-group>
              <b-form-select id="input-11"
                             v-model="form.WHR_Access_ILPT"
                             :options="ILPT_List"
                             required></b-form-select>
            </b-form-group>
            <b-form-group>
              <button type="button" class="btn btn-success" @click="RunTask('WHR_PREPAREPICK')">Prepare Pick</button>
              <button type="button" class="btn btn-success" @click="RunTask('WHR_PICK')">Pick</button>
              <button type="button" class="btn btn-info" @click="RunTask('WHR_PREPAREPLACE')">Prepare Place</button>
              <button type="button" class="btn btn-info" @click="RunTask('WHR_PLACE')">Place</button>
            </b-form-group>
          </b-card>
          <b-card header="WHR Access CTU Command" bg-variant="dark" text-variant="white" sub-title="">
            <b-form-group>
              <button type="button" class="btn btn-success" @click="RunTask('CTU_PREPAREPICK_WHR')">CTU Prepare Pick</button><!-- From WHR -->
              <button type="button" class="btn btn-success" @click="RunTask('WHR_PREPAREPLACE_CTU')">WHR Prepare Place</button><!-- to CTU -->
              <button type="button" class="btn btn-success" @click="RunTask('WHR_EXTEND_PUT')">WHR Extend</button><!-- to CTU -->
              <button type="button" class="btn btn-success" @click="RunTask('CTU_HOLD')">CTU Hold</button><!-- From WHR -->
              <button type="button" class="btn btn-success" @click="RunTask('WHR_DOWN')">WHR Down</button>
              <button type="button" class="btn btn-success" @click="RunTask('WHR_RETRACT')">WHR Retract</button>
            </b-form-group>
            <b-form-group>
              <button type="button" class="btn btn-info" @click="RunTask('CTU_PREPAREPLACE_WHR')">CTU Prepare Place</button><!-- To WHR -->
              <button type="button" class="btn btn-info" @click="RunTask('WHR_PREPAREPICK_CTU')">WHR Prepare Pick</button><!-- From CTU -->
              <button type="button" class="btn btn-info" @click="RunTask('WHR_EXTEND_GET')">WHR Extend</button><!-- From CTU -->
              <button type="button" class="btn btn-info" @click="RunTask('WHR_UP')">WHR Up</button>
              <button type="button" class="btn btn-info" @click="RunTask('CTU_RELEASE')">CTU Release</button><!-- To WHR -->
              <button type="button" class="btn btn-info" @click="RunTask('WHR_RETRACT')">WHR Retract</button>
            </b-form-group>
          </b-card>
          <b-card-group deck>
            <b-card header="CTU Command" bg-variant="dark" text-variant="white" sub-title="">
              <b-form-group>
                <button type="button" class="btn btn-warning" @click="RunTask('CTU_HOME')">Home</button>
                <button type="button" class="btn btn-warning" @click="RunTask('CTU_RESET')">Reset</button>
                <button type="button" class="btn btn-warning" @click="RunTask('CTU_INIT')">Initial</button>
              </b-form-group>
            </b-card>
            <b-card header="PTZ Command" bg-variant="dark" text-variant="white" sub-title="">
              <b-form-group>
                <button type="button" class="btn btn-warning" @click="RunTask('PTZ_HOME')">Home</button>
                <button type="button" class="btn btn-warning">Reset</button>
              </b-form-group>
            </b-card>
            <b-card header="ALIGNER Command" bg-variant="dark" text-variant="white" sub-title="">
              <b-form-group>
                <button type="button" class="btn btn-warning" @click="RunTask('PTZ_HOME')">Home</button>
                <button type="button" class="btn btn-warning">Align</button>
              </b-form-group>
            </b-card>
          </b-card-group>
          <b-card header="CTU Access PTZ Command" bg-variant="dark" text-variant="white" sub-title="">

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
              <button type="button" class="btn btn-info" @click="RunTask('PTZ_TRANSFER')">PTZ Transfer</button><!-- Get from CTU -->
              <button type="button" class="btn btn-info" @click="RunTask('CTU_PLACE_PTZ')">CTU Place</button><!-- to CTU -->
              <button type="button" class="btn btn-info" @click="RunTask('PTZ_HOME')">PTZ Home</button><!-- From WHR -->
            </b-form-group>
            <b-form-group>
              <button type="button" class="btn btn-success">CTU Prepare Pick</button><!-- From PTZ -->
              <button type="button" class="btn btn-success" @click="RunTask('PTZ_TRANSFER')">PTZ Transfer</button><!-- Get from CTU -->
              <button type="button" class="btn btn-success" @click="RunTask('CTU_PICK_PTZ')">CTU Pick</button><!-- to CTU -->
              <button type="button" class="btn btn-success" @click="RunTask('PTZ_HOME')">PTZ Home</button><!-- From WHR -->
            </b-form-group>

          </b-card>

        </b-col>
      </b-row>
  
    <!-- 使用data變數spinnerStatus來控制顯示與否 -->
    <!-- 使用data變數msg來動態設定顯示字串 -->
    <BlockUI :message="msg"
             v-show="spinnerStatus">
      <b-spinner variant="info" label="Loading..."></b-spinner>
    </BlockUI>

    <b-modal id="alarm_modal" hide-footer>
      <template v-slot:modal-title>
        Alarm Happend
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
        msg: "Loading...",
        spinnerStatus: false,
        ELPT_List: [{ text: 'Select ELPT', value: null }, 'ELPT1', 'ELPT2'],
        ILPT_List: [{ text: 'Select ILPT', value: null }, 'ILPT1', 'ILPT2'],
        Source_List: [{ text: 'Select Source', value: null }, 'ILPT1', 'ILPT2', 'ELPT1', 'ELPT2', 'SHELF1', 'SHELF2', 'SHELF3', 'SHELF4', 'SHELF5', 'SHELF6', 'SHELF7', 'SHELF8', 'SHELF9', 'SHELF10', 'SHELF11', 'SHELF12', 'SHELF13', 'SHELF14', 'SHELF15', 'SHELF16'],
        Destination_List: [{ text: 'Select Destination', value: null }, 'ILPT1', 'ILPT2', 'ELPT1', 'ELPT2', 'SHELF1', 'SHELF2', 'SHELF3', 'SHELF4', 'SHELF5', 'SHELF6', 'SHELF7', 'SHELF8', 'SHELF9', 'SHELF10', 'SHELF11', 'SHELF12', 'SHELF13', 'SHELF14', 'SHELF15', 'SHELF16'],
        Paths: [{ text: 'Clean', value: 0 }, { text: 'Dirty', value: 1 }],
        Positions: [
          { text: 'Odd', value: '0' },
          { text: 'Even', value: '1' },
          { text: 'Home', value: '2' }
        ],
        Directions: [
          { text: 'Face To Face', value: '0' },
          { text: 'Back to Back', value: '1' },
          { text: 'Face To Back', value: '2' },
          { text: 'Back To Face', value: '3' }
        ],
        alarm_message: null
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
      RunTask(TaskName) {
        var target = ''
        var mode = this.form.Path
        var station = this.form.Position
        var direction = this.form.Direction
        var value = ''
        var position = ''


        switch (TaskName) {
          case 'WHR_PREPAREPICK':
            position = this.form.WHR_Access_ILPT
            break
          case 'WHR_PREPAREPICK_CTU':
            TaskName = 'WHR_PREPAREPICK'
            position = 'CTU'
            break;
          case 'WHR_PREPAREPLACE_CTU':
            TaskName = 'WHR_PREPAREPLACE'
            position = 'CTU'
            break;
          case 'CTU_PREPAREPICK_WHR':
            TaskName = 'CTU_PREPAREPICK'
            position = 'WHR'
            break;
          case 'CTU_PREPAREPLACE_WHR':
            TaskName = 'CTU_PREPAREPLACE'
            position = 'WHR'
            break;
          case 'WHR_EXTEND_PUT':
            TaskName = 'WHR_EXTEND'
            value = '1'
            station = '3'
            break;
          case 'WHR_EXTEND_GET':
            TaskName = 'WHR_EXTEND'
            value = '0'
            station = '3'
            break;
          case 'CTU_PLACE_PTZ':
            TaskName = 'CTU_PLACE'
            position = 'PTZ'
            break;
          case 'CTU_PICK_PTZ':
            TaskName = 'CTU_PICK'
            position = 'PTZ'
            break;
        }
        this.$http.post('/api/Transfer/TaskRun', { name: TaskName, target: target, mode: mode, station: station, direction: direction, value: value, position: position })

      },
      On_TaskJob_Ack(data) {
        this.msg = 'Excuting...'
        this.spinnerStatus = true
      },
      On_TaskJob_Finished(data) {
        this.spinnerStatus = false
      },
      On_TaskJob_Aborted(data) {
        this.spinnerStatus = false
      },
      On_Alarm_Happen(data) {
        this.alarm_message = data[0]
        this.$bvModal.show('alarm_modal')
      }
    },

    mounted() {
      this.GetNodeList()
    },

    created() {
      this.$signalrHub.$on('On_TaskJob_Ack', this.On_TaskJob_Ack)
      this.$signalrHub.$on('On_TaskJob_Finished', this.On_TaskJob_Finished)
      this.$signalrHub.$on('On_TaskJob_Aborted', this.On_TaskJob_Aborted)
      this.$signalrHub.$on('On_Alarm_Happen', this.On_Alarm_Happen)
    },
    beforeDestroy() {
      this.setForm({ form: form })
      // Make sure to cleanup SignalR event handlers when removing the component
      this.$signalrHub.$off('On_TaskJob_Ack', this.On_TaskJob_Ack)
      this.$signalrHub.$off('On_TaskJob_Finished', this.On_TaskJob_Finished)
      this.$signalrHub.$off('On_TaskJob_Aborted', this.On_TaskJob_Aborted)
      this.$signalrHub.$off('On_Alarm_Happen', this.On_Alarm_Happen)
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
