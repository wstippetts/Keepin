<template>
  <div class="component">

    <form @submit.prevent="handleSubmit">
      <div class="mb-3">
        <label for="name" class="form-label">Name:</label>
        <input required type="text" v-model="editable.name" class="form-control" id="name" placeholder="name..."
          name="name">
      </div>
      <div class="mb-3">
        <label for="bio" class="form-label">Description:</label>
        <input required type="text" v-model="editable.description" class="form-control" id="bio" placeholder="bio..."
          name="bio">
      </div>
      <div class="mb-3">
        <label for="picture" class="form-label">picture</label>
        <input required type="text" v-model="editable.img" class="form-control" id="picture" placeholder="picture..."
          name="picture">
      </div>
      <input v-model="editable.isPrivate" class="form-check-input" type="checkbox" value="" id="graduated">
      <label class="form-check-label" for="graduated">
        <p><b>
            Is this a private Vault?
          </b></p>
      </label>

      <div>
        <button type="submit" class="btn btn-outline-primary fs-2" data-bs-dismiss="modal">
          Post Changes
        </button>
      </div>
    </form>

  </div>
</template>


<script>
import { keepsService } from "../services/keepsService.js"

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async handleSubmit() {
        const newKeep = editable.value
        await keepsService.create(newKeep)

        editable.value = {}
      }
    }
  }
}
</script>


<style lang="scss" scoped></style>