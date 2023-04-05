<template>
  <div class="component vCard">
    <img @click="setActive(vault)" class="img-fluid rounded" :src="vault.img" alt="">
    <div class="d-flex justify-content-between">
      <h4 class="p-2 text-dark text-start textBox">{{ vault.name }}</h4>
      <img class="profPic m-1" :src="vault.creator.picture" alt="">
    </div>
  </div>
</template>


<script>
import { AppState } from "../AppState.js";

export default {
  props: { vault: { type: Object, required: true } },
  setup() {
    return {
      setActive(vault) {
        AppState.activeVault = vault

      }
    }
  }
}
</script>


<style lang="scss" scoped>
.keepCard {
  background-color: #FEF6F0;
}

// .vCard {
//   max-width: 20vw;
// }

.profPic {
  width: 40px;
  height: 40px;
  border-radius: 50%;
}
</style>

