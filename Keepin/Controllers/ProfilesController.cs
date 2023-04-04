namespace Keepin.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _profilesService;
  private readonly Auth0Provider _auth;

  public ProfilesController(ProfilesService profilesService, Auth0Provider auth)
  {
    _profilesService = profilesService;
    _auth = auth;
  }

  [HttpGet("{id}")]
  public ActionResult<Profile> Get(string id)
  {
    try
    {
      Profile profile = _profilesService.GetProfileById(id);
      return Ok(profile);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }

  }
  [HttpGet("{id}/keeps")]
  public ActionResult<List<Keep>> GetKeepsByProfileId(string id)
  {
    try
    {
      List<Keep> keeps = _profilesService.GetKeepsByProfileId(id);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{id}/vaults")]
  public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(string id)
  {
    try
    {
      Profile profile = await _auth.GetUserInfoAsync<Profile>(HttpContext);
      List<Vault> vaults = _profilesService.GetVaultsByProfileId(id, profile);
      return Ok(vaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
