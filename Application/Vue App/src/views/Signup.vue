<template>
<div class="glavni d-flex align-items-center">
  <div class="signup border border-info login d-flex flex-row justify-content-center my-4" style="width:400px;">
    <div class="align-self-center rounder-lg d-flex flex-column align-items-center p-4">
      <!-- <img src="https://i.imgur.com/Wt7up6Q.png" class="w-25 h-25"> -->
      <img src="../assets/signupXSmall.png" class="w-50 h-50">
      <input type="text" placeholder="email" class="input is-rounded my-2 w-75" required/>
      <input type="text" placeholder="username" class="input is-rounded my-2 w-75" required/>
      <input type="text" placeholder="password" class="input is-rounded my-2 w-75" required/>
      <button type="submit" name="Signup" class="mt-3 btn btn-info w-75">Signup</button>
    </div>
  </div>
</div>
</template>

<script>
// @ is an alias to /src

export default {
  name: 'Signup',
  components: {
  }
}
</script>

<style scoped>  
@import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@300&display=swap');
button{
  box-shadow: 5px 5px 10px 0px rgba(0,0,0,0.1), -5px -5px 10px 0px #2ACBDB!important; 
  border-color: #17A2B8; 
  background-color: #371A45; 
  font-weight: 600; color: #363636; 
  border-width: 1px;
  font-family: 'Montserrat', sans-serif;
  color: #9ce2ec;
}
.signup{
  /* box-shadow: 1px 5px 50px 1px #108294; */
  margin:auto;
    background-color: #17A2B8;
    border-radius: 15px;

}
.glavni{
  height:600px;
}

input{
  background-color: #17A2B8!important;
    border-color: #17A2B8!important;
    color: #363636;
    border-radius: 40px!important;
    box-shadow: inset 3px 3px 5px 0px rgba(0,0,0,0.1), inset -3px -3px 5px 0px #2ACBDB!important;
}

input:hover{
  filter: brightness(95%);
}

::placeholder {
  color: #45daeb;
  opacity: 1; /* Firefox */
font-family: 'Montserrat', sans-serif;
}




</style>