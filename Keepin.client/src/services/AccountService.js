import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async editAccount(acc) {
    const res = await api.put('account', acc)
    logger.log(res, 'this is the res', acc, 'this is the account passed to the edit account in account service');
    AppState.account = new Account(res.data)
  }

  // TODO write your edit account method....form is hooked up now lol

}

export const accountService = new AccountService()
