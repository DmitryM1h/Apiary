<template>

    <div class="gameMap">
        <Bee v-for="bee in bees" :style="{
            left: bee.positionX + 'px',
            top: bee.positionY + 'px'
        }" :key="bee.beeId"></Bee>
    </div>

</template>

<script setup>
import { ref, onMounted } from 'vue'
import Bee from './Bee.vue';

const bees = ref([])

let sseListner = null;

onMounted(() => {

    sseListner = new EventSource('https://localhost:7257/api/ApiaryStates')

    sseListner.onmessage = (sdata) => {
        let val = JSON.parse(sdata.data);
        console.log(val)
        AddOrUpdate(val);
    }
});

function AddOrUpdate(item) {

    let beeId = item.beeId;
    let posX = item.position.x;
    let posY = item.position.y;

    let bee = bees.value.find(item => item.beeId === beeId);
    if (bee) {
        bee.positionX = posX
        bee.positionY = posY
    }
    else {
        let bee = {
            beeId: beeId,
            positionX: posX,
            positionY: posY,
        }
        bees.value.push(bee);
    }

}

</script>

<style scoped>
.gameMap {
    background-color: rgb(5, 139, 50);
    width: 1200px;
    height: 800px;
}
</style>