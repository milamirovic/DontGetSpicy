<template>
    <div>
    <div style="overflow:scroll; max-height:200px" class="border border-info rounded bg-light">
        <div v-bind:key="poruka" v-for="poruka in poruke" >
               <div v-bind:poruka="poruka">{{poruka}}</div>        
        </div>
     </div>   
        <div class="d-flex flex-row">
            <input v-model="novaPoruka" type="text" class="input is-info">
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
            {   this.poruke.push("ME : "+this.novaPoruka);
                this.$emit("messageSent", this.novaPoruka);
                this.novaPoruka="";
            }
        
    }

    
}
</script>



<style scoped>

</style>