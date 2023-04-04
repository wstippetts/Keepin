namespace Keepin.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : Controller
{

  private readonly VaultKeepsService _vaultKeepsService;
  private readonly Auth0Provider _auth;

  public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth)
  {
    _vaultKeepsService = vaultKeepsService;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> CreateVK([FromBody] VaultKeep VKData)
  {
    try
    {
      Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
      VKData.CreatorId = userInfo.Id;
      VaultKeep newVK = _vaultKeepsService.CreateVK(VKData, userInfo);
      return Ok(newVK);

    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> RemoveVK(int id)
  {
    try
    {
      Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
      string message = _vaultKeepsService.RemoveVK(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
