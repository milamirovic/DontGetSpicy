<template>
    <div class="map">
        <div v-bind:key="glPolje.index" v-for="glPolje in igra.glavnaPolja">
               <Polje v-bind:polje="glPolje"/>
        </div>

        <div v-bind:key="outPolje.index" v-for="outPolje in igra.outPolja">
               <Polje v-bind:polje="outPolje"/>
               
        </div>
       
    </div>
</template>

<script>

import Igra from '../models/igra';
import Polje from './Polje.vue';

/*
<button v-on:click="connectSignalR">poveziSe</button>
     <input type="text" v-model="poruka">
     <button v-on:click="posaljiPoruku">Posalji</button>*/

export default {
    name: "Igra", 
    props:["gameToken","accessCode"],
    data() {
    return {
        poruka:'',
        igra: ''

    }
    },
    components:{
        Polje
    },
    mounted(){
       
        this.igra=new Igra();

    },
    methods:{
      
      
        Odigraj()
        {
           // this.igra.odigrajPotez({staraPozicija:-1,novaPozicija:1})
        }
    }
}
</script>

<style scoped>
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

</style>