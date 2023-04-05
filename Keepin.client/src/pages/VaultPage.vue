<template>
  <div class="component">


  </div>
</template>


<script>
import { onMounted } from "vue";
import { vaultsService } from "../services/VaultsService.js";

export default {
  setup() {
    onMounted(() => {
      getVaultsByProfileId()
    })

    async function getVaultsByProfileId() {
      try {
        userId = route.params.vaultId
        await vaultsService.getVaultsById(vaultId)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }
    return {}
  }
}
</script>


<style lang="scss" scoped></style>