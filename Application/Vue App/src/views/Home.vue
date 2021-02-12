<template>
  <div id="home" class="m-4 d-flex flex-column" thumbnail>
    <Igre v-bind:igre="igre" />
  </div>
</template>

<script>

import Igre from '../components/Igre';
export default {
  name: 'home',
  components: {
    Igre
  },
  data() {
    return {
      igre: [
        {
          id: 1,
          title: "Igra1", 
          complited: false
        },
        {
          id: 2,
          title: "Igra2", 
          complited: true
        },
        {
          id: 3,
          title: "Igra3", 
          complited: false
        }
      ]
    }
  }
}
</script>

<style>

@import url('https://fonts.googleapis.com/css?family=Varela+Round');

    #home {
            background-color: #17A2B8;
      font-family: 'Varela Round', Helvetica, Arial, sans-serif;
      -webkit-font-smoothing: antialiased;
      -moz-osx-font-smoothing: grayscale;
      color: #2c3e50;
    }

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #dae4ee;
}

#nav a.router-link-exact-active {
  color: #edae49;
}
</style>
