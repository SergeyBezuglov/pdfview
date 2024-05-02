<template>
  <div class="search-container">
    <h1 class="title">PDF Search Engine</h1>
    <div class="search-box">
      <input v-model="searchParams.author" placeholder="Author" class="search-input">
      <input v-model="searchParams.publisher" placeholder="Publisher" class="search-input">
      <input v-model="searchParams.keywords" placeholder="Keywords" class="search-input">
      <button @click="searchDocuments" class="search-button">Search</button>
    </div>
    <div v-if="documents.length > 0" class="results-container">
      <div v-for="doc in documents" :key="doc.filePath" class="result-item">
        <h5>{{ doc.title }}</h5>
        <p>{{ doc.author }}</p>
        <p>{{ doc.publisher }}</p>
        <button @click="downloadFile(doc.filePath)">Download</button>
        <iframe :src="'/pdfs/' + doc.filePath" class="pdf-preview"></iframe>
      </div>
    </div>
    <div v-else class="no-results">No results found.</div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      searchParams: {
        author: '',
        publisher: '',
        keywords: ''
      },
      documents: []
    };
  },
  methods: {
    searchDocuments() {
      axios.get('/api/search', { params: this.searchParams })
        .then(response => {
          this.documents = response.data;
        })
        .catch(error => console.error('Error:', error));
    },
    downloadFile(filePath) {
      window.open(`/api/download/${filePath}`, '_blank');
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
  font-family: 'Arial', sans-serif;
}

.title {
  color: #017374;
}

.search-box {
  margin-bottom: 20px;
}

.search-input {
  margin: 10px;
  padding: 8px;
  width: 200px;
  border: 2px solid #017374;
  border-radius: 4px;
}

.search-button {
  padding: 10px 20px;
  background-color: #017374;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.search-button:hover {
  background-color: #016362;
}

.results-container {
  width: 80%;
}

.result-item {
  border: 1px solid #ccc;
  padding: 10px;
  margin-bottom: 20px;
}

.pdf-preview {
  width: 100%;
  height: 500px;
  border: none;
}

.no-results {
  color: #666;
}
</style>
