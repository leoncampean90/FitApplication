<template>
  <div class="clienti">
    <h1>Clienti</h1>

    <div class="client-list">
      <div class="client-card add-card" @click="showCreateForm = true">
        <div class="add-icon">+</div>
        <h3>Add Client</h3>
      </div>

      <div class="client-card" v-for="client in clienti" :key="client.id_client">
        <img
          src="data:image/svg+xml;charset=UTF-8,<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' fill='currentColor'><path d='M12 12c2.21 0 4-1.79 4-4s-1.79-4-4-4-4 1.79-4 4 1.79 4 4 4zm0 2c-2.67 0-8 1.34-8 4v2h16v-2c0-2.66-5.33-4-8-4z'/></svg>"
          alt="Client Avatar"
          class="avatar"
        />
        <div class="client-info">
          <h3>{{ client.first_name }} {{ client.last_name }}</h3>
          <p>@{{ client.username }}</p>
          <small>Tip: {{ client.tip }}</small>
        </div>
        <button class="delete-btn" @click="deleteClient(client.id_client)">X</button>
      </div>
    </div>

    <!-- Modal for creating new client -->
    <div class="modal" v-if="showCreateForm">
      <div class="modal-content">
        <span class="close" @click="showCreateForm = false">&times;</span>
        <h2>Create Client</h2>

        <form @submit.prevent="createClient">
          <input v-model="newClient.first_name" placeholder="First Name" required />
          <input v-model="newClient.last_name" placeholder="Last Name" required />
          <input v-model="newClient.nr_telefon" placeholder="Phone Number" required />
          <input v-model="newClient.username" placeholder="Username" required />
          <input v-model="newClient.password" placeholder="Password" type="password" required />

          <!-- Tip selector (required) -->
          <select v-model="newClient.tip" required>
            <option disabled value="">Select tip</option>
            <option value="regular">Regular</option>
            <option value="premium">Premium</option>
          </select>

          <!-- Trainer selector (optional, for future use) -->
          <select v-model="newClient.antrenor_id">
            <option value="">Select trainer (optional for now)</option>
            <option v-for="a in antrenori" :key="a.id_antrenor" :value="a.id_antrenor">
              {{ a.first_name }} {{ a.last_name }}
            </option>
          </select>

          <button type="submit" :disabled="isSubmitting">
            {{ isSubmitting ? 'Creating...' : 'Create' }}
          </button>

          <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'ClientiList',
  data() {
    return {
      clienti: [],
      antrenori: [],
      showCreateForm: false,
      isSubmitting: false,
      errorMessage: '',
      newClient: {
        first_name: '',
        last_name: '',
        nr_telefon: '',
        username: '',
        password: '',
        tip: 'regular', // default value so "tip" is always sent
        antrenor_id: ''
      }
    };
  },
  methods: {
    // Fetch all clients
    fetchClienti() {
      axios
        .get('https://localhost:7181/api/Clienti')
        .then((response) => {
          this.clienti = response.data;
        })
        .catch((error) => {
          console.error('Error fetching clients:', error);
        });
    },

    // Fetch all trainers
    fetchAntrenori() {
      axios
        .get('https://localhost:7181/api/Antrenori')
        .then((response) => {
          this.antrenori = response.data;
        })
        .catch((error) => {
          console.error('Error fetching trainers:', error);
        });
    },

    // Create a new client (currently hardcoded trainer id = 38)
    createClient() {
      this.errorMessage = '';
      this.isSubmitting = true;

      const url = 'https://localhost:7181/api/Clienti/38'; // hardcoded trainer for now

      const payload = {
        first_name: this.newClient.first_name,
        last_name: this.newClient.last_name,
        nr_telefon: this.newClient.nr_telefon,
        username: this.newClient.username,
        password: this.newClient.password,
        tip: this.newClient.tip
      };

      axios
        .post(url, payload, {
          headers: { 'Content-Type': 'application/json' }
        })
        .then((response) => {
          this.clienti.push(response.data);
          this.resetNewClient();
          this.showCreateForm = false;
        })
        .catch((error) => {
          console.error('Error creating client:', error);
          const apiErrors = error?.response?.data?.errors;
          if (apiErrors) {
            this.errorMessage = Object.values(apiErrors).flat().join(' ');
          } else {
            this.errorMessage =
              error?.response?.data?.message ||
              error?.response?.data ||
              'Failed to create client. Please check your input.';
          }
        })
        .finally(() => {
          this.isSubmitting = false;
        });
    },

    // Delete client by ID
    deleteClient(id) {
      axios
        .delete(`https://localhost:7181/api/Clienti/${id}`)
        .then(() => {
          this.clienti = this.clienti.filter((c) => c.id_client !== id);
        })
        .catch((error) => {
          console.error('Error deleting client:', error);
        });
    },

    // Reset form
    resetNewClient() {
      this.newClient = {
        first_name: '',
        last_name: '',
        nr_telefon: '',
        username: '',
        password: '',
        tip: 'regular',
        antrenor_id: ''
      };
      this.errorMessage = '';
    }
  },

  // On component mount
  created() {
    this.fetchClienti();
    this.fetchAntrenori();
  }
};
</script>

<style scoped>
.clienti {
  padding: 20px;
}

.client-list {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}

.client-card {
  position: relative;
  width: 200px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  overflow: hidden;
  text-align: center;
  padding: 20px;
  cursor: pointer;
  transition: transform 0.2s;
}

.client-card:hover {
  transform: translateY(-5px);
}

.add-card {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  border: 2px dashed #ccc;
}

.add-icon {
  font-size: 48px;
  color: #ccc;
}

.avatar {
  width: 100px;
  height: 100px;
  border-radius: 50%;
  margin-bottom: 15px;
}

.client-info h3 {
  margin: 0;
  font-size: 18px;
}

.client-info p {
  margin: 5px 0 0;
  color: #777;
}

.delete-btn {
  position: absolute;
  top: 10px;
  right: 10px;
  background: none;
  border: none;
  font-size: 18px;
  cursor: pointer;
  color: #aaa;
}

.delete-btn:hover {
  color: #f00;
}

.modal {
  position: fixed;
  z-index: 1;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  overflow: auto;
  background-color: rgba(0, 0, 0, 0.4);
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

form input,
form select {
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

.error {
  margin-top: 8px;
  color: #e11d48;
  font-size: 0.9rem;
}
</style>
