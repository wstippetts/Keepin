<template>
  <div class="component">
    <div class="modal fade" tabindex="-1" data-bs-keyboard="false" role="dialog" aria-labelledby="modalTitleId"
      aria-hidden="true">
      <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-xl" role="document">
        <div class="modal-content">
          <slot>Slot goodies did not load</slot>
        </div>
      </div>
    </div>

  </div>
</template>


<script>
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped></style>