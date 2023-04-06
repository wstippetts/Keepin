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

      <div>
        <button type="submit" class="btn btn-outline-primary fs-2" data-bs-dismiss="modal">
          Post Keep
        </button>
      </div>
    </form>

  </div>
</template>


<script>
import { ref } from "vue"
import { keepsService } from "../services/keepsService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async handleSubmit() {
        try {
          const newKeep = editable.value
          await keepsService.createKeep(newKeep)

          editable.value = {}

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