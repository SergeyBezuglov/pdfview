<template>
  <div class="search-container">
    <input class="search-input" v-model="query" @keyup.enter="searchPdf" placeholder="Введите ключевое слово">
    <button class="search-button" @click="searchPdf">Поиск</button>
    <ul class="search-results" v-if="searchResults.length">
        <li v-for="(fileName, index) in searchResults" :key="index" @click="selectPdf(fileName)">
            {{ fileName }}
        </li>
    </ul>
    <!-- Добавляем iframe для предпросмотра PDF -->
    <iframe v-if="pdfUrl" class="pdf-iframe" :src="pdfUrl" frameborder="0"></iframe>
  </div>
</template>

<script>
import axios from 'axios';

export default {
    data() {
        return {
            query: '',
            searchResults: [],
            error: '',
            pdfUrl: '' // URL для предпросмотра PDF
        };
    },
    methods: {
        async searchPdf() {
            try {
                const response = await axios.get(`https://localhost:7085/Pdf/search-pdf?query=${encodeURIComponent(this.query)}`);
                this.searchResults = response.data;
            } catch (error) {
                console.error('Ошибка при выполнении запроса:', error);
                this.error = error.message;
            }
        },
        async selectPdf(fileName) {
    try {
        const response = await axios.get(`https://localhost:7085/Pdf/download-pdf?fileName=${encodeURIComponent(fileName)}`, {
            responseType: 'blob', // Важно для обработки бинарных данных
        });
        const fileURL = window.URL.createObjectURL(new Blob([response.data], { type: 'application/pdf' }));
        this.pdfUrl = fileURL; // Установка URL для iframe
    } catch (error) {
        console.error('Ошибка при скачивании файла:', error);
        this.error = error.message;
    }
}
}
}
</script>

<style scoped>
.search-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 20px;
}

.search-input {
  width: 50%;
  padding: 10px;
  margin-bottom: 10px;
  border: 2px solid #ddd;
  border-radius: 5px;
  font-size: 16px;
}

.search-button {
  padding: 10px 20px;
  background-color: #4CAF50;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.search-button:hover {
  background-color: #45a049;
}

.search-results {
  list-style-type: none;
  padding: 0;
}

.search-results li {
  padding: 8px;
  cursor: pointer;
  background-color: #ffffff;
  border-bottom: 1px solid #cccccc;
}

.search-results li:hover {
  background-color: #f5f5f5;
}

.pdf-iframe {
  width: 70%;  /* или другой размер по вашему выбору */
  height: 70vh; /* высота iframe */
  border: none; /* убрать рамку */
}
</style>