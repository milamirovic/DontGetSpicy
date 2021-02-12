<template>
  <div id="app">
    <div id="nav" class="bg-info" v-if="this.$route.path !== '/game'">
      <router-link to="/" class="mx-2">Home</router-link> 
      <router-link to="/login" class="mx-2">Login</router-link>  
      <router-link to="/signup" class="mx-2">Signup</router-link>
    </div>
    <router-view v-on:LoginSuccess="test"/>
-  </div>
</template>




<script>
import router from './router/index.js'
export default {
  name:"App",
  data() {
    return {
      loginToken: ''
      
    }
  },
  methods:{
    test(data)
    {
      this.loginToken=data.data.tokenStr;
      router.push({ name: 'JoinGame', params: { loginToken: this.loginToken } })
      
      
    }
  }
}
</script>










<style>



@import url('https://fonts.googleapis.com/css?family=Varela+Round');

    #app {
      font-family: 'Varela Round', Helvetica, Arial, sans-serif;
      -webkit-font-smoothing: antialiased;
      -moz-osx-font-smoothing: grayscale;
      color: #f5f9fd;
    }

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #edae49;
}
</style>
