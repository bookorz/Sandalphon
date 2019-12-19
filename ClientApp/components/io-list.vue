<template>
  <div>
    <h1>{{$t('io.header')}}</h1>
    <b-container fluid>
      <b-row>
        <b-col lg="6" class="my-1">
          <b-form-group label="Filter"
                        label-cols-sm="3"
                        label-align-sm="right"
                        label-size="sm"
                        label-for="filterInput"
                        class="mb-0">
            <b-input-group size="sm">
              <b-form-input v-model="filter"
                            type="search"
                            id="filterInput"
                            placeholder="Type to Search"></b-form-input>
              <b-input-group-append>
                <b-button :disabled="!filter" @click="filter = ''">Clear</b-button>
              </b-input-group-append>
            </b-input-group>
          </b-form-group>
        </b-col>

        <b-col lg="6" class="my-1">
          <b-form-group 
                        label-cols-sm="3"
                        label-align-sm="right"
                        label-size="sm"
                        class="mb-0">
              <b-form-checkbox v-model="autoRefresh">Auto refresh</b-form-checkbox>
          </b-form-group>
        </b-col>
      </b-row>
    </b-container>
    <div v-if="!filtList" class="text-center">
      <p><em>Loading...</em></p>
      <h1><icon icon="spinner" pulse /></h1>
    </div>

    <template v-if="filtList">
      <table class="table">
        <thead class="dark-bg text-white">
          <tr>
            <th>{{$t('io.ioNo')}}</th>
            <th>{{$t('io.ioName')}}</th>
            <th>{{$t('io.ioDesc')}}</th>
            <th>{{$t('io.ioType')}}</th>
            <th>{{$t('io.ioValue')}}</th>
          </tr>
        </thead>

        <tbody class="content">
          <tr :class="index % 2 == 0 ? 'bg-white' : 'bg-light'" v-for="(io, index) in filtList" :key="index">
            <td>{{ io.ioNo }}</td>
            <td>{{ io.ioName }}</td>
            <td>{{ io.ioDesc }}</td>
            <td>{{ io.ioType }}</td>
            <td>{{ io.ioType }}</td>
          </tr>
        </tbody>

      </table>
    </template>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        ioList: null,
        filter: '',
        filtList: null,
        autoRefresh: false
      }
    },
    watch: {
      filter: {
        handler: function (val) {
          if (this.filter != '') {
            this.filtList = this.ioList.filter(e => e.ioName.toLowerCase().includes(this.filter.toLowerCase()))
          } else {
            this.filtList = this.ioList
          }
        }
      }
    },
    methods: {
      refreshData() {
        if (this.autoRefresh) {
          this.loadPage()
        }
      },
      async loadPage() {
        // ES2017 async/await syntax via babel-plugin-transform-async-to-generator
        // TypeScript can also transpile async/await down to ES5
        try {

          let response = await this.$http.get(`/api/Transfer/IO_List`)
          this.ioList = response.data.ioList
          this.filtList = response.data.ioList
        } catch (err) {
          window.alert(err)
          console.log(err)
        }
      },
      content(data) {
        return data.length > this.maxLen ? data.slice(0, this.maxLen) + "..." : data;
      }
    },

    async created() {
      this.loadPage()
      setInterval( this.refreshData, 3 * 1000);
    }
  }</script>

<style>
  body {
    align-items: normal;
  }

  table {
    overflow: auto;
    width: 100%;
    height: 108px; /* 固定高度 */
  }

  td, th {
    border: 1px solid gray;
    width: 100px;
    height: 30px;
  }

  th {
    background-color: #1e0e40;
  }


  table {
    table-layout: fixed;
    width: 200px; /* 固定寬度 */
  }


  thead tr th {
    position: sticky;
    top: 0; /* 列首永遠固定於上 */
  }

  th:first-child {
    z-index: 2;
  }
</style>
