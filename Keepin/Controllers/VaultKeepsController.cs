namespace Keepin.Controllers;

[Route("[controller]")]
public class VaultKeepsController : Controller
{

  private readonly VaultKeepsService _vs;
  private readonly Auth0Provider _auth;

  public VaultKeepsController(VaultKeepsService vs, Auth0Provider auth)
  {
    _vs = vs;
    _auth = auth;
  }

}
