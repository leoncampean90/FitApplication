<template>
  <div class="antrenori">
    <h1>Antrenori</h1>

    <div class="actions">
      <div class="add-card" @click="showCreateForm = true">
        <div class="add-icon">+</div>
        <h3>Add Antrenor</h3>
      </div>
    </div>

    <div class="modal" v-if="showCreateForm">
      <div class="modal-content">
        <span class="close" @click="showCreateForm = false">&times;</span>
        <h2>Create Antrenor</h2>
        <form @submit.prevent="createAntrenor">
          <input v-model="newAntrenor.first_name" placeholder="First Name" required />
          <input v-model="newAntrenor.last_name" placeholder="Last Name" required />
          <input v-model="newAntrenor.tip" placeholder="Tip" />
          <input v-model="newAntrenor.tip_abonament" placeholder="Tip Abonament" />
          <input v-model="newAntrenor.nr_telefon" placeholder="Nr Telefon" />
          <input v-model="newAntrenor.mail" placeholder="Mail" />
          <input v-model="newAntrenor.username" placeholder="Username" required />
          <input v-model="newAntrenor.password" placeholder="Password" type="password" required />
          <button type="submit">Create</button>
        </form>
      </div>
    </div>

    <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>First Name</th>
          <th>Last Name</th>
          <th>Tip</th>
          <th>Tip Abonament</th>
          <th>Nr Telefon</th>
          <th>Mail</th>
          <th>Username</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="antrenor in antrenori" :key="antrenor.antrenor_id">
          <td>{{ antrenor.antrenor_id }}</td>
          <td>{{ antrenor.first_name }}</td>
          <td>{{ antrenor.last_name }}</td>
          <td>{{ antrenor.tip }}</td>
          <td>{{ antrenor.tip_abonament }}</td>
          <td>{{ antrenor.nr_telefon }}</td>
          <td>{{ antrenor.mail }}</td>
          <td>{{ antrenor.username }}</td>
          <td><button @click="deleteAntrenor(antrenor.antrenor_id)">Delete</button></td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'AntrenoriList',
  data() {
    return {
      antrenori: [],
      showCreateForm: false,
      newAntrenor: {
        first_name: '',
        last_name: '',
        tip: '',
        tip_abonament: '',
        nr_telefon: '',
        mail: '',
        username: '',
        password: ''
      }
    };
  },
  methods: {
    fetchAntrenori() {
      axios.get('https://localhost:7181/api/Antrenori')
        .then(response => {
          this.antrenori = response.data;
        })
        .catch(error => {
          console.error(error);
        });
    },
    createAntrenor() {
      axios.post('https://localhost:7181/api/Antrenori', this.newAntrenor)
        .then(response => {
          this.antrenori.push(response.data);
          this.resetNewAntrenor();
          this.showCreateForm = false;
        })
        .catch(error => {
          console.error(error);
        });
    },
    deleteAntrenor(id) {
      axios.delete(`https://localhost:7181/api/Antrenori/${id}`)
        .then(() => {
          this.antrenori = this.antrenori.filter(a => a.antrenor_id !== id);
        })
        .catch(error => {
          console.error(error);
        });
    },
    resetNewAntrenor() {
      this.newAntrenor = {
        first_name: '',
        last_name: '',
        tip: '',
        tip_abonament: '',
        nr_telefon: '',
        mail: '',
        username: '',
        password: ''
      };
    }
  },
  created() {
    this.fetchAntrenori();
  }
};
</script>

<style scoped>
.antrenori {
  padding: 20px;
}

.actions {
  margin-bottom: 20px;
}

.add-card {
  display: inline-block;
  width: 200px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  overflow: hidden;
  text-align: center;
  padding: 20px;
  cursor: pointer;
  transition: transform 0.2s;
  border: 2px dashed #ccc;
}

.add-card:hover {
  transform: translateY(-5px);
}

.add-icon {
  font-size: 48px;
  color: #ccc;
}

.modal {
  position: fixed;
  z-index: 1;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  overflow: auto;
  background-color: rgba(0,0,0,0.4);
}

.modal-content {
  background-color: #fefefe;
  margin: 15% auto;
  padding: 20px;
  border: 1px solid #888;
  width: 80%;
  max-width: 500px;
  border-radius: 8px;
}

.close {
  color: #aaa;
  float: right;
  font-size: 28px;
  font-weight: bold;
}

.close:hover,
.close:focus {
  color: black;
  text-decoration: none;
  cursor: pointer;
}

form input {
  width: calc(100% - 20px);
  padding: 10px;
  margin-bottom: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

form button {
  width: 100%;
  padding: 10px;
  background-color: #2c3e50;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  border-radius: 8px;
  overflow: hidden;
}

th, td {
  border-bottom: 1px solid #ddd;
  padding: 12px 15px;
  text-align: left;
}

th {
  background-color: #f4f6f8;
  font-weight: 600;
}

tr:hover {
  background-color: #f1f1f1;
}
</style>
