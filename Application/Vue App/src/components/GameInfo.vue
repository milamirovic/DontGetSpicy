<template>
    <div id="main" style="width:fit-content;" class="p-2">
        <div>
            <table>
                <tr>
                    <td class="crveni">
                        <center v-if="gameData.crveniUsername!=null">{{gameData.crveniUsername}}</center>      
                        <center v-if="gameData.crveniUsername==null">/</center>    
                    </td>
                    <td class="zeleni">
                        <center>{{gameData.zeleniUsername}}</center> 
                        <center v-if="gameData.zeleniUsername==null">/</center>   
                    </td>
                </tr>
                <tr>
                    <td class="plavi">
                       <center>{{gameData.plaviUsername}}</center> 
                       <center v-if="gameData.plaviUsername==null">/</center>   
                    </td>
                    <td class="zuti">
                       <center>{{gameData.zutiUsername}}</center>  
                       <center v-if="gameData.zutiUsername==null">/</center>       
                    </td>
                    
                </tr>
            </table>
        </div>
        
        <center class="my-2"><button style="font-size:12px" class="btn btn-info" v-on:click="resume">Resume</button></center>
    </div>
</template>
<script>
import axios from "axios"
import router from '../router/index.js'
export default {
    name:"GameInfo",
    props:["gameData","loginToken"],
    mounted()
    {
    },
    methods:{
        resume()
        {
            let loginConfig = { headers: {Authorization: "Bearer " + this.loginToken}}
            axios.get(`https://localhost:5001/Game/ResumeRequest?id=${this.gameData.accessCode}`,loginConfig)
                    .then((data) =>
                    {
                        router.push({ name: 'Game', params:{loginToken:this.loginToken,accessCode:data.data.accessCode, gameToken:data.data.token, mojaBoja:data.data.boja, username:data.data.username, slika:data.data.slika,igraciImena:data.data.igraciImena,igraciSlike:data.data.igraciSlike,privateGame:false, gameState:data.data.figure, potez:data.data.naPotezu} })
                    }).catch(err =>console.log(err));
        }
    }
}
</script>
<style scoped>

.crveni
{
    background-color: #ff2c01;
}
.zeleni
{
    background-color: #01c03e;
}
.zuti
{
    background-color: #fefe00;
}
.plavi
{
    background-color: #0071f1;
}
td
{
    padding:4px;
    min-width: 70px;
    min-height: 20px;
    width: fit-content;
}
#main
{
    border: 1px solid #e28d04;
    
}

</style>