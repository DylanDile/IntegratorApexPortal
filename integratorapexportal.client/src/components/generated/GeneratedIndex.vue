<script>
import {VueAwesomePaginate} from "vue-awesome-paginate";
export default {
  components: {VueAwesomePaginate},
  props: {
    list: {
      type: Array,
      required: true
    }
  },
  data() {
    return {
      page: 1,
      currentPage: 1,
      pageCount: 0,
      perPage: 15,
      filteredGenerated:[],
      loading: false,
      generated: []
    }
  },
  created() {
    this.fetchGenerated()
  },
  mounted() {
    this.fetchGenerated();
  },
  methods: {
    fetchGenerated() {
      this.generated = this.list
      this.filteredGenerated = this.list
    },
    onClickHandler (pageNum){
      this.page = pageNum;
    }
  },
  computed: {
    displayGenerated() {
      const startIndex = this.perPage * (this.page - 1);
      const endIndex = startIndex + this.perPage;
      return this.filteredGenerated.slice(startIndex, endIndex);
    },
  },
}
</script>

<template>
  <div>
    <div class="table-responsive">
      <table class="table table-hover mg-b-0">
        <thead>
        <tr>
          <th>RID</th>
          <th>Institution</th>
          <th>ReportingDate</th>
          <th>Year</th>
          <th>Month</th>
          <th>FileSize</th>
          <th>EMU_Report</th>
          <th>State</th>
          <th>DateGenerated</th>
          <th>Action</th>
        </tr>
        </thead>
        <tbody>
        <tr>
          <th scope="row">0</th>
          <td>Adansi Rural Bank</td>
          <td>Test Submission Pack</td>
          <td>30-04-2024</td>
          <td>2024</td>
          <td>April</td>
          <td>3456009 KB</td>
          <td>processed</td>
          <td>26-04-2024</td>
          <td>
            <router-link to="/view/generated/returns/1">
              <i class="fas fa-eye"></i>
            </router-link>
          </td>
        </tr>
        <tr v-for="generated in displayGenerated">
          <th scope="row">{{ generated.rid}}</th>
          <td>{{ generated.nameOfInstitution}}</td>
          <td>{{ generated.submissionPack}}</td>
          <td>{{ generated.reportingDate }}</td>
          <td>{{ generated.yearYYYY }}</td>
          <td>{{ generated.monthMM }}</td>
          <td>{{ generated.fileSize }} KB</td>
          <td>{{ generated.processed }}</td>
          <td>{{ generated.dateGenerated }}</td>
          <td>
            <router-link :to="`/view/generated/returns/${generated.rid}`">
              <i class="fas fa-eye"></i>
            </router-link>
          </td>
        </tr>
        </tbody>
      </table>
    </div>
    <div :class="['app-content pt-3 p-md-3 p-lg-4' , loading ? 'disabled' : '' ]">
      <div class="row">
        <div style="margin-left: auto; " class="ms-4 row justify-content-center col-sm-12">
          <vue-awesome-paginate
              class="justify-content-center"
              :total-items="filteredGenerated.length"
              :items-per-page="perPage"
              :max-pages-shown="perPage"
              v-model="currentPage"
              :disabled="loading"
              :page-count="Math.ceil(filteredGenerated.length / perPage)"
              :on-click="onClickHandler"
          />
        </div>
      </div>
    </div>

  </div>
</template>
<style>
.pagination-container {
  display: flex;
  column-gap: 10px;
}
.paginate-buttons {
  height: 40px;
  width: 40px;
  border-radius: 20px;
  cursor: pointer;
  background-color: rgb(242, 242, 242);
  border: 1px solid rgb(217, 217, 217);
  color: black;
}
.paginate-buttons:hover {
  background-color: #d8d8d8;
}
.active-page {
  background-color: #3498db;
  border: 1px solid #3498db;
  color: white;
}
.active-page:hover {
  background-color: #2988c8;
}
</style>