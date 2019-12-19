<template>
  <div>
    <b-row>
      <b-col>
        <b-card title="ELPT Command" bg-variant="dark" text-variant="white" sub-title="">
          <b-form-group>
            <b-form-select id="input-3"
                           v-model="form.ELPT"
                           :options="ELPT_List"
                           required></b-form-select>
          </b-form-group>
          <b-form-group :disabled="form.ELPT==null">
            <button type="button" class="btn btn-primary" @click="RunTask('ELPT_READ_RFID')">Read RFID</button>
            <button type="button" class="btn btn-secondary" @click="RunTask('ELPT_CLAMP')">Clamp</button>
            <button type="button" class="btn btn-secondary" @click="RunTask('ELPT_UNCLAMP')">UnClamp</button>
          </b-form-group>
          <b-form-group :disabled="form.ELPT==null">
            <button type="button" class="btn btn-success" @click="RunTask('ELPT_OPEN_SHUTTER')">Open Shutter</button>
            <button type="button" class="btn btn-success" @click="RunTask('ELPT_CLOSE_SHUTTER')">Close Shutter</button>

          </b-form-group>
          <b-form-group :disabled="form.ELPT==null">
            <button type="button" class="btn btn-info" @click="RunTask('ELPT_MOVE_IN')">Move In</button>
            <button type="button" class="btn btn-info" @click="RunTask('ELPT_MOVE_OUT')">Move Out</button>
          </b-form-group>
          <b-form-group :disabled="form.ELPT==null">
            <b-row>
              <b-col sm="4">
                <b-form-input v-model="form.elpt_speed"
                              :state="ELPTSpeedState"
                              aria-describedby="input-live-help input-live-feedback"
                              placeholder="Speed %"
                              trim></b-form-input>

                <!-- This will only be shown if the preceding input has an invalid state -->
                <!--<b-form-invalid-feedback id="input-live-feedback">
                  Enter 1~100
                </b-form-invalid-feedback>-->
              </b-col>
              <b-col sm="3">
                <button type="button" class="btn btn-warning" :disabled="!ELPTSpeedState" @click="RunTask('ELPT_SET_SPEED')">Speed</button>
              </b-col>
              <b-col sm="5">
                <button type="button" class="btn btn-warning" @click="RunTask('ELPT_INIT')">Init</button>
                <button type="button" class="btn btn-warning" @click="RunTask('ELPT_RESET')">Reset</button>
              </b-col>
            </b-row>
          </b-form-group>
          <b-form-group label="Light Curtain">
            <b-row>
              <b-col sm="3">
                <button type="button" class="btn btn-warning" @click="RunTask('LIGHT_CURTAIN_RESET')">Reset</button>
              </b-col>
              <b-col sm="3">
                <b-form-checkbox v-model="form.light_curtain" @change="RunTask('LIGHT_CURTAIN_ENABLED')" switch>
                  Enabled
                </b-form-checkbox>
              </b-col>

            </b-row>
          </b-form-group>

        </b-card>

        <b-card title="ILPT Command" bg-variant="dark" text-variant="white" sub-title="">
          <b-form-group>
            <b-form-select id="input-4"
                           v-model="form.ILPT"
                           :options="ILPT_List"
                           required></b-form-select>
          </b-form-group>
          <b-form-group :disabled="form.ILPT==null">
            <b-row>
              <b-col sm="5">
                <button type="button" class="btn btn-success" @click="RunTask('ILPT_LOAD')">Load</button>
                <button type="button" class="btn btn-success" @click="RunTask('ILPT_UNLOAD')">UnLoad</button>
              </b-col>
              <b-col sm="7">
                <div class="custom-control custom-checkbox">
                  <input type="checkbox" class="custom-control-input" id="withMap" v-model="form.WithMapping">
                  <label class="custom-control-label" for="withMap">With Mapping</label>
                </div>
              </b-col>
            </b-row>
          </b-form-group>
          <b-form-group :disabled="form.ILPT==null">
            <button type="button" class="btn btn-warning" @click="RunTask('ILPT_INIT')">Init</button>
            <button type="button" class="btn btn-warning" @click="RunTask('ILPT_RESET')">Reset</button>
          </b-form-group>
        </b-card>
        <b-card title="FOUP Robot Command" bg-variant="dark" text-variant="white" sub-title="">
          <b-form-group>
            <b-row>
              <b-col sm="5">
                <b-form-select id="input-5"
                               v-model="form.Source"
                               :options="Source_List"
                               required></b-form-select>
              </b-col>
              <b-col sm="7">
                <button type="button" class="btn btn-primary" :disabled="form.Source==null" @click="RunTask('FOUPROBOT_PREPARE_PICK')">Prepare Pick</button>
                <button type="button" class="btn btn-primary" :disabled="form.Source==null" @click="RunTask('FOUPROBOT_PICK')">Pick</button>
              </b-col>
            </b-row>
          </b-form-group>
          <b-form-group>
            <b-row>
              <b-col sm="5">
                <b-form-select id="input-6"
                               v-model="form.Destination"
                               :options="Destination_List"
                               required></b-form-select>
              </b-col>
              <b-col sm="7">
                <button type="button" class="btn btn-primary" :disabled="form.Destination==null" @click="RunTask('FOUPROBOT_PREPARE_PLACE')">Prepare Place</button>
                <button type="button" class="btn btn-primary" :disabled="form.Destination==null" @click="RunTask('FOUPROBOT_PLACE')">Place</button>
              </b-col>
            </b-row>
          </b-form-group>
          <b-form-group>
            <b-row>
              <b-col sm="4">
                <b-form-input v-model="form.foup_robot_speed"
                              :state="FoupRobotSpeedState"
                              aria-describedby="input-live-help input-live-feedback"
                              placeholder="Speed %"
                              trim></b-form-input>

                <!-- This will only be shown if the preceding input has an invalid state -->
                <!--<b-form-invalid-feedback id="input-live-feedback">
                  Enter 1~100
                </b-form-invalid-feedback>-->
              </b-col>
              <b-col sm="3">
                <button type="button" class="btn btn-warning" :disabled="!FoupRobotSpeedState" @click="RunTask('FOUPROBOT_SET_SPEED')">Speed</button>
              </b-col>
              <b-col sm="5">
                <button type="button" class="btn btn-warning" @click="RunTask('FOUPROBOT_INIT')">Init</button>
                <button type="button" class="btn btn-warning" @click="RunTask('FOUPROBOT_RESET')">Reset</button>
              </b-col>
            </b-row>
          </b-form-group>
        </b-card>
        <!--<b-card header="DEBUG Area" sub-title="">
          {{form}}
        </b-card>-->
      </b-col>
      <b-col cols="8">
        <b-card-group deck>
          <b-card title="WHR Command" bg-variant="dark" text-variant="white" sub-title="" class=" col-9">
            <b-form-group>
              <b-row>
                <b-col sm="6">
                  <button type="button" class="btn btn-warning" @click="RunTask('WHR_RETRACT')">Retract</button>
                  <button type="button" class="btn btn-warning" @click="RunTask('WHR_SHOME')">Safty Home</button>
                  <button type="button" class="btn btn-warning" @click="RunTask('WHR_RESET')">Reset</button>
                </b-col>
                <b-col sm="4">
                  <b-form-input v-model="form.whr_speed"
                                :state="WHRSpeedState"
                                aria-describedby="input-live-help input-live-feedback"
                                placeholder="Speed %"
                                trim></b-form-input>

                  <!-- This will only be shown if the preceding input has an invalid state -->
                  <!--<b-form-invalid-feedback id="input-live-feedback">
                    Enter 1~100
                  </b-form-invalid-feedback>-->
                </b-col>
                <b-col sm="2">
                  <button type="button" class="btn btn-warning" :disabled="!WHRSpeedState" @click="RunTask('WHR_SET_SPEED')">Set</button>
                </b-col>
              </b-row>
            </b-form-group>
          </b-card>
          <b-card title="Path" bg-variant="dark" text-variant="white" sub-title="" class=" col-3">
            <b-form-radio-group id="radio-group-8"
                                v-model="form.Path"
                                :options="Paths"
                                name="radio-options4"></b-form-radio-group>
          </b-card>
        </b-card-group>



        <b-card title="WHR Access ILPT Command" bg-variant="dark" text-variant="white" sub-title="">
          <b-form-group>
            <b-row>
              <b-col sm="4">
                <b-form-select id="input-11"
                               v-model="form.WHR_Access_ILPT"
                               :options="ILPT_List"
                               required></b-form-select>
              </b-col>
              <b-col sm="8">
                <b-form-group :disabled="form.WHR_Access_ILPT==null">
                  <button type="button" class="btn btn-success" @click="RunTask('WHR_PREPAREPICK')">Prepare Pick</button>
                  <button type="button" class="btn btn-success" @click="RunTask('WHR_PICK')">Pick</button>
                  <button type="button" class="btn btn-info" @click="RunTask('WHR_PREPAREPLACE')">Prepare Place</button>
                  <button type="button" class="btn btn-info" @click="RunTask('WHR_PLACE')">Place</button>
                </b-form-group>
              </b-col>
            </b-row>
          </b-form-group>
        </b-card>
        <b-card title="WHR Access CTU Command" bg-variant="dark" text-variant="white" sub-title="">
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
          <b-card title="CTU Command" bg-variant="dark" text-variant="white" sub-title="">
            <b-form-group>
              <button type="button" class="btn btn-warning" @click="RunTask('CTU_HOME')">Home</button>
              <button type="button" class="btn btn-warning" @click="RunTask('CTU_RESET')">Reset</button>
              <button type="button" class="btn btn-warning" @click="RunTask('CTU_INIT')">Initial</button>
            </b-form-group>
            <b-form-group>
              <b-row>
                <b-col sm="8">
                  <b-form-input v-model="form.ctu_speed"
                                :state="CTUSpeedState"
                                aria-describedby="input-live-help input-live-feedback"
                                placeholder="Speed %"
                                trim></b-form-input>

                  <!-- This will only be shown if the preceding input has an invalid state -->
                  <!--<b-form-invalid-feedback id="input-live-feedback">
                    Enter 1~100
                  </b-form-invalid-feedback>-->
                </b-col>
                <b-col sm="4">
                  <button type="button" class="btn btn-warning" :disabled="!CTUSpeedState" @click="RunTask('CTU_SET_SPEED')">Set</button>
                </b-col>
              </b-row>
            </b-form-group>
          </b-card>
          <b-card title="PTZ Command" bg-variant="dark" text-variant="white" sub-title="">
            <b-form-group>
              <button type="button" class="btn btn-warning" @click="RunTask('PTZ_HOME')">Home</button>
              <button type="button" class="btn btn-warning" @click="RunTask('CTU_RESET')">Reset</button>
            </b-form-group>
            <b-form-group>
              <b-row>
                <b-col sm="8">
                  <b-form-input v-model="form.ptz_speed"
                                :state="PTZSpeedState"
                                aria-describedby="input-live-help input-live-feedback"
                                placeholder="Speed %"
                                trim></b-form-input>

                  <!-- This will only be shown if the preceding input has an invalid state -->
                  <!--<b-form-invalid-feedback id="input-live-feedback">
                    Enter 1~100
                  </b-form-invalid-feedback>-->
                </b-col>
                <b-col sm="4">
                  <button type="button" class="btn btn-warning" :disabled="!PTZSpeedState" @click="RunTask('PTZ_SET_SPEED')">Set</button>
                </b-col>
              </b-row>
            </b-form-group>
          </b-card>
          <b-card title="ALIGNER Command" bg-variant="dark" text-variant="white" sub-title="">
            <b-form-group>
              <b-row>
                <b-col sm="8">
                  <b-form-input v-model="form.align_angle"
                                :state="AngleState"
                                aria-describedby="input-live-help input-live-feedback"
                                placeholder="Align angle"
                                trim></b-form-input>

                  <!-- This will only be shown if the preceding input has an invalid state -->
                  <b-form-invalid-feedback id="input-live-feedback">
                    Enter 6 digits
                  </b-form-invalid-feedback>
                </b-col>
                <b-col sm="4">
                  <button type="button" class="btn btn-warning" :disabled="!AngleState" @click="RunTask('WTSALIGNER_ALIGN')">Align</button>
                </b-col>
              </b-row>
            </b-form-group>
            <b-form-group>
              <b-row>
                <b-col sm="8">
                  <b-form-input v-model="form.aligner_speed"
                                :state="AlignerSpeedState"
                                aria-describedby="input-live-help input-live-feedback"
                                placeholder="Speed %"
                                trim></b-form-input>

                  <!-- This will only be shown if the preceding input has an invalid state -->
                  <!--<b-form-invalid-feedback id="input-live-feedback">
                    Enter 1~100
                  </b-form-invalid-feedback>-->
                </b-col>
                <b-col sm="4">
                  <button type="button" class="btn btn-warning" :disabled="!AlignerSpeedState" @click="RunTask('WTSALIGNER_SET_SPEED')">Set</button>
                </b-col>
              </b-row>
            </b-form-group>
          </b-card>
        </b-card-group>
        <b-card title="CTU Access PTZ Command" bg-variant="dark" text-variant="white" sub-title="">

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

    <b-modal id="alarm_modal" size="lg"
             header-bg-variant="danger"
             header-text-variant="light"
             centered hide-footer>
      <template v-slot:modal-title>
        Alarm Happend
      </template>
      <b-container fluid>
        <b-row class="mb-1">
          <b-col cols="2">Node:</b-col>
          <b-col>
            {{alarm_message.nodeName}}
          </b-col>
        </b-row>

        <b-row class="mb-1">
          <b-col cols="2">Error Code:</b-col>
          <b-col>
            {{alarm_message.errorCode}}
          </b-col>
        </b-row>
        <b-row class="mb-1">
          <b-col cols="2">Error Desc:</b-col>
          <b-col>
            {{alarm_message.errorDesc}}
          </b-col>
        </b-row>
      </b-container>
      <b-button class="mt-3" block @click="$bvModal.hide('alarm_modal')">Close Me</b-button>
    </b-modal>
  </div>
