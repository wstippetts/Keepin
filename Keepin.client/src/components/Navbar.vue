<template>
  <nav class="navbar navbar-expand-lg navbar-dark body-bg px-3">
    <router-link class="navbar-brand d-flex border" :to="{ name: 'Home' }">
      <!-- <div class="d-flex flex-column align-items-center">
        <img alt="logo" src="../assets/img/cw-logo.png" height="45" />
      </div> -->
      <div class="">
        <img alt="Keepr-logo" src="../assets/img/Keepr-logo.png" height="45" />
      </div>
    </router-link>
    <button v-if="account.id" data-bs-toggle="modal" data-bs-target="#newKeep" class="btn btn-secondary mdi mdi-plus m-2">
      Create Keep
    </button>
    <button v-if="account.id" data-bs-toggle="modal" data-bs-target="#newVault"
      class=" btn btn-outline-secondary mdi mdi-plus m-2">
      Create Vault
    </button>
    <!-- <CreateDropdown class="d-flex justify-content-between" /> -->
    <button class="navbar-toggler bg-secondary" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <!-- <li>
          <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link>
        </li> -->
        <li>
          <Login />

        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
    </div>
  </nav>
</template>

<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import Login from './Login.vue'
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
export default {
  setup() {
    return {
      account: computed(() => AppState.account),

    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
