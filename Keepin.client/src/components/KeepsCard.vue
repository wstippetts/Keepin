<template >
  <div class="component keepCard" data-bs-toggle="modal" data-bs-target="#exampleModal">

    <img @click="setActive(this.keep)" class="img-fluid rounded" :src="keep?.img" alt="">
    <div class="d-flex justify-content-between">
      <h4 class="p-2 text-dark text-start textBox">{{ keep?.name }}</h4>
      <router-link :to="{ name: 'ProfileDetailsPage', params: { profileId: keep.creatorId } }">

        <img class="profPic m-1" :src="keep.creator.picture" alt="">
      </router-link>
    </div>
  </div>
</template>


<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/keepsService.js";
import { vaultsService } from "../services/VaultsService.js";
import { useRoute } from "vue-router";

export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {
    const route = useRoute();
    return {
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault),

      async setActive(keep) {
        await keepsService.setActive(keep)

      }

    };
  },
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

