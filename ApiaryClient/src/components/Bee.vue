<template>
    <div class="bee" :style="{ left: x + 'px', top: y + 'px' }">
        🐝<p class="beeId">
            id={{ id }}
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

.beeId {
    font-size: 20px;
}
</style>