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
      Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
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
      Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
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
  public ActionResult<List<Keep>> GetKeeps()
  {
    try
    {
      List<Keep> keeps = _keepsService.GetKeeps();
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public ActionResult<Keep> GetOneKeep(int id)
  {
    try
    {
      Keep keep = _keepsService.GetOneKeep(id);
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
      Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
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
