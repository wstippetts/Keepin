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

  internal int RemoveVault(int id)
  {
    string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";
    int rows = _db.Execute(sql, new { id });
    return rows;

  }

}
