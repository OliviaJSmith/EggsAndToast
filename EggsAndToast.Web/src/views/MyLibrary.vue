<template>
  <div class="My Library">
    <v-btn @click="searchBook()">search</v-btn>

    <div v-if="results">{{ results }}</div>
  </div>
</template>

<script setup lang="ts">
useTitle("My Library");

const results = ref<any>();
async function searchBook() {
  results.value = await fetch(
    "https://openlibrary.org/search.json?q=the+lord+of+the+rings",
  ).then(function (response) {
    if (response.ok) {
      return response.json();
    } else {
      throw new Error("request failed");
    }
  });
}
</script>
