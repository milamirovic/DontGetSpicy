<template>
  <div>
  <div class="game d-flex flex-row justify-content-" v-bind:class="{'opacity':!started}">
    <Igra v-bind:gameToken="this.gameToken " v-bind:accessCode="this.accessCode"/>
    <Korisnik class="crveni" v-bind:ime="crveni"/>
    <Korisnik class="zeleni" v-bind:ime="zeleni"/>
    <Korisnik class="zuti" v-bind:ime="zuti"/>
    <Korisnik class="plavi" v-bind:ime="plavi"/>
  </div>
      <div v-if="!started" class="pos">
        Waiting for players to join... <div class="loader"></div>
      </div>

  </div>
</template>

<script>
import {HubConnectionBuilder} from '@microsoft/signalr'
import Igra from "../components/Igra"
import Korisnik from '../components/Korisnik.vue'
import { Boja } from '../models/Consts'



export default {
  name:"Game",
  components:{
    Igra,
    Korisnik
    
  },data() {
    return {
      started: false,
      crveni:"",
      plavi:"",
      zuti:"",
      zeleni:"",
      
    }
  },
  props:["accessCode","gameToken", "Boja","username","guid","igraci"],
  mounted()
  {       
      this.$data[this.Boja]=this.username;
      const connection =new HubConnectionBuilder().withUrl("https://localhost:5001/GameHub").build();
      connection.start().then(()=>
        {
          connection.invoke("JoinGameGroup",this.guid, this.username,this.Boja);
        })
     
      connection.on("userJoined",(username,boja) =>{
            this.$data[boja]=username;
      })

    if(this.igraci==null) return;
    this.igraci.forEach((igrac,index) => {
      this.$data[Boja.naziv[index]]=igrac;
      
    });
   
  }
}
</script>
<style scoped>
*
{
  color: black;
}
.opacity
{
  opacity: 30%;
}
.loader {
  border: 16px solid #f3f3f3; /* Light grey */
  border-top: 16px solid #3498db; /* Blue */
  border-radius: 50%;
  width: 120px;
  height: 120px;
  animation: spin 2s linear infinite;
   
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}
.pos
{
  position: absolute;
  left: 50%;
  top: 40%;
}
.crveni
{
   position: absolute;
  left: 5%;
  top: 10%;
}
.zuti
{
   position: absolute;
  left: 50%;
  top: 80%;
}
.zeleni
{
   position: absolute;
  left: 50%;
  top: 10%;
}
.plavi
{
   position: absolute;
  left: 5%;
  top: 80%;
}
</style>