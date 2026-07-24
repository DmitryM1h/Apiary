<template>
    <div class="bee" 
         :style="{ left: x + 'px', top: y + 'px' }" 
         :class="{ 
             queen: isQueen, 
             guard: isGuard,
             worker: !isQueen && !isGuard 
         }">
        <span v-if="isQueen">🐝</span>
        <span v-else-if="isGuard">🐝</span>
        <span v-else>🐝</span>
        <p class="beeId">
            id={{ id }}
            <span v-if="isQueen" class="label queen-label">👑 Королева</span>
            <span v-else-if="isGuard" class="label guard-label">🛡️ Охранник</span>
        </p>
    </div>
</template>

<script setup>
defineProps({
    x: {
        type: Number,
        required: true,
    },
    y: {
        type: Number,
        required: true,
    },
    id: {
        type: Number,
        required: true,
    },
    isQueen: {
        type: Boolean,
        default: false
    },
    isGuard: {
        type: Boolean,
        default: false
    }
})
</script>

<style scoped>
.bee {
    position: absolute;
    font-size: 44px;
    width: 44px;
    height: 44px;
    transform: translate(-50%, -50%);
    transition: left 0.15s ease-out, top 0.15s ease-out;
    user-select: none;
    pointer-events: none;
    line-height: 1;
    text-align: center;
}

.bee.queen {
    font-size: 54px;
    z-index: 5;
    animation: pulse 2s ease-in-out infinite;
}

.bee.guard {
    font-size: 48px;
    z-index: 3;
    animation: guardPulse 1.5s ease-in-out infinite;
    filter: drop-shadow(0 0 10px rgba(255, 165, 0, 0.5));
}

.bee.worker {
    z-index: 1;
}

.beeId {
    font-size: 14px;
    margin: 0;
    background: rgba(0,0,0,0.5);
    padding: 2px 6px;
    border-radius: 10px;
    color: white;
    white-space: nowrap;
    margin-top: 40px;
}

.label {
    font-size: 12px;
    font-weight: bold;
    display: block;
    margin-top: 2px;
}

.queen-label {
    color: gold;
}

.guard-label {
    color: orange;
    animation: blink 1s step-end infinite;
}

@keyframes pulse {
    0% { transform: translate(-50%, -50%) scale(1); }
    50% { transform: translate(-50%, -50%) scale(1.2); }
    100% { transform: translate(-50%, -50%) scale(1); }
}

@keyframes guardPulse {
    0% { transform: translate(-50%, -50%) scale(1) rotate(0deg); }
    50% { transform: translate(-50%, -50%) scale(1.1) rotate(5deg); }
    100% { transform: translate(-50%, -50%) scale(1) rotate(0deg); }
}

@keyframes blink {
    0%, 100% { opacity: 1; }
    50% { opacity: 0.3; }
}
</style>