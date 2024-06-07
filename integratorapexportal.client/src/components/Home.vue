<script>
export default {
  data(){
    return{
      generatedReturns:[
        {
          returnPack: 'MAFI Submission Pack',
          returnsCount: 10
        },
        {
          returnPack: 'MNB Submission Pack',
          returnsCount: 20
        },
        {
          returnPack: 'WNB Submission Pack',
          returnsCount: 30
        },
        {
          returnPack: 'QAFI Submission Pack',
          returnsCount: 40
        }
      ]
    }
  },
  methods: {
    getReturnsBySubmissionPack(pack){
      window.axios.get(`/api/generated_returns/submission_pack/consolidated`).then((response) => {
        this.generatedReturns = response.data.body.list;
      })
      },
      test() {
         
          window.axios.get(`/api/Institutions`)
              .then((response) => {
                 console.log(response);
              })
              .catch((error) => {
                    console.log(error);
                });
      }
    
  },
  mounted() {
      this.test();
      this.getReturnsBySubmissionPack();
  }
}
</script>

<template>
  <div>
    <div class="az-dashboard-one-title">
      <div>
        <h class="az-dashboard-title">Hi, welcome back!</h>
        <p class="az-dashboard-text">ORASS Returns analytics dashboard and reporting.</p>
      </div>
      <div class="az-content-header-right">
        <div class="media">
          <div class="media-body">
            <label>Start Date</label>
            <h6>Oct 10, 2018</h6>
          </div><!-- media-body -->
        </div><!-- media -->
        <div class="media">
          <div class="media-body">
            <label>End Date</label>
            <h6>Oct 23, 2018</h6>
          </div><!-- media-body -->
        </div><!-- media -->
        <div class="media">
          <div class="media-body">
            <label>Event Category</label>
            <h6>All Categories</h6>
          </div><!-- media-body -->
        </div><!-- media -->
        <router-link to="/returns" href="" class="btn btn-primary">Returns</router-link>
      </div>
    </div>
    <div>
      <h3 class="text-primary mb-3"> Generated Returns</h3>
      <hr>
      <div class="row">
        <div class="col-xl-3 col-lg-6 mt-3" v-for="pack in generatedReturns">
          <div class="card card-stats mb-4 mb-xl-0">
            <div class="card-body">
              <div class="row">
                <div class="col">
                  <h5 class="h6 text-uppercase text-muted mb-0">{{ pack.returnPack }}</h5>
                </div>
                <div class="col-auto">
                  <div class="icon icon-shape shadow">
                    <i class="fas fa-chart-bar"></i>
                  </div>
                </div>
              </div>
              <p class="mt-3 mb-0 text-muted text-sm">
                <span class="text-success mr-2">
                  <i class="fa fa-calendar mr-3"></i>April
                </span>
                <span class="text-nowrap">2024</span>
                <span class="h5 font-weight-bold mb-0 float-right">{{ pack.returnsCount }}</span>
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>