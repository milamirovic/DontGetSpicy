<template>
    <div>
    <div style="overflow:scroll; max-height:200px">
        <div v-bind:key="poruka" v-for="poruka in poruke" >
               <div v-bind:poruka="poruka">{{poruka}}</div>        
        </div>
     </div>   
        <div><input v-model="novaPoruka" type="text"><button v-on:click="addMsg">Send</button></div>
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
            {   this.poruke.push("ME : "+this.novaPoruka);
                this.$emit("messageSent", this.novaPoruka);
                this.novaPoruka="";
            }
        
    }

    
}
</script>



<style scoped>

</style>