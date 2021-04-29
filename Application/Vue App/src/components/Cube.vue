<template>
    <div>
     <div id="stage" style="width: 120px; height: 120px;">
        <div class="spinner anim0" v-if="spinCube">
            <div class="face1"><img src="../assets/11.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face2"><img src="../assets/22.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face3"><img src="../assets/33.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face4"><img src="../assets/44.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face5"><img src="../assets/55.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face6"><img src="../assets/66.png" alt="" srcset="" width="120px" height="120px"></div>
            
        </div>
        <div class="spinner anim2" v-if="!spinCube&&kocka==1">
            <div class="face1"><img src="../assets/11.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face2"><img src="../assets/22.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face3"><img src="../assets/33.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face4"><img src="../assets/11.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face5"><img src="../assets/44.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face6"><img src="../assets/66.png" alt="" srcset="" width="120px" height="120px"></div>
            
        </div>
        <div class="spinner anim2" v-if="!spinCube&&kocka==2">
            <div class="face1"><img src="../assets/22.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face2"><img src="../assets/55.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face3"><img src="../assets/33.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face4"><img src="../assets/22.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face5"><img src="../assets/11.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face6"><img src="../assets/66.png" alt="" srcset="" width="120px" height="120px"></div>
            
        </div>
        <div class="spinner anim2" v-if="!spinCube&&kocka==3">
            <div class="face1"><img src="../assets/33.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face2"><img src="../assets/22.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face3"><img src="../assets/55.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face4"><img src="../assets/33.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face5"><img src="../assets/11.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face6"><img src="../assets/66.png" alt="" srcset="" width="120px" height="120px"></div>
            
        </div>
        <div class="spinner anim2" v-if="!spinCube&&kocka==4">
            <div class="face1"><img src="../assets/44.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face2"><img src="../assets/22.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face3"><img src="../assets/33.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face4"><img src="../assets/44.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face5"><img src="../assets/11.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face6"><img src="../assets/66.png" alt="" srcset="" width="120px" height="120px"></div>
            
        </div>
        <div class="spinner anim2" v-if="!spinCube&&kocka==5">
            <div class="face1"><img src="../assets/55.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face2"><img src="../assets/22.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face3"><img src="../assets/33.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face4"><img src="../assets/55.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face5"><img src="../assets/11.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face6"><img src="../assets/66.png" alt="" srcset="" width="120px" height="120px"></div>
            
        </div>
        <div class="spinner anim2" v-if="!spinCube&&kocka==6">
            <div class="face1"><img src="../assets/66.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face2"><img src="../assets/22.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face3"><img src="../assets/33.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face4"><img src="../assets/66.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face5"><img src="../assets/11.png" alt="" srcset="" width="120px" height="120px"></div>
            <div class="face6"><img src="../assets/44.png" alt="" srcset="" width="120px" height="120px"></div>
            
        </div>
        
    </div>
    <div style="display:none">{{kocka}}</div>
    </div>
</template>

<script>
export default {
        name:"Cube",
      props:["spinCube","kocka"],
      mounted(){ 
         if(!this.spinCube) this.baciKocku(this.kocka)
     },
      methods:{
          baciKocku(broj) {
       
            /*document.querySelectorAll(".spinner img").forEach((slika, index) =>
            {
                let ind =index+1;
                slika.src=require('../assets/'+ind+ind+".png");    
            })*/

            
            var prvi = document.querySelector(".face1>img");
            var cetvrti=document.querySelector(".face4>img");
            prvi.src=cetvrti.src=require('../assets/'+broj+broj+".png");
           
            document.querySelector(".spinner").classList.replace("anim0", "anim2");


      },
         
      }

      
    
}
</script>

<style scoped>
.spinner div {
    position: absolute;
    width: 120px;
    height: 120px;
    border: 1px solid #ccc;
    background: rgba(255,255,255,0.8);
    box-shadow: inset 0 0 20px rgba(0,0,0,0.2);
    text-align: center;
    
}

