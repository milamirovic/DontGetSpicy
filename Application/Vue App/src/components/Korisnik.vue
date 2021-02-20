<template>
    <div border border-info>
        <h2>{{ime}}</h2><br>
        <img :src="slika" width="50px" height="50px">
    </div>
</template>


<script>
export default {
    name:"Korisnik",
    props:["ime","slika"]

    
}
</script>


<style scoped>
*
{
    color: black;
}
</style>