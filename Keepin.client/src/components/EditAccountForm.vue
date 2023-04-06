<template>
  <div class="component">
    <form @submit.prevent="handleSubmit">
      <div class="mb-3">
        <label for="name" class="form-label">name</label>
        <input required type="text" v-model="editable.name" class="form-control" id="name" placeholder="name..."
          name="name">
      </div>
      <div class="mb-3">
        <label for="picture" class="form-label">picture</label>
        <input required type="text" v-model="editable.picture" class="form-control" id="picture" placeholder="picture..."
          name="picture">
      </div>

      <div>
        <!-- <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button> -->
        <button type="submit" class="btn btn-outline-primary fs-2" data-bs-dismiss="modal" aria-label="Close">
          Post Changes
        </button>
      </div>
    </form>

  </div>
</template>


<script>
import { computed, ref } from "vue";
import { AppState } from "../AppState.js";
import { router } from "../router.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { accountService } from "../services/AccountService.js";


export default {
  setup() {
    const editable = ref({ ...AppState.account })
    return {
      editable,
      account: computed(() => AppState.account),
      async handleSubmit() {
        try {
          await accountService.editAccount(editable.value)
          editable.value = {}
          if (account?.id) {
            router.push({ name: 'Account', params: { accountId: account.id } })
          }
        } catch (error) {
          logger.error(error, 'error in edit account form')
          Pop.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped></style>