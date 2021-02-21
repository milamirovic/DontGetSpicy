<template>
    <div style="margin-left:20px"> <br><br>
        <h1 class="display-4">Profile</h1><br>
        <div class="ml-5">
            <div >
                <u>Email:</u>
                <span class="pl-4">{{this.korisnik.email}}</span>
            </div><br>
            <div>
                <u>Username:</u>
                <span class="pl-4">{{this.korisnik.username}}</span>
            </div><br>
            <u>Progress:</u><br><br>
            <div class="w-25" style="display:flex;flex-direction:row" >
                <p class="pr-2">{{level}}</p><b-progress class="my-auto" :value="value" :max="max" show-progress animated style="width:200px; margin-top:2px"></b-progress><p class="pl-2">{{level+1}}</p>
            </div>    <br><br>
             <u>Profile picture:</u><br><br>
            
            <img id="imported-img" :src="this.korisnik.slika" width="150px" height="150px">
           
            <br><br>
             <input id="import-files" type="file" accept="image/*" />   
            <br><br><br>
             <div class="hideMe pl-4 ml-2" style="visibility:hidden; display:flex; flex-direction:row">
            <button type="submit" @click="updateImg" class="mr-4 btn btn-success" >Save</button><button type="button" class="btn btn-danger" @click="refreshPage">Discard</button>
            
            <svg style="width:50px;height:50px;visibility:hidden" class="checkmark" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 52 52">
                    <circle class="checkmark__circle" cx="26" cy="26" r="25" fill="none"/>
                    <path class="checkmark__check" fill="none" d="M14.1 27.2l7.1 7.2 16.7-16.8"/>
            </svg>


            
            
            </div> 
            
        </div>
            
       </div>
</template>

<script>
import axios from "axios";
import router from '../router/index.js'
export default {
    name:"Profile",
    data() {
    return {
        korisnik:'',
        value:0,
        max:0,
        level:0,
     
      
    }
  },
  props:["loginToken"]
  ,
  mounted(){
      console.log(this.loginToken);
            let loginConfig = { headers: {
                   Authorization: "Bearer " + this.loginToken
                 }     }
             axios.get(`https://localhost:5001/Korisnik/PodaciKorisnika`,loginConfig)
                    .then((data) =>
                    { 

                        this.korisnik=data.data.korisnik;
                         let int=5;   
                        let levelMax=1
                        this.korisnik.slika="http://localhost:5000/Resources/Images/"+this.korisnik.slika;
                     
                        
                        while(int==5)
                        {
                            if(this.korisnik.brojPobeda<levelMax)
                            {
                               break;
                            }
                            
                            let dodatak=levelMax/2;
                            if(Math.floor(dodatak)==dodatak)
                            {
                               
                                levelMax+=dodatak
                            }
                            else
                            {
                              
                                levelMax+=Math.floor(levelMax/2)+1      
                            } 
                            this.level++;
                           
                            
                        }
                        this.value=levelMax-Math.floor(levelMax*2/3)-(levelMax-this.korisnik.brojPobeda);
                        this.max=levelMax-Math.floor(levelMax*2/3)
                        
                        
                        
                    }).catch(err =>console.log(err));
        
        var filesInput = document.getElementById("import-files");
        
        filesInput.addEventListener("change", function pictureNotificationMessage(event){
            let file=event.target.files[0];
            var picReader = new FileReader();
            picReader.fileName=file.name;
            picReader.readAsDataURL(file);
            picReader.addEventListener("load",function(event){
         
            let imgElement=document.querySelector("#imported-img");
            imgElement.src=event.target.result; 
            
            document.querySelector(".hideMe").style.visibility="visible"
               
              
            });});
        
    },
    methods:{
        refreshPage()
        {
            router.push({ name: 'Home', params: { } })
        },
        updateImg()
        {      
            let formData = new FormData();
            formData.append("slika",document.querySelector("#import-files").files[0])
              let loginConfig = { headers: {
                   Authorization: "Bearer " + this.loginToken,
                    "Content-Type": 'multipart/form-data'
                 }     }
            axios.put(`https://localhost:5001/Korisnik/AzurirajSliku`,formData,loginConfig).then(()=>
            {
                 var element = document.querySelector(".checkmark");
                 var cloneElement = element.cloneNode(true);
                 element.parentNode.replaceChild(cloneElement, element); 
                 cloneElement.style.visibility="visible";
                 setTimeout(()=>{router.push({ name: 'Home', params: { } })},1500);
                

            })
        }

    }
}
</script>



<style scoped>
.obojiMe
{
color: #edae49; 
} 

.checkmark__circle {
  stroke-dasharray: 166;
  stroke-dashoffset: 166;
  stroke-width: 2;
  stroke-miterlimit: 10;
  stroke: #7ac142;
  fill: none;
  animation: stroke 0.6s cubic-bezier(0.65, 0, 0.45, 1) forwards;
}

.checkmark {
  width: 56px;
  height: 56px;
  border-radius: 50%;
  display: block;
  stroke-width: 2;
  stroke: #fff;
  stroke-miterlimit: 10;
  margin-left: 1%;
  box-shadow: inset 0px 0px 0px #7ac142;
  animation: fill .4s ease-in-out .4s forwards, scale .3s ease-in-out .9s both;
}

.checkmark__check {
  transform-origin: 50% 50%;
  stroke-dasharray: 48;
  stroke-dashoffset: 48;
  animation: stroke 0.3s cubic-bezier(0.65, 0, 0.45, 1) 0.8s forwards;
}

@keyframes stroke {
  100% {
    stroke-dashoffset: 0;
  }
}
@keyframes scale {
  0%, 100% {
    transform: none;
  }
  50% {
    transform: scale3d(1.1, 1.1, 1);
  }
}
@keyframes fill {
  100% {
    box-shadow: inset 0px 0px 0px 30px #7ac142;
  }
}
</style>