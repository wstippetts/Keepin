<template>
  <!-- <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p> -->
  <div class="d-flex justify-content-around">
    <h1>{{ profile?.name }}</h1>
  </div>
  <div class="m-3">
    <img :src="profile?.picture" alt="">

  </div>
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
import { useRoute } from "vue-router"
export default {
  setup() {
    const vaults = null
    const route = useRoute()
    // onMounted(() => {
    watchEffect(() => {
      // if (AppState.account.id) {
      getVaultsByProfileId()
      getKeepsByProfileId()
      getProfileData()
      // }
    })

    async function getProfileData() {
      try {
        const profileId = route.params.profileId
        await vaultsService.getProfileData(profileId)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }

    // TODO go get the profile so then we can render details

    async function getKeepsByProfileId() {
      try {
        const userId = route.params.profileId
        await keepsService.getKeepsByProfileId(userId)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }

    async function getVaultsByProfileId() {
      try {
        const userId = route.params.profileId
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
      profile: computed(() => AppState.activeProfile)


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
