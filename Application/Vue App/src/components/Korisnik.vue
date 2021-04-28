<template>
    <div v-if="ime!=null&&slika!=null">
        
        <img :src="slika" width="100px" height="100px" v-bind:class="{'naPotezu':naPotezu}">
        <center ><u >{{ime}}</u></center>
    </div>
</template>


<script>
export default {
    name:"Korisnik",
    props:["ime","slika","naPotezu"],
    methods:
    {
     
    },
    mounted()
    {
     
    }
}
</script>


<style scoped>
*
{
    color: black;
}
img
{
  height: 100px;
  border-radius: 20px 20px;
}
.naPotezu
{
  
  box-shadow: 10px 10px 20px #17a2b8;
}
</style>