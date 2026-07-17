<template>
    <div class="gameMap">
        <Bee v-for="bee in bees" :key="bee.beeId" :x="bee.positionX" :y="bee.positionY" :id="bee.beeId" />

        <Flower v-for="flower in flowers" :key="flower.flowerId" :x="flower.positionX" :y="flower.positionY"
            :nectarAmount="flower.amountOfNectar"></Flower>

    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import Bee from './Bee.vue'
import Flower from './Flower.vue'

const bees = ref([])
const flowers = ref([])
let sseListener = null

onMounted(() => {

    renderFlowers();

    sseListener = new EventSource('https://localhost:7257/api/ApiaryStates')

    sseListener.onmessage = (event) => {
        const data = JSON.parse(event.data)
        addOrUpdateBee(data)
    }
})



function addOrUpdateBee(item) {
    const index = bees.value.findIndex(b => b.beeId === item.beeId)

    let coords = GetCoordinates(item.position.x, item.position.y)
    let screenX = coords[0]
    let screenY = coords[1]

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

async function renderFlowers() {
    let res = await fetch("https://localhost:7257/api/Flowers");
    console.log(res)
    let flowersResult = await res.json();

    console.log(flowersResult)

    for (let element of flowersResult) {
        let coords = GetCoordinates(element.position.x, element.position.y)

        let screenX = coords[0]
        let screenY = coords[1]
        console.log(coords)

        flowers.value.push({
            flowerId: element.flowerId,
            positionX: screenX,
            positionY: screenY,
            amountOfNectar: element.amountOfNectar
        })

        console.log(flowers);
    }
}

function mapEventToHandler(event) {

}


function GetCoordinates(positionX, positionY) {
    const minX = 0, maxX = 100
    const minY = 0, maxY = 100

    const screenX = ((positionX - minX) / (maxX - minX)) * 1200
    const screenY = 800 - ((positionY - minY) / (maxY - minY)) * 900

    return [screenX, screenY]
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