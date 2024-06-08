<script>
import Form from "../../core/form.js";

export default {
  name: "Login",
  data() {
    return {
     form: new Form({
       email: "",
       password: ""
     })
    };
  },
  methods: {
    login() {
      this.form.submit( "/login").then(() => {

      });
    }
  }
}
</script>

<template>
<div>
  <div class="az-signin-wrapper">
    <div class="az-card-signin">
      <div class="text-center d-flex flex-row justify-content-around">
        <h2 class="text-center text-primary font-weight-bold">ARB Apex</h2>
      </div>
      <div class="az-signin-header">
        <h4>Please sign in to continue</h4>

        <form v-on:submit.prevent="login">
          <div class="form-group">
            <label>Email</label>
            <input type="email" :class="[ 'form-control' , form.errors.get('email') ? 'is-invalid' : '' ]"
                   v-model="form.email" name="email"
                   placeholder="Enter your email">
            <div v-text="form.errors.get('email')" class="invalid-feedback"/>
          </div><!-- form-group -->
          <div class="form-group">
            <label>Password</label>
            <input type="password" :class="[ 'form-control' , form.errors.get('password') ? 'is-invalid' : '' ]"
                   v-model="form.password"  name="password"
                   placeholder="Enter your password">
            <div v-text="form.errors.get('password')" class="invalid-feedback"/>
          </div><!-- form-group -->
          <button class="btn btn-az-primary btn-block" type="submit">Sign In</button>
        </form>
      </div><!-- az-signin-header -->
      <div class="az-signin-footer">
        <p><a href="">Forgot password?</a></p>
        <p>Don't have an account? <a >Contact Apex Bank Administrators</a></p>
      </div><!-- az-signin-footer -->
    </div><!-- az-card-signin -->
  </div>
</div>
</template>