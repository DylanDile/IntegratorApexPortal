<script>
import GeneratedIndex from "./GeneratedIndex.vue";

export default {
  components: {GeneratedIndex},
  data(){
    return{
      institutionId: '',
      generatedReturns: [],
    }
  } ,
  methods: {
    async getSubmissionPacks() {
      await window.axios.get(`/generated_returns/by/institution/${this.institutionId}`).then((response) => {
        this.generatedReturns = response.data.body.list;
        console.log(this.generatedReturns);
      })
    }
  },
  mounted() {
    this.institutionId = this.$route.params.institutionId;
    this.getSubmissionPacks();
  }
}
</script>

<template>
  <div>
    <div class="mb-4">
      <h3 class="az-dashboard-title">Generated Returns / {{  institutionId }}</h3>
      <p class="az-dashboard-text">List Generated Returns by Institution .</p>
    </div>
    <div v-if="generatedReturns.length > 0">
      <hr class="mg-y-30">
      <!-- List here -->
      <GeneratedIndex :list="generatedReturns" />
      <div class="ht-40"></div>
    </div>
  </div>
</template>