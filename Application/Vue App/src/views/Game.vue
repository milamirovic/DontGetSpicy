<template>
  <div >
  <div class="game d-flex flex-row justify-content-" v-bind:class="{'opacity':!started}">
    <div class="map" v-bind:class="{'disabled':!started}">
        <div v-bind:key="glPolje.index" v-for="glPolje in igra.glavnaPolja">
               <Polje v-bind:polje="glPolje" v-on:figuraIzabrana="izabranaFigura"/>
        </div>
        <div v-bind:key="outPolje.index" v-for="outPolje in igra.outPolja">
               <Polje v-bind:polje="outPolje" v-on:figuraIzabrana="izabranaFigura"/> 
        </div>       
    </div>
    <Korisnik class="crveni" v-bind:class="{'naPotezu':naPotezu=='crveni'}" v-bind:ime="crveniUsername" v-bind:slika="crveniSlika"/>
    <Korisnik class="zeleni" v-bind:class="{'naPotezu':naPotezu=='zeleni'}" v-bind:ime="zeleniUsername" v-bind:slika="zeleniSlika"/>
    <Korisnik class="zuti" v-bind:class="{'naPotezu':naPotezu=='zuti'}" v-bind:ime="zutiUsername" v-bind:slika="zutiSlika"/>
    <Korisnik class="plavi" v-bind:class="{'naPotezu':naPotezu=='plavi'}" v-bind:ime="plaviUsername" v-bind:slika="plaviSlika"/>
    <button  v-on:click="baciKocku" style="position:absolute; top:70%;left:65%;font-size:200px; width:300px; height:300px;" class="btn btn-outline-info" v-bind:class="{'disabled':!started}">{{kocka}}</button>
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


export default {
  name:"Game",
  components:{
    Korisnik,
    Polje,
    Chat
    
  },data() {
    return {
      started: false,
      kocka:0,
      naPotezu:Boja.naziv[0],
      crveniSlika:null,
      plaviSlika:null,
      zutiSlika:null,
      zeleniSlika:null,
      crveniUsername:null,
      plaviUsername:null,
      zutiUsername:null,
      zeleniUsername:null,
      igra:'',
      pristiglePoruke:[],
      connection:"",
      connection2:""

    }
  },
  props:["accessCode","gameToken", "mojaBoja","username","slika","igraciSlike","igraciImena"],
  mounted()
  {   
     this.connection =new HubConnectionBuilder().withUrl("https://localhost:5001/GameHub",{accessTokenFactory:()=>this.gameToken}).build();
     this.connection2 =new HubConnectionBuilder().withUrl("https://localhost:5001/ChatHub",{accessTokenFactory:()=>this.gameToken}).build();

     if(this.accessCode!=undefined)this.pristiglePoruke.push("Access code is: "+this.accessCode);

    this.connection2.start().then(()=>
        {
          this.connection2.invoke("StartChat");
        })

        this.connection2.on("userSentMessage",(message, sender) =>{
           
          this.pristiglePoruke.unshift(sender+" :   "+message)
      }) 
        
        
    
      this.igra=new Igra();
      this.$data[this.mojaBoja+"Username"]=this.username;
      this.$data[this.mojaBoja+"Slika"]="http://localhost:5000/Resources/Images/"+this.slika;
      this.connection.start().then(()=>
        {
          this.connection.invoke("JoinGameGroup");
        })
     
      this.connection.on("userJoined",(username,boja,slika) =>{ 
            this.$data[boja+"Username"]=username;
            this.$data[boja+"Slika"]="http://localhost:5000/Resources/Images/"+slika;
            if(this.sviPrisutni()==true)
            this.started=true;

      })  

      this.connection.on("krajIgre",(pobednik) =>{
            alert("pobednik je: "+pobednik);
          setTimeout(()=>
          {
             router.push({ name: 'Home' })
          },10000).catch(err =>console.log(err));
              });

      
      this.connection.on("kockaBacena",(vrKocke, next) =>{
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
       this.connection.on("figuraPomerena",(potezi, next) =>{
          
          if(next==true)
           this.naPotezu=Next[this.naPotezu];
          
          this.igra.odigrajPotez({staraPozicija:potezi[0].item1,novaPozicija:potezi[0].item2}, (potezi[1]===undefined)?null:{staraPozicija:potezi[1].item1,novaPozicija:potezi[1].item2})
         

      }) 
     
     if(this.igraciImena!=null&&this.igraciSlike!=null) 
     {
       console.log(this.igraciImena);
       console.log(this.igraciSlike);
      this.igraciImena.forEach((igrac,index) => {
      this.$data[Boja.naziv[index]+"Username"]=igrac;
      })

      this.igraciSlike.forEach((igrac,index) => {
      this.$data[Boja.naziv[index]+"Slika"]="http://localhost:5000/Resources/Images/"+igrac;
      })
      }
      
     if(this.sviPrisutni()==true)
          this.started=true;
  },
  methods:{
    sendMsg(msg)
    {
        this.connection2.invoke("SendMessage", msg);
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
      if(this.crveniSlika==null||this.plaviSlika==null||this.zutiSlika==null||this.zeleniSlika==null
        || this.crveniUsername==null||this.plaviUsername==null||this.zutiUsername==null||this.zeleniUsername==null)
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
  left: 65%;
  top: 5%;
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
.disabled
{
  pointer-events:none;
}

</style>