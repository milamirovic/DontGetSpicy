<template>
     <div style="display:flex;flex-direction:row" v-bind:class="{'opacitate':mute}">
            <div style="width:80%" v-bind:class="{'hide':mute}">   
            <div style="overflow-y:scroll; height:250px" class="chatDiv border border-info rounded bg-light">
                <div v-bind:key="poruka" v-for="poruka in pristiglePoruke" >
                    <div style="word-wrap: break-word;" v-bind:poruka="poruka">{{poruka}}</div>        
                </div>
            </div>   
                <div class="d-flex flex-row">
                    <input v-model="novaPoruka" type="text" class="input is-info" @keyup="addMsgEnter">

                    

                    <button v-on:click="addMsg" class="btn btn-info" style="margin-left:1px">
                        <i class="fa fa-paper-plane" aria-hidden="true"></i>
                    </button>
                </div>
            </div>

            <span>
                <button style="margin-left:1px" class="btn btn-info"  v-if="!mute" type="button" v-on:click="chatMute"><img id="muteChat" src="../assets/mute.jpg" alt=""></button>
                <button style="margin-left:1px"  class="btn btn-info" id="unmute" v-if="mute" type="button" v-on:click="chatUnmute"><img id="unmuteChat" src="../assets/unmute.png" alt=""></button>
            </span>
         

    </div>
</template>

<script>
import {HubConnectionBuilder} from '@microsoft/signalr'
export default {
    name:"Chat",
    data(){
        return{
        novaPoruka:"",
        mute:false,
        connection:null,
        pristiglePoruke:[],
        inChat:''

        }
    },
    props:["started","gameToken","accessCode","privateGame"],
    mounted()
    {   
        this.connection =new HubConnectionBuilder().withUrl("https://localhost:5001/ChatHub",{accessTokenFactory:()=>this.gameToken}).build();

        if(this.accessCode!=undefined)
        this.pristiglePoruke.push("Access code is: "+this.accessCode);

         this.connection.start().then(()=>
        {
          this.connection.invoke("StartChat");
        })

        this.connection.on("userSentMessage",(message, sender) =>{
           
          this.pristiglePoruke.unshift(sender+" :   "+message)
         }) 

    },
    methods:
    {
         chatMute()
        { 
        this.connection.invoke("DisableChat");
        this.mute=!this.mute;  
        },
        chatUnmute()
        {
        this.connection.invoke("Reconnect2Chat");
        this.mute=!this.mute;  
        },
        sendMsg(msg)
        {
            this.connection.invoke("SendMessage", msg);
            
        },
        generisiNazivSlike(slika)
        {
            return "http://localhost:5000/"+slika;
        },
        addMsg()
        {   
            if(this.novaPoruka=="") return;
            this.pristiglePoruke.unshift("ME :   "+this.novaPoruka);
            this.sendMsg(this.novaPoruka)
            this.novaPoruka="";
        },
        addMsgEnter(ev)
        {
            if (ev.keyCode === 13) 
            this.addMsg();
        },
           
           

    }

    
}
</script>



<style scoped>

.chatDiv
{
    display: flex;
     flex-direction: column-reverse;
}
.opacitate div
{
    opacity: 30%;
}
.hide
{
    visibility: hidden;
}

img
{
    width: 25px;
    height: 25px;
}
</style>