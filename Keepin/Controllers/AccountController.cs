namespace Keepin.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultsService _vaultsService;


  public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vaultsService, IDbConnection db)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _vaultsService = vaultsService;
  }

  [HttpGet]
  [Authorize]



  public async Task<ActionResult<Profile>> Get()
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Profile>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaults")]
  [Authorize]
  public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId()
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Profile>(HttpContext);
      List<Vault> vaults = _vaultsService.GetVaultsByProfileId(userInfo);
      return Ok(vaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut]
  [Authorize]
  public async Task<ActionResult<Profile>> Edit([FromBody] Profile editData)
  {
    try
    {
      Profile original = await _auth0Provider.GetUserInfoAsync<Profile>(HttpContext);
      Profile newAccount = _accountService.Edit(editData, original);
      return Ok(newAccount);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // TODO write method for editing account; make sure to pass the correct arguments here.... refer to the editAccount in the service

}
