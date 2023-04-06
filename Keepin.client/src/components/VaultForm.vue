<template>
  <div class="component">

    <form @submit.prevent="handleSubmit">
      <div class="mb-3">
        <label for="name" class="form-label">Name:</label>
        <input required type="text" v-model="editable.name" class="form-control" id="name" placeholder="name..."
          name="name">
      </div>
      <div class="mb-3">
        <label for="Description" class="form-label">Description:</label>
        <input required type="text" v-model="editable.description" class="form-control" id="Description"
          placeholder="Description..." name="Description">
      </div>
      <div class="mb-3">
        <label for="picture" class="form-label">picture</label>
        <input required type="text" v-model="editable.img" class="form-control" id="picture" placeholder="picture..."
          name="picture">
      </div>
      <input v-model="editable.isPrivate" class="form-check-input" type="checkbox" value="" id="isPrivate">
      <label class="form-check-label" for="isPrivate">
        <p><b>
            Is this a private Vault?
          </b></p>
      </label>

      <div>
        <button type="submit" class="btn btn-outline-primary fs-2" data-bs-dismiss="modal">
          Post new Vault
        </button>
      </div>
    </form>

  </div>
</template>


<script>
import { ref } from "vue"
import { vaultsService } from "../services/VaultsService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { AppState } from "../AppState.js"

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async handleSubmit() {
        try {

          const newVault = editable.value
          await vaultsService.createVault(newVault)
          editable.value = {}
          AppState.vaults.push(newVault)

        } catch (error) {
          logger.error(error)
          Pop.error(error)
        }

      }
    }
  }
}
</script>


<style lang="scss" scoped></style>