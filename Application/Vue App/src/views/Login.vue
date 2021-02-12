<template>
<div class="glavni d-flex align-items-center">

  <div class="login w-50 border border-info login d-flex flex-row justify-content-center my-4">
    <div class="align-self-center rounder-lg d-flex flex-column align-items-center p-4">
      <img src="../assets/login.png" class="w-25 h-25"/>
      <input type="text" v-model="username" placeholder="username" class="input is-rounded my-2" />
      <input type="password" v-model="password" placeholder="password" class="input is-rounded my-2" />
      <button type="submit" name="Login" class="mt-3 btn btn-info w-100" v-on:click=Login>Login</button>
    </div>
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

<style scoped>  
@import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@300&display=swap');
button{
  box-shadow: 5px 5px 10px 0px rgba(0,0,0,0.1), -5px -5px 10px 0px #2ACBDB!important; 
  border-color: #17A2B8; 
  background-color: #371A45; 
  font-weight: 600; color: #363636; 
  border-width: 1px;
  font-family: 'Montserrat', sans-serif;
  color: #9ce2ec;
}
.login{
  /* box-shadow: 1px 5px 50px 1px #108294; */
  margin:auto;
    background-color: #17A2B8;
    border-radius: 15px;

}
.glavni{
  height:600px;
}

input{
  background-color: #17A2B8!important;
    border-color: #17A2B8!important;
    color: #363636;
    border-radius: 40px!important;
    box-shadow: inset 3px 3px 5px 0px rgba(0,0,0,0.1), inset -3px -3px 5px 0px #2ACBDB!important;
}

input:hover{
  filter: brightness(95%);
}

::placeholder {
  color: #45daeb;
  opacity: 1; /* Firefox */
font-family: 'Montserrat', sans-serif;
}




</style>
