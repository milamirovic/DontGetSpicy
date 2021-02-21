<template>
    <div style="width:100%" class="d-flex my-4"><br><br>
        <div class="mx-5">
        <div class="border border-info p-2 w-50" >
            <h2>Create new game</h2>
            <button class="btn btn-info" v-on:click="newGame">New game</button>
        </div><br><br>
        <div class="border border-info p-2 w-75" >
        <h2 style="color:black">Join Game</h2><br>
        <h2 style="color:black">Game code:<input type="text" v-model="accessCode" ></h2>
        <button class="btn btn-info" v-on:click="joinGame">Join game</button><br><br>
        </div>
        </div>
         <BojaSelector  v-on:colorChanged="promeniBoju" v-bind:crveniZauzet="crveniZauzet" v-bind:zeleniZauzet="zeleniZauzet" v-bind:zutiZauzet="zutiZauzet" v-bind:plaviZauzet="plaviZauzet" />

    </div>
</template>

<script>
import router from '../router/index.js'
import BojaSelector from "../components/BojaSelector"
import axios from "axios"
export default {
    name:"JoinGame",
    components:{
        BojaSelector
    },
    data() {
    return {
      accessCode: '',
      selektovanaBoja:'crveni',
      crveniZauzet:false,
      zeleniZauzet:false,
      zutiZauzet:false,
      plaviZauzet:false
    }
  },
    methods:{
        promeniBoju(ev)
        {
               this.selektovanaBoja=ev;
        },
        newGame()
        {   let boja=this.selektovanaBoja;
            let loginConfig = { headers: {
                   Authorization: "Bearer " + this.loginToken
                     }
                        }
                     axios.get(`https://localhost:5001/Game/NewGame?boja=${boja}`,loginConfig)
                    .then((data) =>
                    { 
                        router.push({ name: 'Game', params:{accessCode:data.data.accessCode, gameToken:data.data.token, mojaBoja:this.selektovanaBoja, username:data.data.username, slika:data.data.slika,igraciImena:null,igraciSlike:null} })
                    }).catch(err =>console.log(err));
        },
        joinGame()
        {   
            let boja=this.selektovanaBoja;
            let loginConfig = { headers: {
                   Authorization: "Bearer " + this.loginToken
                     }}
            axios.get(`https://localhost:5001/Game/JoinGame?boja=${boja}&accessCode=${this.accessCode}`,loginConfig)
            .then((data) =>
        { console.log(data);
        router.push({ name: 'Game', params:{gameToken:data.data.token,mojaBoja:this.selektovanaBoja,username:data.data.username,slika:data.data.slika,igraciImena:data.data.igraciImena,igraciSlike:data.data.igraciSlike} })
         }).catch(err =>
         {
            if(err.response.status==403)
            {    alert("Color already taken! Pick again")
                let zauzeteBoje=err.response.data;
                 zauzeteBoje.forEach(element => {
                     this.$data[element+"Zauzet"]=true
                 });
            }

         });
        }


    },
    props:["loginToken"],
    mounted()
    {
       
    }
}
</script>
<style scoped>
h2
{
    color: black;
}

</style>