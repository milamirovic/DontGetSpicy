<template>
  <div >
  <div class="game d-flex flex-row justify-content-" v-bind:class="{'opacity':!started}">
    <div class="map">
        <div v-bind:key="glPolje.index" v-for="glPolje in igra.glavnaPolja">
               <Polje v-bind:polje="glPolje" v-on:figuraIzabrana="izabranaFigura"/>
        </div>

        <div v-bind:key="outPolje.index" v-for="outPolje in igra.outPolja">
               <Polje v-bind:polje="outPolje" v-on:figuraIzabrana="izabranaFigura"/>
               
        </div>
       
    </div>
    <Korisnik class="crveni" v-bind:class="{'naPotezu':naPotezu=='crveni'}" v-bind:ime="crveni"/>
    <Korisnik class="zeleni" v-bind:class="{'naPotezu':naPotezu=='zeleni'}" v-bind:ime="zeleni"/>
    <Korisnik class="zuti" v-bind:class="{'naPotezu':naPotezu=='zuti'}" v-bind:ime="zuti"/>
    <Korisnik class="plavi" v-bind:class="{'naPotezu':naPotezu=='plavi'}" v-bind:ime="plavi"/>
    <button  v-on:click="baciKocku" style="position:absolute; top:70%;left:65%;font-size:200px; width:300px; height:300px;" class="btn btn-outline-info">{{kocka}}</button>
  </div>
      <div v-if="!started" class="pos">
        Waiting for players to join... <div class="loader"></div>
      </div>
  <Chat v-on:messageSent="sendMsg" v-bind:pristiglePoruke="pristiglePoruke" class="chat"/>
  </div>
</template>

<script>
import Polje from '../components/Polje';
import {HubConnectionBuilder} from '@microsoft/signalr'
import Korisnik from '../components/Korisnik.vue'
import Chat from '../components/Chat.vue'
import { Boja,Next } from '../models/Consts'
import Igra from "../models/igra"
import axios from "axios"
import router from '../router/index.js'
 const connection =new HubConnectionBuilder().withUrl("https://localhost:5001/GameHub").build();
  const connection2 =new HubConnectionBuilder().withUrl("https://localhost:5001/ChatHub").build();

export default {
  name:"Game",
  components:{
    Korisnik,
    Polje,
    Chat
    
  },data() {
    return {
      started: false,
      crveni:null,
      plavi:null,
      zuti:null,
      zeleni:null,
      igra:'',
      kocka:0,
      naPotezu:Boja.naziv[0], 
      pristiglePoruke:[]

    }
  },
  props:["accessCode","gameToken", "mojaBoja","username","guid","igraci"],
  mounted()
  {     
    if(this.accessCode!=undefined)this.pristiglePoruke.push("Access code is: "+this.accessCode);
    connection2.start().then(()=>
        {
          connection2.invoke("StartChat",this.guid);
        })

        connection2.on("userSentMessage",(message, sender) =>{
           
          this.pristiglePoruke.push(sender+" : "+message)
      }) 
        
        
    
      this.igra=new Igra();
      this.$data[this.mojaBoja]=this.username;
      connection.start().then(()=>
        {
          connection.invoke("JoinGameGroup",this.guid, this.username,this.mojaBoja);
        })
     
      connection.on("userJoined",(username,b) =>{
            this.$data[b]=username;
            if(this.sviPrisutni()==true)
            this.started=true;

      })  

      connection.on("krajIgre",(pobednik) =>{
            alert("pobednik je: "+pobednik);
          setTimeout(()=>
          {
             router.push({ name: 'Home' })
          },10000).catch(err =>console.log(err));
              });

      
      connection.on("kockaBacena",(vrKocke, next) =>{
           this.kocka=vrKocke;
            if(next==true)
            {
              this.naPotezu=Next[this.naPotezu]; return;
            }
            if(this.naPotezu==this.mojaBoja)
           { 
             alert("izaberite figuru");
           }
         
      }) 
       connection.on("figuraPomerena",(potezi, next) =>{
          
          if(next==true)
           this.naPotezu=Next[this.naPotezu];
          
          this.igra.odigrajPotez({staraPozicija:potezi[0].item1,novaPozicija:potezi[0].item2}, (potezi[1]===undefined)?null:{staraPozicija:potezi[1].item1,novaPozicija:potezi[1].item2})
         

      }) 
     if(this.igraci!=null) 
     {
      this.igraci.forEach((igrac,index) => {
      this.$data[Boja.naziv[index]]=igrac;
      })
      }
      
     if(this.sviPrisutni()==true)
          this.started=true;
  },
  methods:{
    sendMsg(ev)
    {
     // console.log(ev);
        connection2.invoke("SendMessage",this.guid, ev, this.username);
    },
    izabranaFigura(ev)
    {
      if(this.naPotezu==this.mojaBoja&&ev.figura.boja==this.mojaBoja)
      {
         let loginConfig = { headers: {
                   Authorization: "Bearer " + this.gameToken
                     }
                        }
         axios.get(`https://localhost:5001/Game/MoveFigure?figuraIndex=${ev.index}`,loginConfig)
                    .then(() =>
                    { 
                      // console.log("figura pomerena");
                    }).catch(err =>console.log(err));
      }
    },
    sviPrisutni()
    { 
      if(this.crveni==null||this.plavi==null||this.zuti==null||this.zeleni==null)
      return false;
      return true;
    },
    baciKocku()
    { 
      if(this.naPotezu==this.mojaBoja)
      {
         let loginConfig = { headers: {
                   Authorization: "Bearer " + this.gameToken
                     }
                        }
         axios.get(`https://localhost:5001/Game/ThrowCube`,loginConfig)
                    .then(() =>
                    { 
                       //console.log("kocka bacena");
                    }).catch(err =>console.log(err));
      }
    }
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
.chat
{
   position: absolute;
  left: 55%;
  top: 20%;
  width: 500px;
}

.map{
    background-image: url("../assets/map.jpg");
    background-repeat: no-repeat;
    background-position-x: 50px;
     background-position-y: 0px;
     width: 900px;
    height: 650px;
    margin-left: 230px;
     margin-top: 150px;
    
    
}
.naPotezu
{
  border: 5px solid red;
}
</style>