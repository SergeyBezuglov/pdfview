<template>
  <!--... -->
  <div class="search-box">
    <input v-model="query" placeholder="Search Query" class="search-input" @keyup.enter="searchPdf">
    <button @click="searchPdf" class="search-button">Search</button>
  </div>
  <!--... -->
  <div v-if="searchResults.length > 0" class="results-container">
    <div v-for="result in searchResults" :key="result" class="result-item">
      <h3>{{ result }}</h3>
      <div class="preview-container">
        <iframe :src="`/api/download-pdf?fileName=${result}`" class="pdf-preview"></iframe>
      </div>
      <button @click="downloadPdf(result)" class="download-button">Download</button>
    </div>
  </div>
  <!--... -->
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
    async searchPdf() {
      try {
        const response = await axios.get('http://localhost:7085/api/search-pdf', { params: { query: this.query } });
        this.searchResults = response.data;
      } catch (error) {
        console.error('Error:', error);
      }
    },
    downloadPdf(fileName) {
      window.open(`/api/download-pdf?fileName=${fileName}`, '_blank');
    }
  }
}
</script>

<style scoped>
.container {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.title {
  color: #017374;
  margin-bottom: 20px;
}

.search-box {
  margin-bottom: 20px;
}

.search-input {
  margin-right: 10px;
  padding: 8px;
  width: 300px;
  border: 2px solid #017374;
  border-radius: 4px;
}

.search-button {
  padding: 8px 16px;
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
  padding: 20px;
  margin-bottom: 20px;
}

.pdf-preview {
  width: 100%;
  height: 500px;
  border: none;
}

.download-button {
  margin-top: 10px;
  padding: 8px 16px;
  background-color: #017374;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.download-button:hover {
  background-color: #016362;
}

.no-results {
  color: #666;
}
</style>
