<script>
import {VueAwesomePaginate} from "vue-awesome-paginate";
export default {
  components: {
    VueAwesomePaginate
  },
   data() {
    return{
      page: 1,
      search: '',
      currentPage: 1,
      pageCount: 0,
      perPage: 20,
      filteredBanks:[],
      loading: false,
      banks:[
        {
          institutionID: 1,
          instName: 'Adansi Rural Bank',
          leaD_CO_CODE: 'GN009809',
          instOperatingStatus: 'active'
        },
        {
          institutionID: 2,
          instName: 'Upper Manya Rural Bank',
          leaD_CO_CODE: 'GN209809',
          instOperatingStatus: 'active'
        },
        {
          institutionID: 3,
          instName: 'Anlo Rural Bank',
          leaD_CO_CODE: 'GN209809',
          instOperatingStatus: 'active'
        },
        {
          institutionID: 4,
          instName: 'Akorobi Manya Rural Bank',
          leaD_CO_CODE: 'GN209809',
          instOperatingStatus: 'active'
        }
      ]
    }
  },
  methods: {
    async getBanks() {
      await window.axios.get('/api/Institutions').then((response) => {
        this.banks = response.data.body.data;
        this.filteredBanks = response.data.body.data;
      })
    },
    onClickHandler (pageNum){
      this.page = pageNum;
    },
    searchBank(){
      if(this.search === ''){
        this.filteredBanks =  this.banks;
        return;
        }

      this.banks = this.banks;

        let filtered = this.banks.filter(bank =>
          bank.instName.toLowerCase().includes(this.search.toLowerCase())
        );

        if(filtered.length === 0){
          this.filteredBanks = [];
          return;
        }

        this.filteredBanks = filtered;
        console.log(this.filteredBanks.length);

    },
    resetBanks(){
        this.filteredBanks = this.banks;
        this.search = '';
    }
  },
  mounted() {
    this.getBanks();
  },
  computed: {
    displayBanks() {
      const startIndex = this.perPage * (this.page - 1);
      const endIndex = startIndex + this.perPage;
      return this.filteredBanks.slice(startIndex, endIndex);
    },
  },

}
</script>

<template>
  <div>
    <div class="mb-4">
      <h3 class="az-dashboard-title">Apex / Rural Banks {{ banks.length }}</h3>
      <p class="az-dashboard-text">List of the banks on the Integrator Api.</p>
    </div>
    <div class="card">
      <div class="card-header">
        <form class="col-sm-12 d-flex flex-row" v-on:submit.prevent="searchBank()">
          <input v-model="search" type="text" name="search" id="search" class="form-control  rounded-50" placeholder="search bank..." />
         <div class="col-sm-3 d-flex flex-row">
           <button type="button" class="btn btn-outline-primary w-100 rounded-50" v-on:click="resetBanks()">
             Refresh
           </button>
           <button class="btn btn-outline-primary w-100 rounded-50" type="submit">
             <i class="fas fa-search mr-1"></i>
             Search
           </button>
         </div>
        </form>
      </div>
      <div class="card-body">
        <div class="row">
          <div class="col-sm-4" v-for="bank in displayBanks">
           <router-link :to="`/generated/returns/institution/${bank.institutionID}`">
             <div class="d-flex flex-row">
               <div class="forum-icon">
                 <span href="" class="az-img-user mr-2">
                      <img src="/apex-icons/icon8.png" alt="">
                </span>
               </div>
               <div class="border-left pl-1">
                 <span class="forum-item-title">{{  bank.instName }}</span>
                 <div class="forum-sub-title">
                   {{ bank.leaD_CO_CODE }}
                   <span class="badge badge-primary float-right ml-3">{{ bank.instOperatingStatus }}</span>
                 </div>

               </div>
             </div>
           </router-link>
            <hr>
          </div>
        </div>
      </div>
    </div>

    <div :class="['app-content pt-3 p-md-3 p-lg-4' , loading ? 'disabled' : '' ]">
      <div class="row">
        <div style="margin-left: auto; " class="ms-4 row justify-content-center col-sm-12">
          <vue-awesome-paginate
              class="justify-content-center"
              :total-items="filteredBanks.length"
              :items-per-page="20"
              :max-pages-shown="20"
              v-model="currentPage"
              :disabled="loading"
              :page-count="Math.ceil(filteredBanks.length / perPage)"
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
