<template>
  <!-- <h1>is it working?</h1> -->
  <div class="component">

    <div class="row">

      <div class="col-6">
        <div class="resize h-100" :style="{ 'background-image': 'url(' + keep?.img + ')' }">

          <img class="keepImg blur" :src="keep?.img" alt="">
        </div>
      </div>
      <div class="col-6">
        <div class="text-end">
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="d-flex justify-content-center">
          <i class="px-5 py-2">
            <button>
              <img src="../assets/img/ViewImg.png" alt="">
            </button>
            {{ keep?.views }}
          </i>
          <i class="px-5 py-2">
            <img src="../assets/img/KeptImg.png" alt="">
            {{ keep?.kept }}
          </i>
        </div>
        <button data-bs-dismiss="modal" aria-label="Close" v-if="account.id == keep?.creatorId" @click="removeKeep(keep)"
          class="btn btn-md mdi btn-danger mdi-delete selectable">
        </button>
        <h1>{{ keep?.name }}</h1>
        <h5>{{ keep?.description }}</h5>
        <div>
          <!-- TODO add keep to vault -->
          <form @submit.prevent="handleSubmit">

            <div class="mb-3">
              <label for="name" class="form-label">Your Vaults:</label>
              <select name="name" id="name" v-model="editable.name">
                <option v-for="n in myVaultNames" :value="n" class="text-capitalize">{{ n }}</option>
              </select>
            </div>

            <div>
              <button type="submit" class="btn btn-primary" data-bs-dismiss="modal">
                {{ 'Add to Vault' }}
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>


    <!-- <img :src="keep.img" alt="">
    {{ keep.name }}
    {{ keep.description }}
    {{ keep.views }}
    <img class="" :src="keep.creator.picture" alt="">
-->

  </div>
</template>


<script>
import { computed, ref } from "vue";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/keepsService.js";
import { useRoute, useRouter } from "vue-router";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";


export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    const editable = ref({});
    const router = useRouter();
    const route = useRoute();
    return {
      editable,
      // myVaults: AppState.myVaults,
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),
      myVaultNames: computed(() => {
        // Create an array of all vault names
        const allVaultNames = AppState.myVaults?.map(vault => vault.name);
        // Filter out any duplicates and return the unique values
        return Array.from(new Set(allVaultNames));
      }),
      async removeKeep(keep) {
        try {
          logger.log()
          const check = await Pop.confirm("Are you sure you want to delete this keep?")
          if (!check) return;
          await keepsService.removeKeep(keep)
        } catch (error) {
          console.error(error)
        }
      },
      async handleSubmit() {
        try {
          const kept = editable.value
          await keepsService.addKeepToVault(kept)
        } catch (error) {
          logger.error(error)
          Pop.error(error)

        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.keepImg {
  height: 60vh;
  width: 100%;
  object-fit: contain;

}

.blur {
  backdrop-filter: blur(12px);

}

.resize {
  object-fit: cover;
  object-position: center;
}
</style>