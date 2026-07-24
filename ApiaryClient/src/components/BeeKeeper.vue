<template>
    <div class="bee-keeper" :style="{ left: x + 'px', top: y + 'px' }">
        <div class="keeper-container">
            <img src="/Images/ArtemKos.jpg" alt="Пасечник" class="keeper-image" @error="handleImageError" />
            <div class="state-badge" v-if="state">
                {{ formatState(state) }}
            </div>
        </div>

        <div class="honey-counter">
            🍯 {{ collectedHoney }}
        </div>
        <div class="keeper-label">🧑‍🌾 Пасечник</div>
    </div>
</template>

<script setup>
import { ref } from 'vue'

const props = defineProps({
    x: {
        type: Number,
        required: true,
    },
    y: {
        type: Number,
        required: true,
    },
    collectedHoney: {
        type: Number,
        default: 0
    },
    state: {
        type: String,
        default: ''
    }
})

const imageLoaded = ref(true)

function handleImageError() {
    console.error('Failed to load beekeeper image')
    imageLoaded.value = false
}

function formatState(state) {
    if (!state) return ''
    return state.replace('State', '')
        .replace(/([A-Z])/g, ' $1')
        .trim()
}
</script>

<style scoped>
.bee-keeper {
    position: absolute;
    transform: translate(-50%, -50%);
    user-select: none;
    pointer-events: none;
    text-align: center;
    z-index: 10;
    transition: left 0.15s ease-out, top 0.15s ease-out;
}

.keeper-container {
    position: relative;
    animation: walk 0.8s ease-in-out infinite alternate;
}

.keeper-image {
    width: 60px;
    height: 60px;
    border-radius: 50%;
    object-fit: cover;
    border: 3px solid #ffd700;
    box-shadow: 0 0 20px rgba(255, 215, 0, 0.3);
    transition: all 0.3s ease;
    background: #f0f0f0;
}

.keeper-image:hover {
    transform: scale(1.1);
    border-color: #ff6b00;
    box-shadow: 0 0 30px rgba(255, 215, 0, 0.5);
}

.state-badge {
    position: absolute;
    top: -20px;
    left: 50%;
    transform: translateX(-50%);
    background: rgba(0, 0, 0, 0.8);
    color: #fff;
    font-size: 10px;
    padding: 2px 10px;
    border-radius: 10px;
    white-space: nowrap;
    font-weight: bold;
    border: 1px solid rgba(255, 200, 0, 0.3);
}

.honey-counter {
    font-size: 14px;
    background: rgba(0, 0, 0, 0.8);
    padding: 2px 12px;
    border-radius: 12px;
    color: gold;
    font-weight: bold;
    margin-top: 4px;
    border: 1px solid rgba(255, 215, 0, 0.3);
    text-shadow: 0 0 10px rgba(255, 215, 0, 0.3);
}

.keeper-label {
    font-size: 11px;
    color: white;
    background: rgba(0, 0, 0, 0.7);
    padding: 2px 10px;
    border-radius: 10px;
    margin-top: 2px;
    font-weight: bold;
}

@keyframes walk {
    0% {
        transform: translateY(0) scale(1);
    }

    100% {
        transform: translateY(-5px) scale(1.02);
    }
}

@keyframes glow {

    0%,
    100% {
        box-shadow: 0 0 5px rgba(255, 215, 0, 0.1);
    }

    50% {
        box-shadow: 0 0 20px rgba(255, 215, 0, 0.4);
    }
}

.honey-counter {
    animation: glow 2s ease-in-out infinite;
}
</style>