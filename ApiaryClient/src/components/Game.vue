<template>
    <div class="gameMap">
        <Bee v-for="bee in bees" :key="bee.beeId" :x="bee.positionX" :y="bee.positionY" :id="bee.beeId"
            :isQueen="bee.isQueen" :isGuard="bee.isGuard" />

        <Flower v-for="flower in flowers" :key="flower.flowerId" :x="flower.positionX" :y="flower.positionY"
            :nectarAmount="flower.nectarAmount" />

        <BeeKeeper v-if="beeKeeper" :x="beeKeeper.positionX" :y="beeKeeper.positionY"
            :collectedHoney="beeKeeper.collectedHoney" :state="beeKeeper.state" />
    </div>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import Bee from './Bee.vue'
import Flower from './Flower.vue'
import BeeKeeper from './BeeKeeper.vue'

const bees = ref([])
const flowers = ref([])
const beeKeeper = ref(null)
let sseListener = null

const ACTOR_TYPES = {
    WorkerBee: 1,
    QueenBee: 2,
    GuardBee: 3,
    Flower: 4,
    BeeKeeper: 5
}

onMounted(() => {
    renderFlowers();

    sseListener = new EventSource('https://localhost:7257/api/ApiaryStates')

    sseListener.onmessage = (event) => {
        try {
            const data = JSON.parse(event.data);

            if (data.actorType === ACTOR_TYPES.Flower) {
                updateFlower(data);
            } else if (data.actorType === ACTOR_TYPES.BeeKeeper) {
                updateBeeKeeper(data);
            } else {
                addOrUpdateBee(data);
            }
        } catch (error) {
            console.error('Error parsing SSE message:', error);
        }
    }

    sseListener.onerror = (error) => {
        console.error('SSE Error:', error)
        sseListener.close()
    }
})

onUnmounted(() => {
    if (sseListener) {
        sseListener.close()
    }
})

function updateBeeKeeper(item) {
    if (!item.position) {
        console.warn('BeeKeeper without position:', item)
        return
    }

    let coords = GetCoordinates(item.position.x, item.position.y)
    let screenX = coords[0]
    let screenY = coords[1]

    if (beeKeeper.value) {
        beeKeeper.value.positionX = screenX
        beeKeeper.value.positionY = screenY
        beeKeeper.value.collectedHoney = item.collectedHoney || 0
        beeKeeper.value.state = item.state || ''
    } else {
        beeKeeper.value = {
            positionX: screenX,
            positionY: screenY,
            collectedHoney: item.collectedHoney || 0,
            state: item.state || ''
        }
    }
}

function updateFlower(item) {
    const index = flowers.value.findIndex(f => f.flowerId === item.flowerId)

    if (!item.position) {
        console.warn('Flower without position:', item)
        return
    }

    let coords = GetCoordinates(item.position.x, item.position.y)
    let screenX = coords[0]
    let screenY = coords[1]

    if (index !== -1) {
        flowers.value[index].positionX = screenX
        flowers.value[index].positionY = screenY
        flowers.value[index].nectarAmount = item.nectarAmount
    } else {
        flowers.value.push({
            flowerId: item.flowerId,
            positionX: screenX,
            positionY: screenY,
            nectarAmount: item.nectarAmount
        })
    }
}

function addOrUpdateBee(item) {
    const index = bees.value.findIndex(b => b.beeId === item.beeId)

    if (!item.position) {
        console.warn('Bee without position:', item)
        return
    }

    let coords = GetCoordinates(item.position.x, item.position.y)
    let screenX = coords[0]
    let screenY = coords[1]

    const isQueen = item.actorType === ACTOR_TYPES.QueenBee
    const isGuard = item.actorType === ACTOR_TYPES.GuardBee

    if (index !== -1) {
        bees.value[index].positionX = screenX
        bees.value[index].positionY = screenY
        bees.value[index].isQueen = isQueen
        bees.value[index].isGuard = isGuard
    } else {
        bees.value.push({
            beeId: item.beeId,
            positionX: screenX,
            positionY: screenY,
            isQueen: isQueen,
            isGuard: isGuard
        })
    }
}

async function renderFlowers() {
    try {
        let res = await fetch("https://localhost:7257/api/Flowers");
        let flowersResult = await res.json();

        for (let element of flowersResult) {
            let coords = GetCoordinates(element.position.x, element.position.y)
            let screenX = coords[0]
            let screenY = coords[1]

            flowers.value.push({
                flowerId: element.flowerId,
                positionX: screenX,
                positionY: screenY,
                nectarAmount: element.amountOfNectar
            })
        }
    } catch (error) {
        console.error('Error loading flowers:', error)
    }
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