namespace Keepin.Controllers;
[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
  private readonly KeepsService _keepsService;
  private readonly Auth0Provider _auth;
  public KeepsController(KeepsService keepsService, Auth0Provider auth)
  {
    _keepsService = keepsService;
    _auth = auth;
  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> RemoveKeep(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      _keepsService.RemoveKeep(id, userInfo.Id);
      return Ok("Successfully Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      newKeep.CreatorId = userInfo.Id;
      Keep created = _keepsService.Create(newKeep);
      created.Creator = userInfo;
      return Ok(created);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public async Task<ActionResult<List<Keep>>> GetKeeps()
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      List<Keep> keeps = _keepsService.GetKeeps(userInfo?.Id);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Keep>> GetOneKeep(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Keep keep = _keepsService.GetOneKeep(id, userInfo?.Id);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<Keep>> EditKeep(int id, [FromBody] Keep keepData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      keepData.CreatorId = userInfo.Id;
      keepData.Id = id;
      Keep keep = _keepsService.EditKeep(keepData, userInfo.Id);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


}
