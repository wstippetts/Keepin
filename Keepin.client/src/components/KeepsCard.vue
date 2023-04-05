<template>
  <div class="component keepCard">
    <img @click="setActive(keep)" class="img-fluid rounded" :src="keep?.img" alt="">
    <div class="d-flex justify-content-between">
      <h4 class="p-2 text-dark text-start textBox">{{ keep?.name }}</h4>
      <img class="profPic m-1" :src="keep?.creator.picture" alt="">
    </div>
  </div>
</template>


<script>
import { AppState } from "../AppState.js";

export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {
      setActive(keep) {
        AppState.activeKeep = keep

      }
    }
  }
}
</script>


<style lang="scss" scoped>
.keepCard {
  background-color: #FEF6F0;
}

.profPic {
  width: 40px;
  height: 40px;
  border-radius: 50%;
}
</style>

