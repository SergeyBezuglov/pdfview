<template>
    <div class="search-container">
        <h1 class="textR">Расширенный поиск</h1>
        <input class="search-input" v-model="searchParams.title" placeholder="Название">
        <input class="search-input" v-model="searchParams.author" placeholder="Автор">
        <input class="search-input" v-model="searchParams.publisher" placeholder="Издательство">
        <input type="number" class="search-input" v-model="searchParams.year" placeholder="Год">
        <input class="search-input" v-model="searchParams.keywords" placeholder="Ключевые слова">
        <button class="search-button" @click="searchPdf">Поиск</button>
        <ul class="search-results" v-if="searchResults.length">
            <li v-for="(fileName, index) in searchResults" :key="index" @click="selectPdf(fileName)">
                {{ fileName }}
            </li>
        </ul>
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
                pdfUrl: '', // URL для предпросмотра PDF
                searchParams: {
                    title: '',
                    author: '',
                    publisher: '',
                    year: null,
                    keywords: ''
                }
            };
        },
        methods: {
            async searchPdf() {
                const params = new URLSearchParams();
                Object.keys(this.searchParams).forEach(key => {
                    if (this.searchParams[key]) { // Добавляем только непустые параметры
                        params.append(key, this.searchParams[key]);
                    }
                });

                try {
                    const response = await axios.get(`https://localhost:7085/Pdf/search-pdf`, { params });
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
  background-color: #f4f4f4; /* Светло-серый фон, если это необходимо */
  padding: 20px;
  width: 100%;
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
    background-color: #007BFF;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.search-button:hover {
background-color: #0056b3;
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
.textR{
    background: none;
    color: dimgrey; /* Цвет текста, можете выбрать любой */
    border: none;
    padding: 0;
    font: inherit; /* Унаследовать шрифт от родителя */
   font-size:24px;
}
</style>