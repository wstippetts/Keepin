<template>
  <div class="modal modal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-fullscreen-sm-down">
      <div class="modal-content">

        <div class="modal-body">
          <slot> slot goodies not found </slot>
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


<style lang="scss" scoped>
.modal {
  --bs-modal-width: 80vw;
}
</style>