</template>

<script>
  import { mapActions, mapState } from 'vuex'
  import qs from 'qs'
  export default {
    data() {
      return {
        msg: "Loading...",
        spinnerStatus: false,
        ELPT_List: [{ text: 'Select ELPT', value: null }, { text: 'ELPT1', value: 'ELPT1' }, { text: 'ELPT2', value: 'ELPT2' }],
        ILPT_List: [{ text: 'Select ILPT', value: null }, { text: 'ILPT1', value: 'ILPT1' }, { text: 'ILPT2', value: 'ILPT2' }],
        Source_List: [{ text: 'Source', value: null }, 'ILPT1', 'ILPT2', 'ELPT1', 'ELPT2', 'SHELF1', 'SHELF2', 'SHELF3', 'SHELF4', 'SHELF5', 'SHELF6', 'SHELF7', 'SHELF8', 'SHELF9', 'SHELF10', 'SHELF11', 'SHELF12', 'SHELF13', 'SHELF14', 'SHELF15', 'SHELF16'],
        Destination_List: [{ text: 'Destination', value: null }, 'ILPT1', 'ILPT2', 'ELPT1', 'ELPT2', 'SHELF1', 'SHELF2', 'SHELF3', 'SHELF4', 'SHELF5', 'SHELF6', 'SHELF7', 'SHELF8', 'SHELF9', 'SHELF10', 'SHELF11', 'SHELF12', 'SHELF13', 'SHELF14', 'SHELF15', 'SHELF16'],
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
        alarm_message: { nodeName: '', errorCode: '', errorDesc: '' }
      }
    },

    computed: {
      ...mapState({
        form: state => state.form
      }),
      AngleState() {
        return /^([012]?[0-9]?[0-9]|3[0-5][0-9])$/.test(this.form.align_angle)
      },
      FoupRobotSpeedState() {
        return /^(100)$|^(\d{1,2})$/.test(this.form.foup_robot_speed)
      },
      WHRSpeedState() {
        return /^(100)$|^(\d{1,2})$/.test(this.form.whr_speed)
      },
      CTUSpeedState() {
        return /^(100)$|^(\d{1,2})$/.test(this.form.ctu_speed)
      },
      PTZSpeedState() {
        return /^(100)$|^(\d{1,2})$/.test(this.form.ptz_speed)
      },
      ELPTSpeedState() {
        return /^(100)$|^(\d{1,2})$/.test(this.form.elpt_speed)
      },
      AlignerSpeedState() {
        return /^(100)$|^(\d{1,2})$/.test(this.form.aligner_speed)
      }
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
        var val2 = ''

        switch (TaskName) {

          case 'LIGHT_CURTAIN_ENABLED':
            value = !this.form.light_curtain ? '1' :'0'
            break;
          case 'FOUPROBOT_SET_SPEED':
            value = this.form.foup_robot_speed
            break;
          case 'CTU_SET_SPEED':
            value = this.form.ctu_speed
            break;
          case 'PTZ_SET_SPEED':
            value = this.form.ptz_speed
            break;
          case 'ELPT_SET_SPEED':
            target = this.form.ELPT
            value = this.form.elpt_speed
            break;
          case 'WHR_SET_SPEED':
            value = this.form.whr_speed
            break;
          case 'WTSALIGNER_SET_SPEED':
            value = this.form.aligner_speed
            break;
          case 'ELPT_READ_RFID':
          case 'ELPT_CLAMP':
          case 'ELPT_UNCLAMP':
          case 'ELPT_OPEN_SHUTTER':
          case 'ELPT_CLOSE_SHUTTER':
          case 'ELPT_MOVE_IN':
          case 'ELPT_MOVE_OUT':
          case 'ELPT_INIT':
          case 'ELPT_RESET':
            target = this.form.ELPT
            break;
          case 'ILPT_LOAD':
            target = this.form.ILPT
            value = this.form.WithMapping ? '1' : '0'
            val2 = this.form.ILPT == 'ILPT1' ? '1' : '2'
            break;
          case 'ILPT_UNLOAD':
          case 'ILPT_INIT':
          case 'ILPT_RESET':
            target = this.form.ILPT
            val2 = this.form.ILPT == 'ILPT1' ? '1' : '2'
            break;
          case 'FOUPROBOT_PREPARE_PICK':
          case 'FOUPROBOT_PICK':
            position = this.form.Source
            break;
          case 'FOUPROBOT_PREPARE_PLACE':
          case 'FOUPROBOT_PLACE':
            position = this.form.Destination
            break;
          case 'WHR_PREPAREPICK':
          case 'WHR_PICK':
          case 'WHR_PREPAREPLACE':
          case 'WHR_PLACE':
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
          case 'WTSALIGNER_ALIGN':
            value = this.form.align_angle
            break;
        }
        let payload = { 'name': TaskName, 'target': target, 'mode': mode, 'station': station, 'direction': direction, 'value': value, 'position': position, 'val2': val2 }
        this.$http.post('/api/Transfer/TaskRun', qs.stringify(payload))
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
        this.alarm_message = data[0].alarm
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
      // Make sure to cleanup SignalR event handlers when removing the component
      this.$signalrHub.$off('On_TaskJob_Ack', this.On_TaskJob_Ack)
      this.$signalrHub.$off('On_TaskJob_Finished', this.On_TaskJob_Finished)
      this.$signalrHub.$off('On_TaskJob_Aborted', this.On_TaskJob_Aborted)
      this.$signalrHub.$off('On_Alarm_Happen', this.On_Alarm_Happen)

      //this.setForm({ form: this.form })
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
