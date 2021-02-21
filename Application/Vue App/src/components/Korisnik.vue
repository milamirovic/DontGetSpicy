<template>
    <div v-if="ime!=null&&slika!=null">
        <center ><u >{{ime}}</u></center>
        <img :src="generisiNazivSlike()" width="100px" height="100px" v-bind:class="{'naPotezu':naPotezu}">
    </div>
</template>


<script>
export default {
    name:"Korisnik",
    props:["ime","slika","naPotezu"],
    methods:
    {
      generisiNazivSlike()
      {
        return "http://localhost:5000/Resources/Images/"+this.slika;
      }
    }

  //  border border-info
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
  
  box-shadow: 10px 10px 20px #4ad557;
}
</style>