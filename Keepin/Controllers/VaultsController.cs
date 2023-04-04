namespace Keepin.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth;


  public VaultsController(VaultsService vaultsService, Auth0Provider auth)
  {
    _vaultsService = vaultsService;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
  {
    try
    {
      Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
      newVault.CreatorId = userInfo.Id;
      Vault created = _vaultsService.CreateVault(newVault);
      created.Creator = userInfo;
      return Ok(created);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Vault>> GetById(int id)
  {
    try
    {
      Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
      Vault found = _vaultsService.GetById(id, userInfo?.Id);
      return Ok(found);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> RemoveVault(int id)
  {
    try
    {
      Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
      string message = _vaultsService.RemoveVault(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<Vault>> Edit([FromBody] Vault editVault, int id)
  {
    try
    {
      Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
      editVault.Id = id;
      editVault.CreatorId = userInfo.Id;
      Vault edited = _vaultsService.Edit(editVault);
      return Ok(edited);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // SECTION VK area...............................................

  [HttpGet("{vaultId}/keeps")]
  public async Task<ActionResult<List<VKeep>>> GetKeepsByVaultId(int vaultId)
  {
    try
    {
      Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
      List<VKeep> keeps = _vaultsService.GetKeepsByVaultId(vaultId, userInfo);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
