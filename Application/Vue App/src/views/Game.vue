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
        <Korisnik class="crveni" v-bind:naPotezu="naPotezu=='crveni'" v-bind:ime="crveniUsername" v-bind:slika="crveniSlika"/>
        <Korisnik class="zeleni" v-bind:naPotezu="naPotezu=='zeleni'" v-bind:ime="zeleniUsername" v-bind:slika="zeleniSlika"/>
        <Korisnik class="zuti" v-bind:naPotezu="naPotezu=='zuti'" v-bind:ime="zutiUsername" v-bind:slika="zutiSlika"/>
        <Korisnik class="plavi" v-bind:naPotezu="naPotezu=='plavi'" v-bind:ime="plaviUsername" v-bind:slika="plaviSlika"/>
        <button  v-on:click="baciKocku" style="position:absolute; top:70%;left:65%;font-size:200px; width:300px; height:300px;" class="btn btn-outline-info" v-bind:class="{'disabled':!started}">{{kocka}}</button>
       </div>
        
        <div v-if="!started" class="pos">
         <div class="loader"></div><center>Waiting for players... </center> 
        </div>

        

    <Chat class="chat" v-bind:accessCode="accessCode" v-bind:started="started" v-bind:gameToken="gameToken" v-bind:privateGame="privateGame"/>
    <div v-if="accessCode!=undefined&&started&&privateGame">
    <button v-on:click="pauzirajIgru" style="margin-left:1px; position: absolute;left: 97.2%;  top: 5%; height:41px;width:46px" class="btn btn-info" type="button"><img  style="height:100%;width:100%" src="../assets/pause.png" alt=""></button>
    </div>
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
import figura from '../models/figura';


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
      connection:"",
      playerCount:1
    }
  },
  props:["accessCode","gameToken", "mojaBoja","username","slika","igraciSlike","igraciImena","privateGame","loginToken","gameState","potez"],
  mounted()
  {   
     this.connection =new HubConnectionBuilder().withUrl("https://localhost:5001/GameHub",{accessTokenFactory:()=>this.gameToken}).build();
      
      this.igra=new Igra();
      this.$data[this.mojaBoja+"Username"]=this.username;
      
      this.$data[this.mojaBoja+"Slika"]="https://localhost:5001/Resources/Images/"+this.slika;
      this.connection.start().then(()=>
        {
          this.connection.invoke("JoinGameGroup");
        })
     
      this.connection.on("userJoined",(username,boja,slika) =>{ 
            this.$data[boja+"Username"]=username;
            this.$data[boja+"Slika"]="https://localhost:5001/Resources/Images/"+slika;
            this.playerCount++;
            if(this.gameState==undefined)
              {
                if(this.sviPrisutni()==true)
                this.started=true;
              }
            else
            {
              if(this.playerCount==4)
              {
                   let loginConfig = { headers: {Authorization: "Bearer " + this.gameToken}}
                   axios.get(`https://localhost:5001/Game/ContinueGame`,loginConfig)
                    .then(() =>
                    {

                    }).catch((err)=>
                    console.log(err)
                    );
              }
            }
         
      })  
      this.connection.on("nastaviIgru",() =>{ 
           this.started=true;   

      })  
      this.connection.on("pauzirajIgru",() =>{ 
           this.connection.invoke("LeaveGameHub");
           router.push({ name: 'PlayGame', params:{loginToken:this.loginToken} })        

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
      this.igraciImena.forEach((igrac,index) => {
      this.$data[Boja.naziv[index]+"Username"]=igrac;
      })

      this.igraciSlike.forEach((igrac,index) => {
      this.$data[Boja.naziv[index]+"Slika"]="https://localhost:5001/Resources/Images/"+igrac;
      })
     
      }
      if(this.gameState!=undefined)
      {
        this.naPotezu=this.potez;
        this.igra.glavnaPolja.map(polje=>polje.figura=null);  
        this.igra.outPolja.map(polje=>polje.figura=null);  
        this.gameState.forEach(el=>{
        if(el.index<0) 
        this.igra.outPolja.find(polje=>polje.index==el.index).figura=new figura(Boja.naziv[el.boja]);
        else
        this.igra.glavnaPolja.find(polje=>polje.index==el.index).figura=new figura(Boja.naziv[el.boja]);


        })
      }
     if(this.sviPrisutni()==true && this.gameState==undefined)
       this.started=true;
 
      
  },
  methods:{
   pauzirajIgru()
   {  if(confirm("Are you sure you want to pause the game?"))
      {
        let loginConfig = { headers: {Authorization: "Bearer " + this.gameToken}}
        axios.get(`https://localhost:5001/Game/PauseGame`,loginConfig)
                    .then(() =>
                    {
                    }).catch(()=>
                    alert("Pausing is currently not available")
                    );
      }
   },
    izabranaFigura(ev)
    {
      if(this.naPotezu==this.mojaBoja&&ev.figura.boja==this.mojaBoja)
      {
         let loginConfig = { headers: {Authorization: "Bearer " + this.gameToken }}
         axios.get(`https://localhost:5001/Game/MoveFigure?figuraIndex=${ev.index}`,loginConfig)
                    .then(() =>
                    {
                    }).catch(err =>console.log(err));
      }
    },
    sviPrisutni()
    { if(this.gameState==undefined)
      {
        if(this.crveniSlika==null||this.plaviSlika==null||this.zutiSlika==null||this.zeleniSlika==null
        || this.crveniUsername==null||this.plaviUsername==null||this.zutiUsername==null||this.zeleniUsername==null)
        return false;
        return true;
      } 
      else
      {
        if(this.playerCount==4)
        return true
        else
        return false;
      }
      
    },
    baciKocku()
    { 
      if(this.naPotezu==this.mojaBoja)
      {
         let loginConfig = { headers: { Authorization: "Bearer " + this.gameToken}}
         axios.get(`https://localhost:5001/Game/ThrowCube`,loginConfig)
                    .then(() =>
                    { 
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
  border-top: 16px solid #17a2b8; /* Blue */
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
  left: 47%;
  top: 40%;
}
.loader
{
  margin-left: 20px;
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
  width: 700px;
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

.disabled
{
  pointer-events:none;
  
}

</style>