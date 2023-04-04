import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {
  async getKeeps() {
    const res = await api.get('api/keeps')
    logger.log('[I got da KEEPS- keep service get keeps]', res.data)
    AppState.keeps = res.data
  }

}
export const keepsService = new KeepsService()