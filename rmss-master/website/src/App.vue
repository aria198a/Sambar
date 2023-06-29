<template>
  <Header v-if="states.isLogin"></Header>
  <router-view/>
</template>
<script>
import Header from './components/Header.vue';
import { defineComponent, reactive, computed } from 'vue';
import { useStore } from "vuex";
export default defineComponent ({
  setup() {
    const store = useStore();
    const states = reactive({
        isLogin: computed(()=> store.state.auth.isLogin),
    });
    return {
      states
    }
  },
  components: {
    Header
  }
})
</script>