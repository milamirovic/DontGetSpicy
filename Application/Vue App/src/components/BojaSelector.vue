<template>
    <div class="border border-info p-3 w-20 h-25">
        <input type="radio" id="crveni" value="crveni" v-model="picked" @change="changeValue" >
        <label for="crveni">Crveni</label><br>
        <input type="radio" id="zeleni" value="zeleni" v-model="picked" @change="changeValue">
        <label for="zeleni">Zeleni</label><br>
        <input type="radio" id="zuti" value="zuti" v-model="picked" @change="changeValue">
        <label for="zuti">Zuti</label><br>
        <input type="radio" id="plavi" value="plavi" v-model="picked" @change="changeValue">
        <label for="plavi">Plavi</label><br>
        <br>
    </div>
</template>

<script>
export default {
    name:"BojaSelector",
    data() {
    return {
      picked: 'crveni'
      
    }
  },
  methods:{
      changeValue()
      {
          this.$emit("colorChanged",this.picked)
      }
  }
}
</script>



<style scoped>
*{
    color: black;
}
</style>