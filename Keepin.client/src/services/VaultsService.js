import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "../AppState.js"

class VaultsService {


  async getVaultsByProfileId(userId) {
    AppState.vaults = null
    const res = await api.get('api/profiles/' + userId + '/vaults')
    logger.log('[I got da VAULTS- vaults service get vaults]', res.data)
    res.data.filter(v => v.creatorId == userId || v.isPrivate == false)
    AppState.vaults = res.data
  }

  async getVaultsById(vaultId) {
    const res = await api.get('api/vaults/' + vaultId)
    logger.log('[I got da VAULTS- vaults service get vaults]', res.data)
    AppState.activeVault = res.data
  }
  async getKeepsByVaultId(vaultId) {
    AppState.keeps = null
    const res = await api.get('api/vaults/' + vaultId + '/keeps')
    logger.log('[I got da KEEPS- vault service get keeps by vault id]', res.data)
    AppState.keeps = res.data
  }

  async removeVaultKeep(vaultKeepId, vaultId) {
    const res = await api.delete('api/vaultkeeps/' + vaultKeepId)
    logger.log(`[vaultkeep removed from database]`, res.data)
    getKeepsByVaultId(vaultId)
  }

  async getProfileData(profileId) {
    AppState.activeProfile = null
    const res = await api.get('api/profiles/' + profileId)
    logger.log('[I got da Profile from vaults service get profile data]', res.data)
    AppState.activeProfile = res.data
  }

  async removeVault(vaultId) {
    const res = await api.delete('api/vaults/' + vaultId)
    logger.log('[vault removed from database]', res.data)
    getVaultsByProfileId(AppState.account.id)
  }

  // TODO go get account vaults and save into its own collection

}
export const vaultsService = new VaultsService()