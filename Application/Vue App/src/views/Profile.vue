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
             <div class="hideMe pl-4 ml-2" style="visibility:hidden">
            <button type="submit" @click="updateImg" class="mr-4 btn btn-success" >Save</button><button type="button" class="btn btn-danger" @click="refreshPage">Discard</button>
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
                router.push({ name: 'Home', params: { } })

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
</style>