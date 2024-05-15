<template>
    <div class="search-container">
        <h1 class="textR">Расширенный поиск и загрузка</h1>

        <!-- Форма загрузки файла -->
        <input type="file" @change="handleFileUpload" class="file-input" />
        <input class="search-input" v-model="uploadParams.title" placeholder="Название файла">
        <input class="search-input" v-model="uploadParams.author" placeholder="Автор">
        <input class="search-input" v-model="uploadParams.publisher" placeholder="Издательство">

        <!-- Кнопка для загрузки файла -->
        <button @click="uploadFile">Загрузить файл</button>

        <!-- Существующий функционал поиска -->
        <input class="search-input" v-model="searchParams.title" placeholder="Название">
        <input class="search-input" v-model="searchParams.author" placeholder="Автор">
        <input class="search-input" v-model="searchParams.publisher" placeholder="Издательство">
        <input class="search-input" v-model="searchParams.keywords" placeholder="Ключевые слова">
        <div class="year-container">
            <span class="year-label">{{ searchParams.year }}</span>
            <input type="range" class="year-slider" v-model="searchParams.year" min="1950" max="2024" placeholder="Год">
        </div>
        <div class="button-s-c">
            <button class="search-button" @click="searchPdf">Поиск</button>
            <button class="clear-button" @click="clearForm">Очистить</button>
        </div>
        <ul class="search-results" v-if="searchResults.length">
            <li v-for="document in searchResults" :key="document.id" @click="createPdf(document)">
                <h3>{{ document.title }}</h3>
                <p>Автор: {{document.author }}</p>
            </li>
        </ul>
        <div v-if="error" class="error">{{ error }}</div>
        <iframe v-if="pdfUrl" class="pdf-iframe" :src="pdfUrl" frameborder="0"></iframe>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                file: null, // Для хранения файла PDF
                uploadParams: {
                    title: '',
                    author: '',
                    publisher: ''
                },
                searchParams: {
                    title: '',
                    author: '',
                    publisher: '',
                    year: null,
                    keywords: ''
                },
                searchResults: [],
                pdfUrl: '', // URL для предпросмотра PDF
                error: ''
            };
        },
    methods: {
        handleFileUpload(event) {
            this.file = event.target.files[0]; // Получаем файл из события выбора файла
        },
        async uploadFile() {
            if (!this.file) {
                alert("Пожалуйста, выберите файл для загрузки.");
                return;
            }
            const formData = new FormData();
            formData.append('file', this.file);
            formData.append('title', this.uploadParams.title);
            formData.append('author', this.uploadParams.author);
            formData.append('publisher', this.uploadParams.publisher);

            try {
                const response = await axios.post('https://localhost:7085/Pdf/upload', formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                });
                alert("Файл успешно загружен.");
                console.log(response.data);
            } catch (error) {
                console.error('Ошибка при загрузке файла:', error);
                this.error = error.message;
            }
        },
        async searchPdf() {
            const params = new URLSearchParams();
            for (const key in this.searchParams) {
                if (this.searchParams[key] || this.searchParams[key] === 0) {
                    params.append(key, this.searchParams[key]);
                }
            }

            try {
                const response = await axios.get('https://localhost:7085/Pdf/search-pdf', { params });
                this.searchResults = response.data;
                if (this.searchResults.length > 0) {
                    await this.createPdf(this.searchResults[0]);
                } else {
                    console.error('Нет результатов для создания PDF');
                }
            } catch (error) {
                console.error('Ошибка при выполнении запроса:', error);
                this.error = error.message;
            }
        },
        async createPdf(documentData) {
            try {
                const response = await axios.post('https://localhost:7085/Pdf/create-pdf', documentData, {
                    responseType: 'blob',
                });
                const fileURL = window.URL.createObjectURL(new Blob([response.data], { type: 'application/pdf' }));
                this.pdfUrl = fileURL;  // Установка URL для отображения PDF в iframe
            } catch (error) {
                console.error('Ошибка при создании PDF:', error);
                this.error = 'Ошибка: ' + error.message;
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
    .error {
        color: red;
        font-size: 16px;
    }
    .button-s-c {
        margin-top: 20px;
    }

    .clear-button, .search-button {
        padding: 10px 20px;
        background-color: #007BFF;
        color: white;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        font-size: 16px;
    }

    .clear-button {
        background-color: #d3d3d3;
        color: #007BFF;
        margin-left: 50px;
    }

        .clear-button:hover, .search-button:hover {
            background-color: #0056b3;
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
