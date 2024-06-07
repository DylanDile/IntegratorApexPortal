<script>
import GeneratedIndex from "./GeneratedIndex.vue";

export default {
  components: {GeneratedIndex},
  data(){
    return{
      submissionPack: '',
      generatedReturns:[]
    }
  },
  methods: {
    async getGeneratedReturns() {
      await window.axios.get(`/generated_returns/by/submission_pack/${this.submissionPack}`).then((response) => {
        this.generatedReturns = response.data.body.list;
      })
    }
  },
  mounted() {
    this.submissionPack = this.$route.params.submissionPack;
    this.getGeneratedReturns();
  }
}
</script>

<template>
  <div>
    <div class="mb-4">
      <h3 class="az-dashboard-title">Generated Returns / {{  submissionPack }}</h3>
      <p class="az-dashboard-text">List Generated Returns by Submission Pack .</p>
    </div>
    <div>
      <hr class="mg-y-30">
      <div class="table-responsive" v-if="generatedReturns.length > 0">
        <GeneratedIndex :list="generatedReturns" />
      </div><!-- table-responsive -->

      <div class="ht-40"></div>
    </div>
  </div>
</template>