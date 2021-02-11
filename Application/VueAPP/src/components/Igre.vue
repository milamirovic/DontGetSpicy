<template>
    <div>
        <h1 class="display-4">Moje igre</h1>
        <div v-bind:key="igra.id" v-for="igra in igre">
            <Igra v-bind:igra="igra" />
        </div>
    </div>
</template>

<script>
import Igra from './Igra.vue';
export default {
    name: "Igre",
    components:{
        Igra
    },
    props: ["igre"]
}
</script>

<style scoped>

</style>