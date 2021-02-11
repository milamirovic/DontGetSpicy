<template>
  <div class="signup d-flex flex-column align-items-center my-4">
    <img alt="Vue logo" src="../assets/2.png" style="width:40%;height:40%;">
    <div class="d-flex flex-column mx-4  p-4">
        <input type="text" placeholder="email"/>
        <input type="text" placeholder="username"/>
        <input type="text" placeholder="password"/>
        <button type="submit" name="Signup" class="btn btn-info">Signup</button>
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
