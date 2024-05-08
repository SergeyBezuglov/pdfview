<template>
    <div class="search-container">
        <h1 class="textR">Расширенный поиск</h1>
        <input class="search-input" v-model="searchParams.title" placeholder="Название">
        <input class="search-input" v-model="searchParams.author" placeholder="Автор">
        <input class="search-input" v-model="searchParams.publisher" placeholder="Издательство">
        <input class="search-input" v-model="searchParams.keywords" placeholder="Ключевые слова">
        <!-- Поле "Год" с ползунком -->
        <div class="year-container">
            <span class="year-label">{{ searchParams.year }}</span>
            <input type="range" class="year-slider" v-model="searchParams.year" min="1950" max="2024" placeholder="Год">
        </div>
        <div class="button-s-c">
            <button class="search-button" @click="searchPdf">Поиск</button>
            <button class="clear-button" @click="clearForm">Очистить</button>

        </div>
        
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
            },
            clearForm() {
                // Очистка всех полей формы
                this.searchParams.title = '';
                this.searchParams.author = '';
                this.searchParams.publisher = '';
                this.searchParams.year = null;
                this.searchParams.keywords = '';
                this.searchResults = [];
                this.pdfUrl = '';
            }
        }
    }
</script>


<style scoped>
    .button-s-c{
        margin-top:20px;
    }
    .clear-button {
        padding: 10px 20px;
        background-color: #d3d3d3; 
        color: #007BFF;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        margin-left: 50px; 
        font-size:16px;

    }

        .clear-button:hover {
            background-color: #e9e9e9; 
        }

    .year-container {
        display: flex;
        align-items: center;
        margin-top: 10px; 
    }

    .year-label {
        margin-right: 10px;
    }

    .year-slider {
        width: 90vh; 
    }

    .search-container {
        display: flex;
        flex-direction: column;
        align-items: center;
        background-color: #f4f4f4;
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
        font-size: 16px;
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
        width: 70%; 
        height: 70vh; 
        border: none; 
    }

    .textR {
        background: none;
        color: dimgrey;
        border: none;
        padding: 0;
        font: inherit; 
        font-size: 24px;
    }
</style>
