<template>
    <div v-bind:id=this.polje.index style="margin-left:10px" v-bind:class="{'hide':polje.figura==null}">
     <img :src="imgSrc()" width="32px" height="50px" @click="print" >
    </div>
</template>

<script>
export default {
        name:"Polje",
      props:["polje"],
      mounted(){
               
             let d=document.getElementById(this.polje.index)
             d.style.position = "absolute";
             d.style.left =parseInt(this.polje.x)+280+'px';
            d.style.top = parseInt(this.polje.y)+150-10+'px';
          
         
         
      },methods:{
          imgSrc()
          {   if(this.polje.figura!=null)
              return require(`../assets/${this.polje.figura.boja}.png`);
          },
          print()
          {
              this.$emit("figuraIzabrana",this.polje);

          }


      }

      
    
}
</script>

<style scoped>
.hide
{
    visibility: hidden;
}

</style>