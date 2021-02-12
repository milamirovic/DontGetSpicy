<template>
  <div class="login d-flex flex-column align-items-center my-4">
    <img alt="Vue logo" src="../assets/2.png"  style="width:40%;height:40%;">
    <div class=" rounder-lg d-flex flex-column p-4">
        <input type="text" v-model="username" placeholder="username"/>
        <input type="password" v-model="password" placeholder="password"/>
        <button type="submit" name="Login" class="btn btn-info" v-on:click=Login>Login</button>
    </div>
    
  </div>
</template>

<script>
import axios from "axios"
// @ is an alias to /src
export default {
  name: 'Login',
  data() {
    return {
      username: '',
      password:''
    }
  },
  components: {
  },
  methods:{
    Login()
    {
      axios.post("https://localhost:5001/Korisnik/Login",{email:this.username,password:this.password})
      .then(data =>
      {
        this.$emit("LoginSuccess",data);
      }).catch(err =>console.log(err));
    }
  }
}
</script>
