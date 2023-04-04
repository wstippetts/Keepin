<template>
  <div class="component keepCard">
    <img class="img-fluid rounded" :src="keep.img" alt="">
    <div class="d-flex justify-content-between">
      <h4 class="p-2 text-dark text-start textBox">{{ keep.name }}</h4>
      <img class="profPic m-1" :src="keep.creator.picture" alt="">
    </div>
  </div>
</template>


<script>
export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {}
  }
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

