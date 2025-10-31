<template>
  <div class="top-bar">
    <div class="logo">AllFit</div>
    <nav>
      <ul>
        <li><a href="#" @click.prevent="$emit('navigate', 'dashboard')">Dashboard</a></li>
        <li><a href="#" @click.prevent="$emit('navigate', 'antrenori')">Antrenori</a></li>
        <li><a href="#" @click.prevent="$emit('navigate', 'clienti')">Clienti</a></li>
      </ul>
    </nav>
  </div>
</template>

<script>
export default {
  name: 'TopBar',
};
</script>

<style scoped>
.top-bar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: #2c3e50;
  color: white;
  padding: 10px 20px;
}

.logo {
  font-size: 24px;
  font-weight: bold;
}

nav ul {
  list-style: none;
  padding: 0;
  margin: 0;
  display: flex;
}

nav li {
  margin-left: 20px;
}

nav a {
  color: white;
  text-decoration: none;
  font-size: 18px;
}
</style>
