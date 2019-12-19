<template>
  <div>
    <h1>{{$t('almhis.header')}}</h1>

    <div v-if="!alarmList" class="text-center">
      <p><em>Loading...</em></p>
      <h1><icon icon="spinner" pulse /></h1>
    </div>

    <template v-if="alarmList">
      <table class="table">
        <thead class="dark-bg text-white">
          <tr>
            <th>{{$t('almhis.node_name')}}</th>
            <th>{{$t('almhis.err_code')}}</th>
            <th>{{$t('almhis.err_desc')}}</th>
            <th>{{$t('almhis.timestamp')}}</th>
          </tr>
        </thead>
        <tbody>
          <tr :class="index % 2 == 0 ? 'bg-white' : 'bg-light'" v-for="(alarm, index) in alarmList" :key="index" @click="showDetail(alarm)">
            <td>{{ alarm.nodeName }}</td>
            <td>{{ content(alarm.errorCode) }}</td>
            <td>{{ content(alarm.errorDesc) }}</td>
            <td>{{ alarm.timeStamp | moment("YYYY/MM/DD HH:mm:ss") }}</td>
          </tr>
        </tbody>
      </table>
      <nav aria-label="...">
        <ul class="pagination justify-content-center">
          <li :class="'page-item' + (currentPage == 1 ? ' disabled' : '')">
            <a class="page-link" href="#" tabindex="-1" @click="loadPage(currentPage - 1)">Previous</a>
          </li>
          <li :class="'page-item' + (n == currentPage ? ' active' : '')" v-for="(n, index) in totalPages" :key="index">
            <a class="page-link" href="#" @click="loadPage(n)">{{n}}</a>
          </li>
          <li :class="'page-item' + (currentPage < totalPages ? '' : ' disabled')">
            <a class="page-link" href="#" @click="loadPage(currentPage + 1)">Next</a>
          </li>
        </ul>
      </nav>
    </template>
    <b-modal id="detail_modal" 
             header-bg-variant="dark"
             header-text-variant="light"
             centered hide-footer>
      <template v-slot:modal-title>
        Alarm Detail
      </template>
      <b-container fluid>
        <b-row class="mb-1">
          <b-col cols="3">Node:</b-col>
          <b-col>
            {{alarm_detail.nodeName}}
          </b-col>
        </b-row>
        <b-row class="mb-1">
          <b-col cols="3">Controller:</b-col>
          <b-col>
            {{alarm_detail.controller}}
          </b-col>
        </b-row>
        <b-row class="mb-1">
          <b-col cols="3">Address No:</b-col>
          <b-col>
            {{alarm_detail.addressNo}}
          </b-col>
        </b-row>
        <b-row class="mb-1">
          <b-col cols="3">Error Code:</b-col>
          <b-col>
            {{alarm_detail.errorCode}}
          </b-col>
        </b-row>
        <b-row class="mb-1">
          <b-col cols="3">Error Desc:</b-col>
          <b-col>
            {{alarm_detail.errorDesc}}
          </b-col>
        </b-row>
      </b-container>
      <b-button class="mt-3" block @click="$bvModal.hide('detail_modal')">Close Me</b-button>
    </b-modal>
  </div>
</template>

<script>
  export default {
    computed: {
      totalPages: function () {
        return Math.ceil(this.total / this.pageSize)
      }
    },
    data() {
      return {
        alarmList: null,
        total: 0,
        pageSize: 15,
        currentPage: 1,
        maxLen: 40,
        alarm_detail: ''
      }
    },
    methods: {
      async loadPage(page) {
        // ES2017 async/await syntax via babel-plugin-transform-async-to-generator
        // TypeScript can also transpile async/await down to ES5
        this.currentPage = page

        try {
          var from = (page - 1) * (this.pageSize)
          var to = from + this.pageSize
          let response = await this.$http.get(`/api/Transfer/AlarmHistory?from=${from}&to=${to}`)
          this.alarmList = response.data.alarmList
          this.total = response.data.total
        } catch (err) {
          window.alert(err)
          console.log(err)
        }
        // Old promise-based approach
        // this.$http
        //    .get('/api/SampleData/WeatherForecasts')
        //    .then(response => {
        //        console.log(response.data)
        //        this.forecasts = response.data
        //    })
        //    .catch((error) => console.log(error))*/
      },
      content(data) {
        return data.length > this.maxLen ? data.slice(0, this.maxLen) + "..." : data;
      },
      showDetail(data) {
        this.alarm_detail = data
        this.$bvModal.show('detail_modal')
      }
    },

    async created() {
      this.loadPage(1)
    }
  }</script>

<style>
</style>
