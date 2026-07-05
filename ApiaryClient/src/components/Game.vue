<template>
    <div class="gameMap">
        <Bee v-for="bee in bees" :key="bee.beeId" :x="bee.positionX" :y="bee.positionY" :id="bee.beeId" />
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import Bee from './Bee.vue'

const bees = ref([])
let sseListener = null

onMounted(() => {
    sseListener = new EventSource('https://localhost:7257/api/ApiaryStates')

    sseListener.onmessage = (event) => {
        const data = JSON.parse(event.data)
        addOrUpdateBee(data)

    }
})



function addOrUpdateBee(item) {
    const index = bees.value.findIndex(b => b.beeId === item.beeId)

    const minX = 0, maxX = 100
    const minY = 0, maxY = 100

    const screenX = ((item.position.x - minX) / (maxX - minX)) * 1200
    const screenY = 800 - ((item.position.y - minY) / (maxY - minY)) * 900

    if (index !== -1) {
        bees.value[index].positionX = screenX
        bees.value[index].positionY = screenY
    } else {
        bees.value.push({
            beeId: item.beeId,
            positionX: screenX,
            positionY: screenY
        })
    }
}
</script>

<style scoped>
.gameMap {
    background-color: rgb(5, 139, 50);
    width: 1200px;
    height: 900px;
    position: relative;
    overflow: hidden;
    margin: 0 auto;
    border: 3px solid #333;
    border-radius: 8px;
    margin-left: 50px;
}
</style>