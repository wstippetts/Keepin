namespace Keepin.Repositories;

public class VaultsRepository
{
  private readonly IDbConnection _db;
  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  public Vault CreateVault(Vault newVault)
  {
    string sql = @"
  INSERT INTO vaults
  (creatorId, name, description, img, isPrivate)
  VALUES
  (@CreatorId, @Name, @Description, @Img, @IsPrivate);
  SELECT LAST_INSERT_ID();";
    int id = _db.ExecuteScalar<int>(sql, newVault);
    newVault.Id = id;
    return newVault;
  }

  public Vault GetById(int id)
  {
    string sql = @"
  SELECT
  v.*,
  a.*
  FROM vaults v
  JOIN accounts a ON v.creatorId = a.id
  WHERE v.id = @id;";

    return _db.Query<Vault, Profile, Vault>(sql, (vault, account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { id }).FirstOrDefault();
  }

  internal Vault Edit(Vault found)
  {
    string sql = @"
  UPDATE vaults
  SET
  name = @Name,
  description = @Description,
  img = @Img,
  isPrivate = @IsPrivate
  WHERE id = @Id;
  SELECT * FROM vaults WHERE id = @Id;";
    return _db.QueryFirstOrDefault<Vault>(sql, found);
  }

  internal List<VKeep> GetKeepsByVaultId(int vaultId)
  {
    string sql = @"
    SELECT
    k.*,
    vk.id as vaultKeepId,
    a.*
    FROM vaultkeeps vk
    JOIN keeps k ON vk.keepId = k.id
    JOIN accounts a ON vk.creatorId = a.id
    WHERE vaultId = @vaultId;";
    return _db.Query<VKeep, Profile, VKeep>(sql, (vkeep, account) =>
    {
      vkeep.Creator = account;
      vkeep.CreatorId = account.Id;
      return vkeep;
    }, new { vaultId }).ToList();
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

  internal int RemoveVault(int id)
  {
    string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";
    int rows = _db.Execute(sql, new { id });
    return rows;

  }

}
