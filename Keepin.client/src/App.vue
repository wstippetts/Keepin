<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <!-- <footer class="bg-dark text-light">
    Made with 💖 by CodeWorks
  </footer> -->
  <Modal id="keepDetails">
    <!-- <template #KeepDetails> -->
    <KeepDetails />
    <!-- </template> -->
  </Modal>
  <Modal id="editAccount">
    <!-- <template #EditAccountForm> -->
    <EditAccountForm />
    <!-- </template> -->
  </Modal>
  <Modal id="newKeep">
    <KeepForm />
  </Modal>
  <Modal id="newVault">
    <VaultForm />
  </Modal>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import EditAccountForm from "./components/EditAccountForm.vue"
import KeepForm from "./components/KeepForm.vue"
import KeepDetails from "./components/KeepDetails.vue"
import VaultForm from "./components/VaultForm.vue"

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, EditAccountForm, KeepForm, KeepDetails, VaultForm }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
