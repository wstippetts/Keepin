<template>
  <!-- <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p> -->
  <h1 class="m-3 p-2">Vault</h1>
  <section v-if="vaults" class="bricks">
    <div v-for="v in vaults">
      <VaultsCard :vault="v" />
    </div>
  </section>
  <h1 class="m-3 p-2">Keeps</h1>
  <section v-if="keeps" class="bricks">
    <div v-for="k in keeps">
      <KeepsCard :keep="k" />
    </div>
  </section>
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger.js"
import { keepsService } from "../services/keepsService.js"
import { vaultsService } from "../services/VaultsService.js"
import Pop from "../utils/Pop.js"
export default {
  setup() {
    // onMounted(() => {
    watchEffect(() => {
      if (AppState.account.id) {
        getVaultsByProfileId()
        getKeepsByProfileId()
      }
    })
    async function getKeepsByProfileId() {
      try {
        const userId = AppState.account.id
        await keepsService.getKeepsByProfileId(userId)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }

    async function getVaultsByProfileId() {
      try {
        const userId = AppState.account.id
        await vaultsService.getVaultsByProfileId(userId)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),


    }
  }
}
</script>

<style scoped lang="scss">
img {
  max-width: 100px;
}

$gap: .5em;

.bricks {
  columns: 300px;
  column-gap: $gap;

  &>div {
    margin-top: $gap;
    display: inline-block;
  }
}
</style>
