<template>
  <div id="home" class="m-4 d-flex flex-column" thumbnail>
    <section class="hero is-small bg-info" style="max-height:85vh;">
        <div class="hero-body d-flex flex-column">
            <div class="d-flex flex-row justify-content-around" style="margin-top:100px;">
                <img src="../assets/2.png" class="w-50 h-50">
                <div id="dodatak" class="ml-5 d-flex flex-column align-items-start pl-5 w-50 h-50">
                    <h2 class="display-4 align-self-center title text-light animate__animated animate__pulse animate__infinite	infinite">Welcome!</h2>
                    <p>This is a collaborative real-time web app for playing Ludo game. </p>
                    <p>Don'tGetSpicy is a student project.</p>
                    <div class="d-flex flex-row align-self-center mt-5">
                        <a href="https://github.com/OgnjenDamnjanovic/DontGetSpicy"><button type="button" class="btn btn-dark m-1">
                            <i class="fa fa-github" aria-hidden="true"></i>
                            github
                        </button></a>
                        <a href="https://en.wikipedia.org/wiki/Mensch_%C3%A4rgere_Dich_nicht?fbclid=IwAR0vfMQn67Qoe5n9yZQsGHAlrvJH-FRRSICgjRynYucr8-TIJrOiaDq-zr4"><button type="button" class="btn btn-dark m-1">
                            <i class="fa fa-gamepad" aria-hidden="true"></i>
                            rules
                        </button></a>
                    </div>
                    <div id="uputstva" class="mt-5 p-5 border border-dark rounded-lg w-100">
                        <ol class="d-flex flex-column align-items-start ml-5">
                            <li>Before you start a game, you need to log in!</li>
                            <li>If you don't have an account already, you need to sign up!</li>
                            <li>When you log in, you can start a new game!</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        </section>
  </div>
</template>

<script>
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

export default {
  name: 'home',
  components: {
   
  },
  data() {
    return {
     
    }
  }
}
</script>

<style>

@import url('https://fonts.googleapis.com/css?family=Varela+Round');

@import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@300&display=swap');
p{
      font-family: 'Montserrat', sans-serif;
      font-size: 20px;
      color:#c3daf1;
}
button, h2{
          font-family: 'Montserrat', sans-serif;
}
li{
      font-family: 'Montserrat', sans-serif;
      font-size: 15px;
      color:#c3daf1;
      margin-bottom: 5px;
}
#uputstva{
    background-color:#17A2B8!important;
    border-color: #17A2B8!important;
    color: #363636;
    border-radius: 20px!important;
    box-shadow: inset 3px 3px 10px 10px rgba(12, 0, 0, 0.1), inset -3px -3px 5px 0px #2ACBDB!important;

}

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
