<template>
    <div class="event-log" :class="{ 'has-events': events.length > 0 }">
        <div class="event-header">
            <h3>📋 Лента событий</h3>
            <span class="event-count">{{ events.length }}</span>
        </div>
        <div class="event-list">
            <div v-for="event in events" :key="event.id" class="event-item" :class="{ 'fading': isFading(event) }">
                <span class="event-time">{{ event.timestamp }}</span>
                <span class="event-message">{{ event.message }}</span>
            </div>
            <div v-if="events.length === 0" class="no-events">
                Нет событий
            </div>
        </div>
    </div>
</template>

<script setup>
import { defineProps, onMounted, onUnmounted } from 'vue'

const props = defineProps({
    events: {
        type: Array,
        required: true,
        default: () => []
    }
})

function isFading(event) {
    const age = Date.now() - event.createdAt
    return age > 12000
}

onMounted(() => {
    const eventList = document.querySelector('.event-list')
    if (eventList) {
        eventList.scrollTop = 0
    }
})
</script>

<style scoped>
.event-log {
    width: 300px;
    height: 900px;
    background: white;
    border: 3px solid #333;
    border-radius: 8px;
    display: flex;
    flex-direction: column;
    overflow: hidden;
    flex-shrink: 0;
    font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
}

.event-header {
    padding: 15px 20px;
    background: #f8f9fa;
    border-bottom: 2px solid #e9ecef;
    display: flex;
    justify-content: space-between;
    align-items: center;
    flex-shrink: 0;
}

.event-header h3 {
    margin: 0;
    font-size: 16px;
    color: #2c3e50;
}

.event-count {
    background: #007bff;
    color: white;
    padding: 2px 10px;
    border-radius: 12px;
    font-size: 12px;
    font-weight: bold;
}

.event-list {
    flex: 1;
    overflow-y: auto;
    padding: 10px;
}

.event-list::-webkit-scrollbar {
    width: 6px;
}

.event-list::-webkit-scrollbar-track {
    background: #f1f1f1;
    border-radius: 3px;
}

.event-list::-webkit-scrollbar-thumb {
    background: #c1c1c1;
    border-radius: 3px;
}

.event-item {
    padding: 8px 12px;
    margin-bottom: 6px;
    background: #f8f9fa;
    border-radius: 6px;
    border-left: 3px solid #007bff;
    display: flex;
    flex-direction: column;
    gap: 2px;
    transition: all 0.3s ease;
    opacity: 1;
    transform: translateX(0);
}

.event-item.fading {
    opacity: 0;
    transform: translateX(-20px);
}

.event-time {
    font-size: 11px;
    color: #6c757d;
    font-weight: 500;
}

.event-message {
    font-size: 13px;
    color: #2c3e50;
    word-wrap: break-word;
}

.no-events {
    text-align: center;
    color: #6c757d;
    padding: 40px 20px;
    font-size: 14px;
}

/* Анимация появления */
.event-item {
    animation: slideIn 0.3s ease-out;
}

@keyframes slideIn {
    from {
        opacity: 0;
        transform: translateX(-20px);
    }

    to {
        opacity: 1;
        transform: translateX(0);
    }
}
</style>