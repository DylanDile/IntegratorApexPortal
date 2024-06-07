<script>
export default {
  data(){
    return{
      submissionPacks:[
        'MAFI Submission Pack',
        'MNB Submission Pack',
        'WNB Submission Pack',
        'QAFI Submission Pack'
      ]
    }
  },
  methods: {
    getSubmissionPacks(){
      window.axios.get('/api/Returns/submissionpacks').then((response) => {
        this.submissionPacks = response.data.body.list;
      })
    }
  },
  mounted() {
    this.getSubmissionPacks();
  }
}
</script>

<template>
  <div>
    <div class="az-dashboard-one-title">
      <div>
        <h3 class="az-dashboard-title">Submission Packs / {{ submissionPacks.length }}</h3>
      </div>
    </div>
    <div>
      <hr>
      <div class="row">
        <div class="col-xl-4 col-lg-6 mt-3" v-for="pack in submissionPacks">
          <router-link :to="`/generated/returns/submission/${pack}`" >
            <div class="card card-stats mb-4 mb-xl-0">
              <div class="card-body">
                <div class="row">
                  <div class="col">
                    <h5 class="h6 text-uppercase text-muted mb-0">{{ pack }}</h5>
                    <span class="badge badge-primary">{{ 'active' }}</span>
                  </div>
                  <div class="col-auto">
                    <div class="icon icon-shape">
                      <!--                    <i class="fas fa-file-archive"></i>-->
                      <a href="" class="az-img-user">
                        <img src="/apex-icons/icon3.png" alt="">
                      </a>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>