<template>
    <div>
        <div style="width:100%" class="d-flex my-4"><br><br>
                    <div class="border border-info p-2 mx-5" style="width:13%" > 
                        <u><h2>Join public game:</h2></u>
                        <center><img src="../assets/globe.jpg" width="60px" height="60px" style="margin-top:15px;margin-bottom:15px"> <br></center>
                        <center><button class="btn btn-info" v-on:click="joinPublic">Join game</button></center>
                    </div><br><br>
                <div class="" style="display:flex;flex-direction:row;">
                    <div class="border border-info p-2 w-75 mx-5" > 
                        <u><h2>Create private game:</h2></u><br>
                        <center><img src="../assets/friends.png" width="60px" height="60px" style="margin-top:0px;margin-bottom:5px;"> <br></center>
                        <center><button class="btn btn-info" v-on:click="newGame" style="margin-top:20px">New game</button></center>
                    </div><br><br>
                    
                    <div class="border border-info p-2 w-75 mx-5">
                    <u><h2 style="color:black">Play with friends:</h2></u><br>
                    <h2 style="color:black">Game code:<input type="text" v-model="accessCode" ></h2><br>
                    <center><button class="btn btn-info" v-on:click="joinGame">Join game</button><br><br></center>
                    </div>

                    
                </div>
            
            <br>
            
        </div>
        <div class="border border-info p-2 mx-5" style="width:13%">
                    <u><h2 style="color:black">Resume game:</h2></u><br>
                    <h2 style="color:black">Game code:<input type="text" v-model="accessCodeResume" ></h2><br>
                    <center><button class="btn btn-info" v-on:click="resumeGame">Resume</button><br><br></center>
         </div><br>
        <div class="border border-info p-2 mx-5" style="width:fit-content" >
            <u><h2>My Games:</h2></u><br> 
            <div class="joinableContainer" style="display:flex;flex-direction:row;flex-wrap:wrap; ">
                <div v-bind:key="game.id" v-for="game in mygames" >
                    <GameInfo v-bind:gameData="game" v-bind:loginToken="loginToken" />
                </div>
            </div>
        </div>
        
    </div>
    
</template>

<script>//v-on:figuraIzabrana="izabranaFigura"
import router from '../router/index.js'
import GameInfo from "../components/GameInfo"
import axios from "axios"
export default {
    name:"PlayGame",
    components:{
        GameInfo
    },
    data() {
    return {
      accessCode: '',
      accessCodeResume:'',
      mygames:[]
    }
  },
    methods:{
        resumeGame()
        {
            let loginConfig = { headers: {Authorization: "Bearer " + this.loginToken}}
            axios.get(`https://localhost:5001/Game/RejoinGame?id=${this.accessCodeResume}`,loginConfig)
                    .then((data) =>
                    {
                        router.push({ name: 'Game', params:{loginToken:this.loginToken,accessCode:data.data.accessCode, gameToken:data.data.token, mojaBoja:data.data.boja, username:data.data.username, slika:data.data.slika,igraciImena:data.data.igraciImena,igraciSlike:data.data.igraciSlike,privateGame:false, gameState:data.data.figure, potez:data.data.naPotezu} })
                    }).catch(err =>console.log(err));
        },
        newGame()
        {   router.push({ name: 'SelectColor', params:{call:(boja,privateGame,accessCode,loginToken)=>{
            console.log(accessCode);
            let loginConfig = { headers: {
                   Authorization: "Bearer " + loginToken
                     }
                        }
                     axios.get(`https://localhost:5001/Game/NewGame?boja=${boja}&privateGame=${privateGame}`,loginConfig)
                    .then((data) =>
                    { 
                        router.push({ name: 'Game', params:{loginToken:this.loginToken,accessCode:data.data.accessCode, gameToken:data.data.token, mojaBoja:boja, username:data.data.username, slika:data.data.slika,igraciImena:null,igraciSlike:null,privateGame:privateGame} })
                    }).catch(err =>console.log(err));
            },privateGame:true,accessCode:this.accessCode,loginToken:this.loginToken }})
            
        },
        joinGame()
        {  router.push({ name: 'SelectColor', params:{call:(boja,privateGame,accessCode,loginToken)=>{
           
           console.log(privateGame);
           let loginConfig = { headers: {
                   Authorization: "Bearer " +loginToken
                     }}
            axios.get(`https://localhost:5001/Game/JoinGame?boja=${boja}&accessCode=${accessCode}`,loginConfig)
            .then((data) =>
            { console.log(data);
            router.push({ name: 'Game', params:{loginToken:this.loginToken,gameToken:data.data.token,mojaBoja:boja,username:data.data.username,slika:data.data.slika,igraciImena:data.data.igraciImena,igraciSlike:data.data.igraciSlike,privateGame:privateGame} })
            }).catch(err =>
          {
           
                if(err.response.status==403)
                {    
                    alert("Color already taken! Pick again")
                }
                if(err.response.status==404)
                {
                    alert("Invalid Access Code")
                    router.push({ name: 'PlayGame', params:{loginToken:loginToken} })
                }
            
            

         });
         },privateGame:false,accessCode:this.accessCode,loginToken:this.loginToken }})
        },


        joinPublic()
        {
            router.push({ name: 'SelectColor', params:{call:(boja,privateGame,accessCode,loginToken)=>{
            console.log(accessCode);
            let loginConfig = { headers: {
                   Authorization: "Bearer " + loginToken
                     }
                        }
                     axios.get(`https://localhost:5001/Game/JoinPublicGame?boja=${boja}`,loginConfig)
                    .then((data) =>
                    { 
                        router.push({ name: 'Game', params:{loginToken:this.loginToken,gameToken:data.data.token, mojaBoja:boja, username:data.data.username, slika:data.data.slika,igraciImena:data.data.igraciImena,igraciSlike:data.data.igraciSlike,privateGame:privateGame} })
                    }).catch(err =>console.log(err));
            },privateGame:false,accessCode:this.accessCode,loginToken:this.loginToken }})
        }
        
        


    },
    props:["loginToken"],
    mounted()
    {
       let loginConfig = { headers: {Authorization: "Bearer " + this.loginToken}}
                     axios.get(`https://localhost:5001/Korisnik/MyPausedGames`,loginConfig)
                    .then((data) =>
                    { 
                        this.mygames=data.data.igre
                     
                    }).catch(err =>console.log(err));
    }
}
</script>
<style scoped>
h2
{
    color: black;
}
.joinableContainer div
{
    margin:5px;

}
*
{
    font-family: 'Montserrat', sans-serif;
    font-size: 16px;
    font-weight: 400;
}

</style>