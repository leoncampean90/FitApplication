<template>
  <div id="app">
    <TopBar @navigate="handleNavigation" />
    <div class="main-content">
      <DashboardView v-if="currentView === 'dashboard'" />
      <AntrenoriList v-if="currentView === 'antrenori'" />
      <ClientiList v-if="currentView === 'clienti'" />
    </div>
  </div>
</template>

<script>
import TopBar from './components/TopBar.vue';
import DashboardView from './components/DashboardView.vue';
import AntrenoriList from './components/AntrenoriList.vue';
import ClientiList from './components/ClientiList.vue';

export default {
  name: 'App',
  components: {
    TopBar,
    DashboardView,
    AntrenoriList,
    ClientiList,
  },
  data() {
    return {
      currentView: 'dashboard',
    };
  },
  methods: {
    handleNavigation(view) {
      this.currentView = view;
    },
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

.main-content {
  padding: 20px;
}
</style>
