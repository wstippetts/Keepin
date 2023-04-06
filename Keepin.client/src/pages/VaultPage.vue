<template>
  <!-- TODO add a button to remove the vault here..make sure to conditionally render -->
  <div class="component">
    <div class="text-center moveTrash">
      <img class="vImg " :src="vault?.img" alt="">
      <button v-if="account.id == vault.creatorId" @click="removeVault()"
        class="moveTrashIn btn btn-md mdi btn-danger mdi-delete selectable">
      </button>
    </div>
    <h1 class="m-3 p-2">Keeps</h1>
    <section v-if="keeps" class="bricks">
      <div v-for="k in keeps">
        <div>
          <!-- <button v-if="account.id == vault.creatorId" @click="removeVaultKeep(keeps.vaultKeepId)"
            class="btn btn-md mdi btn-danger mdi-delete selectable">
          </button> -->
          <KeepsCard :keep="k" />
        </div>
      </div>
    </section>
  </div>
</template>

<script>
import { onMounted, watchEffect } from "vue";
import { vaultsService } from "../services/VaultsService.js";
import { useRoute, useRouter } from "vue-router";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const route = useRoute()
    const router = useRouter()

    async function getVaultsByProfileId() {
      try {
        const vaultId = route.params.vaultId
        const vault = await vaultsService.getVaultsById(vaultId)
        // if (vault?.isPrivate == true && vault?.creatorId != AppState.account.id) {
        // }
      } catch (error) {
        router.push({ name: 'Home' })
        logger.error(error)
        Pop.error(error)
      }
    }
    async function getKeepsByVaultId() {
      try {
        const vaultId = route.params.vaultId
        await vaultsService.getKeepsByVaultId(vaultId)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }



    // onMounted(() => {
    watchEffect(() => {
      getVaultsByProfileId()
      getKeepsByVaultId()
    })
    return {
      keeps: computed(() => AppState.keeps),
      vault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account),

      async removeVaultKeep(vaultKeepId) {
        try {
          await vaultsService.removeVaultKeep(vaultKeepId)
        } catch (error) {
          logger.error(error)
          Pop.error(error)
        }
      },

      async removeVault() {
        try {
          const vaultId = route.params.vaultId
          const check = await Pop.confirm("Are you sure you want to delete this vault?")
          if (!check) return;
          await vaultsService.removeVault(vaultId)
          router.push({ name: 'Home' })
        } catch (error) {
          logger.error(error)
          Pop.error(error)
        }
      }

    }
  }
}
</script >

<style lang="scss" scoped>
$gap: .5em;

.bricks {
  columns: 300px;
  column-gap: $gap;

  &>div {
    margin-top: $gap;
    display: inline-block;
  }

  .vImg {
    width: 70vw;
    height: 30vh;
    object-fit: cover;
  }

  .moveTrash {
    position: relative;
  }

  .moveTrashIn {
    position: absolute;
    top: -75px;
    right: -475px;
    width: 100%;
  }

}
</style>