namespace Keepin.Repositories;

public class ProfilesRepository
{
  private readonly IDbConnection _db;
  public ProfilesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal List<Keep> GetKeepsByProfileId(string id)
  {
    string sql = @"
    SELECT
    keeps.*,
    acct.*
    FROM keeps 
    JOIN accounts acct ON keeps.creatorId = acct.id
    WHERE keeps.creatorId = @id";
    return _db.Query<Keep, Profile, Keep>(sql, (keeps, prof) =>
    {
      keeps.Creator = prof;
      return keeps;
    }, new { id }, splitOn: "id").ToList();
  }

  internal Profile GetProfileById(string id)
  {
    string sql = @"
    SELECT
    *
    FROM
    accounts
    WHERE id = @id";
    return _db.QueryFirstOrDefault<Profile>(sql, new { id });
  }

  internal List<Vault> GetVaultsByProfileId(string id)
  {
    string sql = @"
    SELECT
    vaults.*,
    acct.*
    FROM vaults
    JOIN accounts acct ON vaults.creatorId = acct.id
    WHERE vaults.creatorId = @id";
    return _db.Query<Vault, Profile, Vault>(sql, (vaults, prof) =>
    {
      vaults.Creator = prof;
      return vaults;
    }, new { id }).ToList();
  }
}
