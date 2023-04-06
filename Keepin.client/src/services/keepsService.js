import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"

class KeepsService {
  async getKeeps() {
    AppState.keeps = null
    const res = await api.get('api/keeps')
    logger.log('[I got da KEEPS- keep service get keeps]', res.data)
    AppState.keeps = res.data
  }

  async getKeepsByProfileId(userId) {
    AppState.keeps = null
    const res = await api.get('api/keeps')
    const keeps = res.data.filter(k => k.creatorId === userId)
    logger.log('[I got da KEEPS- keep service get keeps by profile id]', res.data)
    AppState.keeps = keeps
  }

  async removeKeep(keep) {
    if (keep.creatorId != AppState.account.id) {
      Pop.error('Could not remove that keep')
    }
    const res = await api.delete(`api/keeps/${keep.id}`)
    logger.log(`[keep removed from database]`, res.data)
    AppState.keeps = AppState.keeps.filter(k => k.id != keep.id)
  }

  async setActive(keep) {
    AppState.activeKeep = null
    const res = await api.get(`api/keeps/${keep.id}`)
    AppState.activeKeep = res.data
  }



}
export const keepsService = new KeepsService()