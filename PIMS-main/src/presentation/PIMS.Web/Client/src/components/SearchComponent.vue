<template>
  <div class="search-container">
    <input class="search-input" v-model="query" @keyup.enter="searchPdf" placeholder="Введите ключевое слово">
    <button class="search-button" @click="searchPdf">Поиск</button>
    <ul class="search-results" v-if="searchResults.length">
      <li v-for="(file, index) in searchResults" :key="index" @click="selectPdf(file)">
        {{ file }}
      </li>
    </ul>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      query: '',
      searchResults: []
    };
  },
  methods: {
    searchPdf() {
      this.$emit('reset-selection');
      axios.get(`http://localhost:7085/pdfcontroller/search-pdf?query=${this.query}`)
        .then(response => {
          this.searchResults = response.data;
        })
        .catch(error => {
          console.error('There was an error!', error);
        });
    },
    selectPdf(fileName) {
      this.$emit('pdf-selected', fileName);
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
</style>