.spinner .face1 { 
    -webkit-transform: translateZ(60px);
    -ms-transform: translateZ(60px);
    transform: translateZ(60px);
}
.spinner .face2 { 
    -webkit-transform: rotateY(90deg) translateZ(60px); 
    -ms-transform: rotateY(90deg) translateZ(60px); 
    transform: rotateY(90deg) translateZ(60px); 
}
.spinner .face3 { 
    -webkit-transform: rotateY(90deg) rotateX(90deg) translateZ(60px); 
    -ms-transform: rotateY(90deg) rotateX(90deg) translateZ(60px); 
    transform: rotateY(90deg) rotateX(90deg) translateZ(60px); 
}
.spinner .face4 { 
    -webkit-transform: rotateY(180deg) rotateZ(90deg) translateZ(60px); 
    -ms-transform: rotateY(180deg) rotateZ(90deg) translateZ(60px); 
    transform: rotateY(180deg) rotateZ(90deg) translateZ(60px); 
}
.spinner .face5 { 
    -webkit-transform: rotateY(-90deg) rotateZ(90deg) translateZ(60px); 
    -ms-transform: rotateY(-90deg) rotateZ(90deg) translateZ(60px); 
    transform: rotateY(-90deg) rotateZ(90deg) translateZ(60px); 
}
.spinner .face6 { 
    -webkit-transform: rotateX(-90deg) translateZ(60px); 
    -ms-transform: rotateX(-90deg) translateZ(60px); 
    transform: rotateX(-90deg) translateZ(60px); 
}
*
{
    font-size:60px;  
}
.anim0{
    -webkit-animation: spincube 10s linear infinite;
    animation: spincube 10s linear  infinite;
    -webkit-transform-style: preserve-3d;
    -ms-transform-style: preserve-3d;
    transform-style: preserve-3d;
    -webkit-transform-origin: 60px 60px 0;
    -ms-transform-origin: 60px 60px 0;
    transform-origin: 60px 60px 0;
}

.anim1 {
    -webkit-animation: throw1 1s linear 1;
    animation: throw1 1s linear  1;
    -webkit-transform-style: preserve-3d;
    -ms-transform-style: preserve-3d;
    transform-style: preserve-3d;
    -webkit-transform-origin: 60px 60px 0;
    -ms-transform-origin: 60px 60px 0;
    transform-origin: 60px 60px 0;
}
.anim2 {
    -webkit-animation: throw2 1s linear 1;
    animation: throw2 1s linear  1;
    -webkit-transform-style: preserve-3d;
    -ms-transform-style: preserve-3d;
    transform-style: preserve-3d;
    -webkit-transform-origin: 60px 60px 0;
    -ms-transform-origin: 60px 60px 0;
    transform-origin: 60px 60px 0;
}
.anim3 {
    -webkit-animation: throw3 1s linear 1;
    animation: throw3 1s linear  1;
    -webkit-transform-style: preserve-3d;
    -ms-transform-style: preserve-3d;
    transform-style: preserve-3d;
    -webkit-transform-origin: 60px 60px 0;
    -ms-transform-origin: 60px 60px 0;
    transform-origin: 60px 60px 0;
}
.anim4 {
    -webkit-animation: throw4 1s linear 1;
    animation: throw4 1s linear  1;
    -webkit-transform-style: preserve-3d;
    -ms-transform-style: preserve-3d;
    transform-style: preserve-3d;
    -webkit-transform-origin: 60px 60px 0;
    -ms-transform-origin: 60px 60px 0;
    transform-origin: 60px 60px 0;
}
.anim5 {
    -webkit-animation: throw5 1s linear 1;
    animation: throw5 1s linear  1;
    -webkit-transform-style: preserve-3d;
    -ms-transform-style: preserve-3d;
    transform-style: preserve-3d;
    -webkit-transform-origin: 60px 60px 0;
    -ms-transform-origin: 60px 60px 0;
    transform-origin: 60px 60px 0;
}
.anim6 {
    -webkit-animation: throw6 1s linear 1;
    animation: throw6 1s linear  1;
    -webkit-transform-style: preserve-3d;
    -ms-transform-style: preserve-3d;
    transform-style: preserve-3d;
    -webkit-transform-origin: 60px 60px 0;
    -ms-transform-origin: 60px 60px 0;
    transform-origin: 60px 60px 0;
}

@keyframes spincube {
 
    0%      {  transform: rotateY(10800deg) rotateZ(0deg) rotateX(-45deg); }
    100%       { transform: rotateY(0deg) rotateZ(0deg) rotateX(-45deg); }
      
}


@keyframes throw2 {
    0%      {  transform: rotateY(540deg) rotateZ(0deg) rotateX(-45deg); }
    70%       { transform: rotateY(0deg) rotateZ(0deg) rotateX(-45deg); }
    100%       {transform: rotateY(180deg) rotateZ(-90deg); }
   
}

</style>