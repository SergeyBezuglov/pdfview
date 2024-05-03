<template>
  <div class="viewer-container">
    <button class="download-button" @click="downloadPdf">Скачать PDF</button>
    <canvas ref="pdfCanvas" class="pdf-canvas"></canvas>
  </div>
</template>
  
<script>
import * as pdfjsLib from 'pdfjs-dist/legacy/build/pdf'; // Импорт pdf.js

export default {
  props: ['fileName'],
  data() {
    return {
      pdfDoc: null,
      currentPage: 1,
      totalPages: 0
    };
  },
  watch: {
    fileName: {
      immediate: true,
      handler(value) {
        this.loadPdf(value);
      }
    }
  },
  methods: {
    async loadPdfFile(url) {
      const loadingTask = pdfjsLib.getDocument(url);
      try {
        const pdfDoc = await loadingTask.promise;
        console.log('PDF загружен');
        return pdfDoc;
      } catch (error) {
        console.error('Ошибка загрузки PDF:', error);
        return null;
      }
    },
    async loadPdf(fileName) {
      const pdfDoc = await this.loadPdfFile(`https://localhost:7085/Pdf/download-pdf?fileName=${fileName}`);
      if (pdfDoc) {
        this.pdfDoc = pdfDoc;
        this.totalPages = pdfDoc.numPages;
        this.renderPage(this.currentPage);
      }
    },
    async renderPage(pageNumber) {
      if (!this.pdfDoc) return;
      const page = await this.pdfDoc.getPage(pageNumber);
      const viewport = page.getViewport({ scale: 1.5 });
      const canvas = this.$refs.pdfCanvas;
      const context = canvas.getContext('2d');
      canvas.height = viewport.height;
      canvas.width = viewport.width;
      const renderContext = {
        canvasContext: context,
        viewport: viewport
      };
      await page.render(renderContext).promise;
    },
    async nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
        await this.renderPage(this.currentPage);
      }
    },
    async prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
        await this.renderPage(this.currentPage);
      }
    }
  }
}
</script>
  <style scoped>
  .viewer-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    margin-top: 20px;
  }
  
  .download-button {
    padding: 10px 20px;
    background-color: #007BFF;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    margin-bottom: 20px;
  }
  
  .download-button:hover {
    background-color: #0056b3;
  }
  
  .pdf-canvas {
    border: 1px solid #ddd;
    border-radius: 4px;
  }
  </style>