<template>
  <div class="component">
    <h1>is it working?</h1>

  </div>
</template>


<script>
export default {
  props: { vault: { type: Object, required: true } },
  setup() {
    return {
      vault: computed(() => AppState.activeVault)
    }
  }
}
</script>


<style lang="scss" scoped></style>