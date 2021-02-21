<template>
    <div class="border border-info p-3 w-20 h-25">
        <div v-bind:class="{'disabledRadio':crveniZauzet}" class="radioDiv">
        <input type="radio" id="crveni" name="boja" value="crveni" v-model="picked" @change="changeValue">
        <label for="crveni">Crveni</label>
        </div>
        <div  v-bind:class="{'disabledRadio':zeleniZauzet}" class="radioDiv">
        <input type="radio" id="zeleni" name="boja" value="zeleni" v-model="picked" @change="changeValue" >
        <label for="zeleni">Zeleni</label>
        </div>
        <div  v-bind:class="{'disabledRadio':zutiZauzet}" class="radioDiv">
        <input type="radio" id="zuti" name="boja" value="zuti"  v-model="picked" @change="changeValue"  >
        <label for="zuti">Zuti</label>
        </div>
        <div  v-bind:class="{'disabledRadio':plaviZauzet}" class="radioDiv">
        <input type="radio" id="plavi"  name="boja" value="plavi" v-model="picked" @change="changeValue" >
        <label for="plavi">Plavi</label>
        </div>
        <br>
        
    </div>
</template>

<script>
export default {
    name:"BojaSelector",
    data() {
    return {
      picked:"crveni"
      
    }
  },
  updated()
  {
      let selected=false;
      document.querySelectorAll(".radioDiv").forEach(div =>
      {
          if(div.querySelector("input").checked&&!div.classList.contains("disabledRadio"))
          selected=true;
      })
      if(!selected)
      {

            for (let el of document.querySelectorAll(".radioDiv"))
            {
                if(!el.classList.contains("disabledRadio"))
                 {
                    el.querySelector("input").checked=true;
                    this.picked=el.querySelector("input").value;
                    this.changeValue()
                    break;
                 }
            }
      }
      console.log(this.picked);
  },
  props:["crveniZauzet","zeleniZauzet","zutiZauzet","plaviZauzet"],
  mounted()
  {
      

  },
  methods:{
      changeValue()
      {
          this.$emit("colorChanged",this.picked)
      },
     
  },
  
}
</script>



<style scoped>
*{
    color: black;
}
.disabledRadio
{
    pointer-events:none;
  opacity:0.5;
}
</style>