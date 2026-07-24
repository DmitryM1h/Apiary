<template>
    <div class="bee" :style="{ left: x + 'px', top: y + 'px' }" :class="{
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
}

.bee.guard {
    font-size: 48px;
    z-index: 3;
}

.bee.worker {
    z-index: 1;
}

.beeId {
    position: absolute;
    font-size: 11px;
    margin: 0;
    background: rgba(0, 0, 0, 0.6);
    padding: 1px 4px;
    border-radius: 8px;
    color: white;
    white-space: nowrap;
    top: 100%;
    left: 50%;
    transform: translateX(-50%);
    margin-top: 2px;
    font-weight: bold;
    letter-spacing: 0.5px;
    pointer-events: none;
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
}
</style>