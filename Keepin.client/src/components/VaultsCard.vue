<template>
  <div class="component keepCard">
    <router-link :to="{ name: 'VaultDetailsPage', params: { vaultId: vault.id } }">
      <img @click="setActive(vault)" class="img-fluid rounded" :src="vault.img" alt="">
    </router-link>
    <div class="d-flex justify-content-between tranUp ">
      <h4 class="p-2 text-dark text-start textShadow">{{ vault.name }}</h4>
      <router-link :to="{ name: 'ProfileDetailsPage', params: { profileId: vault.creatorId } }">

        <img class="profPic m-1" :src="vault.creator.picture" alt="">
      </router-link>

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

// .tranUp {
//   position: absolute;
//   transform: translate(15px, -60px);
// }

.textShadow {
  filter: text-shadow(0 0 0.2rem #FFF);

}
</style>

