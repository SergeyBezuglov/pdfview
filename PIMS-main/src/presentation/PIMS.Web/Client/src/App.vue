<script lang="ts" setup>
  import SearchResults from '../src/views/user/SearchResults.vue';
  
</script>

<template>
  <v-app>
    <SearchResults/>
    
    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<style>
html {
  -ms-overflow-style: none; /* IE and Edge */
  scrollbar-width: none; /* Firefox */
}
html::-webkit-scrollbar {
  display: none;
}
</style>
