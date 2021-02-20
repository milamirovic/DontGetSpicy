<template>
  <div id="app">
    <div id="nav" class="bg-info" v-if="this.$route.path !== '/game'">
      <router-link to="/" class="mx-2">Don'tGetSpicy</router-link> 
      <router-link to="/login" class="mx-2" v-if="this.loginToken==''">Login</router-link>  
      <router-link to="/signup" class="mx-2" v-if="this.loginToken==''">Signup</router-link>
      <router-link :to="{name:'JoinGame', params:{loginToken:this.loginToken}}"  class="mx-2" v-if="this.loginToken!==''">Join Game</router-link>
      <router-link :to="{name:'Profile', params:{loginToken:this.loginToken}}" v-if="this.loginToken!==''">Profile</router-link>
      
    </div>
    <router-view v-on:LoginSuccess="loginRedirect"/>
  </div>
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
    loginRedirect(data)
    {
      this.loginToken=data.data.tokenStr;
      router.push({ name: 'Home', params: { loginToken: this.loginToken } })
      
      
    },
   
    
  }
}
</script>

<style>

@import url('https://fonts.googleapis.com/css?family=Varela+Round');

    #app {
      font-family: 'Varela Round', Helvetica, Arial, sans-serif;
      -webkit-font-smoothing: antialiased;
      -moz-osx-font-smoothing: grayscale;
      color: #1d3751;
      
    }

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #a5d0fc;
}

#nav a:hover {
  color: #dceeff;
}

#nav a.router-link-exact-active {
  color: #edae49;
}


</style>
