import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "../AppState.js"

class VaultsService {


  async getVaultsByProfileId(userId) {
    AppState.vaults = null
    const res = await api.get('api/profiles/' + userId + '/vaults')
    logger.log('[I got da VAULTS- vaults service get vaults]', res.data)
    AppState.vaults = res.data
  }

  async getVaultsById(vaultId) {
    const res = await api.get('api/vaults/' + vaultId)
    logger.log('[I got da VAULTS- vaults service get vaults]', res.data)
    AppState.vaults = res.data
  }
}
export const vaultsService = new VaultsService()