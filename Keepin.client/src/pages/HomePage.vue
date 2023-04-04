<template>
  <section class="bricks">
    <div v-for="k in keeps">
      <KeepsCard :keep="k" />
    </div>
  </section>
</template>

<script>
import { computed, onMounted } from "vue";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/keepsService.js";
import Pop from "../utils/Pop.js";
import { AppState } from "../AppState.js";

export default {
  setup() {


    onMounted(() => {
      getKeeps()
    })

    async function getKeeps() {
      try {
        await keepsService.getKeeps()
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
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
