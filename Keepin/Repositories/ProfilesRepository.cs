namespace Keepin.Repositories;

public class ProfilesRepository
{
  private readonly IDbConnection _db;
  public ProfilesRepository(IDbConnection db)
  {
    _db = db;
  }
}
