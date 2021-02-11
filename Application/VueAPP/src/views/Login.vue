<template>
  <div class="login d-flex flex-column align-items-center my-4">
    <img alt="Vue logo" src="../assets/2.png"  style="width:40%;height:40%;">
    <div class=" rounder-lg d-flex flex-column p-4">
        <input type="text" placeholder="username"/>
        <input type="text" placeholder="password"/>
        <button type="submit" name="Login" class="btn btn-info">Login</button>
    </div>
    
  </div>
</template>

<script>
// @ is an alias to /src

export default {
  name: 'Login',
  components: {
  }
}
</script>
