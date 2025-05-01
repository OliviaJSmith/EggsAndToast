<template>
  <div class="My Library">
    <!-- Component is auto-imported by `unplugin-vue-components`
    (either kebab-case or PascalCase works.) -->
    <c-input v-model="searchTerm" for="" />
  </div>
</template>

<script setup lang="ts">
useTitle("My Library");

const searchTerm = ref<string>();
const results = ref<string>[];
function searchBook() {
    fetch('https://openlibrary.org/search.json?q=the+lord+of+the+rings')
      .then(response => {
        if(repsonse.ok) {
          console.writeline(response.json());
          results = reponse.json()
        }
        else {
          throw new Error('request failed');
        }
      });
}
</script>
