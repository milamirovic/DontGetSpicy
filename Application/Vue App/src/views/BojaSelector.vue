<template>
    <div class="border border-info p-3 w-20 h-25"><br><br><br><br><br>
        <h2>Select one of following colors<br><br><br><br> 
        </h2>
        <div  class="radioDiv">
        <input type="radio" id="crveni" name="boja" value="crveni" v-model="picked" >
        <label for="crveni">Crveni</label>
        </div>
        <div   class="radioDiv">
        <input type="radio" id="zeleni" name="boja" value="zeleni" v-model="picked"  >
        <label for="zeleni">Zeleni</label>
        </div>
        <div  class="radioDiv">
        <input type="radio" id="zuti" name="boja" value="zuti"  v-model="picked"   >
        <label for="zuti">Zuti</label>
        </div>
        <div  class="radioDiv">
        <input type="radio" id="plavi"  name="boja" value="plavi" v-model="picked" >
        <label for="plavi">Plavi</label>
        </div>
        <br>
        <button class="btn btn-info" v-on:click="fun">Join</button><br>
        <a v-on:click="cancel"><u>cancel</u></a>
        
    </div>
</template>

<script>
import router from '../router/index.js'
export default {
    name:"BojaSelector",
    data() {
    return {
      picked:"crveni"
      
    }
    
  },
          
  props:["call","privateGame","accessCode","loginToken"],
  mounted()
  {
      

  },
  methods:{
      
      fun()
    {
        this.call(this.picked,this.privateGame,this.accessCode,this.loginToken)
               
    },
    cancel()
    {
        router.push({ name: 'PlayGame', params:{loginToken:this.loginToken} })
    } 
     
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