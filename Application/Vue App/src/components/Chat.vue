<template>
    <div>
    <div style="overflow-y:scroll; height:250px" class="chatDiv border border-info rounded bg-light">
        <div v-bind:key="poruka" v-for="poruka in poruke" >
               <div style="word-wrap: break-word;" v-bind:poruka="poruka">{{poruka}}</div>        
        </div>
     </div>   
        <div class="d-flex flex-row">
            <input v-model="novaPoruka" type="text" class="input is-info" @keyup="addMsgEnter">
            <button v-on:click="addMsg" class="btn btn-info">
                <i class="fa fa-paper-plane" aria-hidden="true"></i>
            </button>
        </div>
    </div>
</template>

<script>
export default {
    name:"Chat",
    data(){
        return{
        poruke:[],
        novaPoruka:"",
        

        }
    },
    props:["pristiglePoruke"],
    mounted()
    {   this.poruke=this.pristiglePoruke;
       // this.poruke.push(this.pristiglaPoruka);
    },methods:
    {
        
            addMsg()
            {   
                if(this.novaPoruka=="") return;
                this.poruke.unshift("ME :   "+this.novaPoruka);
                this.$emit("messageSent", this.novaPoruka);
                this.novaPoruka="";
            },
            addMsgEnter(ev)
            {
                if (ev.keyCode === 13) 
                this.addMsg();
                
        
            }
    }

    
}
</script>



<style scoped>

.chatDiv
{
    display: flex;
     flex-direction: column-reverse;
}
</style>