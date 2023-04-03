namespace Keepin.Models;

public class Profile : Account
{

  public string Id { get; set; }
  public string Name { get; set; }
  public string Picture { get; set; }
}

public class Account
{
  public string Email { get; set; }
}

