<template>
  <span>
    <!-- <div class="btn-group dropright">
      <button type="button" class="btn btn-secondary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true"
        aria-expanded="false">
        Create
      </button>
      <div class="dropdown-menu">
        <a class="dropdown-item" href="#">New Keep</a>
        <a class="dropdown-item" href="#">New Vault</a>
      </div>
    </div> -->


    <!-- TODO make this login button a create keep and vault button -->
    <!-- <button class="btn selectable text-success lighten-30 text-uppercase my-2 my-lg-0" @click="login">
      Login
    </button>
    <div class="dropdown dropright my-2 my-lg-0">
      <div type="button" class="bg-dark border-0 selectable no-select" data-bs-toggle="dropdown" aria-expanded="false">
        <img :src="account.picture || user.picture" alt="account photo" height="40" class="rounded" />
      </div>
      <div class="dropdown-menu dropdown-menu-lg-left p-0" aria-labelledby="authDropdown">
        <div class="list-group">
          <router-link :to="{ name: 'Account' }">
            <div class="list-group-item dropdown-item list-group-item-action">
              Manage Account
            </div>
          </router-link>
          <div class="list-group-item dropdown-item list-group-item-action text-primary selectable" @click="editAccount">
            <i class="mdi mdi-recycle"></i>
            Edit Account
          </div>
          <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
            <i class="mdi mdi-logout"></i>
            logout
          </div>

        </div>
      </div>
    </div> -->
  </span>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      },

    }
  }
}
</script>

<style lang="scss" scoped></style>
