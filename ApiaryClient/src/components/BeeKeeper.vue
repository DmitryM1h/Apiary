<template>
    <div class="bee-keeper" :style="{ left: x + 'px', top: y + 'px' }">
        <div class="keeper-container" :class="{ sleeping: isWaiting }">
            <img 
                src="/Images/ArtemKos.jpg" 
                alt="Пасечник" 
                class="keeper-image"
                :class="{ sleeping: isWaiting }"
                @error="handleImageError" 
            />
            
            <!-- ZZZ -->
            <div v-if="isWaiting" class="zzz-container">
                <span class="zzz zzz1">Z</span>
                <span class="zzz zzz2">Z</span>
                <span class="zzz zzz3">Z</span>
            </div>
            
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
import { computed } from 'vue'

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

const isWaiting = computed(() => {
    return props.state === 'WaitingState' || props.state === 'Waiting'
})

function handleImageError() {
    console.error('Failed to load beekeeper image')
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
    transition: all 0.5s ease;
}

/* Спящий режим */
.keeper-container.sleeping {
    animation: sleep 2s ease-in-out infinite;
}

.keeper-image {
    width: 60px;
    height: 60px;
    border-radius: 50%;
    object-fit: cover;
    border: 3px solid #ffd700;
    box-shadow: 0 0 20px rgba(255, 215, 0, 0.3);
    transition: all 0.5s ease;
    background: #f0f0f0;
}

/* Поворот набок когда спит */
.keeper-image.sleeping {
    transform: rotate(15deg) scale(0.95);
    border-color: #9e9e9e;
    filter: grayscale(0.3);
    box-shadow: 0 0 20px rgba(100, 100, 100, 0.2);
}

/* ZZZ анимация */
.zzz-container {
    position: absolute;
    top: -30px;
    right: -30px;
    font-weight: bold;
    color: #4fc3f7;
    text-shadow: 0 0 20px rgba(79, 195, 247, 0.5);
    pointer-events: none;
}

.zzz {
    position: absolute;
    font-weight: 900;
    opacity: 0;
    animation: zzzFloat 2.5s ease-in-out infinite;
}

.zzz1 {
    top: 0;
    right: 0;
    animation-delay: 0s;
    font-size: 16px;
}

.zzz2 {
    top: -10px;
    right: 15px;
    animation-delay: 0.4s;
    font-size: 22px;
}

.zzz3 {
    top: -20px;
    right: 30px;
    animation-delay: 0.8s;
    font-size: 28px;
}

@keyframes zzzFloat {
    0% {
        opacity: 0;
        transform: translate(0, 0) scale(0.5);
    }
    20% {
        opacity: 1;
        transform: translate(-5px, -10px) scale(1);
    }
    80% {
        opacity: 1;
        transform: translate(-15px, -25px) scale(1);
    }
    100% {
        opacity: 0;
        transform: translate(-20px, -35px) scale(1.2);
    }
}

@keyframes sleep {
    0% { transform: translateY(0) rotate(0deg); }
    25% { transform: translateY(-2px) rotate(1deg); }
    75% { transform: translateY(-2px) rotate(-1deg); }
    100% { transform: translateY(0) rotate(0deg); }
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
    0% { transform: translateY(0) scale(1); }
    100% { transform: translateY(-5px) scale(1.02); }
}

@keyframes glow {
    0%, 100% { box-shadow: 0 0 5px rgba(255, 215, 0, 0.1); }
    50% { box-shadow: 0 0 20px rgba(255, 215, 0, 0.4); }
}

.honey-counter {
    animation: glow 2s ease-in-out infinite;
}
</style>