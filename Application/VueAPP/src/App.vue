<template>
  <div id="app">
    <div id="nav" class="bg-info">
      <router-link to="/" class="mx-2">Home</router-link> 
      <router-link to="/about" class="mx-2">About</router-link>  
      <router-link to="/login" class="mx-2">Login</router-link>  
      <router-link to="/signup" class="mx-2">Signup</router-link>
    </div>
    <router-view/>
-  </div>
</template>

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
