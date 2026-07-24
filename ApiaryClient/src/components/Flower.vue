<template>
    <div class="flower" :style="{ left: x + 'px', top: y + 'px' }">
        <div class="flower-icon">🌼</div>
        <div class="nectar-container" :style="{ width: nectarPercent + '%' }">
            <span class="nectar-text">{{ nectarAmount }}</span>
        </div>
    </div>
</template>

<script setup>
import { computed, toRefs } from 'vue'

const props = defineProps({
    x: {
        type: Number,
        required: true,
    },
    y: {
        type: Number,
        required: true,
    },
    nectarAmount: {
        type: Number,
        required: true,
    }
})

// Используем toRefs для реактивности
const { nectarAmount } = toRefs(props)

// Вычисляем процент нектара
const nectarPercent = computed(() => {
    // Максимальное значение нектара - 100
    const maxNectar = 100
    const percent = Math.min((nectarAmount.value / maxNectar) * 100, 100)
    return percent
})

// Для отладки
console.log('Nectar amount:', nectarAmount.value)
console.log('Nectar percent:', nectarPercent.value)
</script>

<style scoped>
.flower {
    position: absolute;
    transform: translate(-50%, -50%);
    user-select: none;
    pointer-events: none;
    text-align: center;
    display: flex;
    flex-direction: column;
    align-items: center;
    transition: all 0.3s ease;
}

.flower-icon {
    font-size: 44px;
    line-height: 1;
    margin-bottom: 2px;
}

.nectar-container {
    background: rgba(255, 215, 0, 0.8);
    border-radius: 10px;
    min-width: 20px;
    height: 20px;
    display: flex;
    align-items: center;
    justify-content: center;
    padding: 0 4px;
    transition: width 0.5s ease;
    border: 1px solid rgba(255, 200, 0, 0.6);
}

.nectar-text {
    font-size: 12px;
    font-weight: bold;
    color: #333;
    text-shadow: 0 0 2px rgba(255, 255, 255, 0.5);
}
</style>