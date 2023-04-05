import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {
  async getKeeps() {
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

}
export const keepsService = new KeepsService()