namespace Keepin.Repositories;

public class VaultKeepsRepository
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal VaultKeep CreateVK(VaultKeep vKData)
  {
    string sql = @"
    INSERT INTO vaultkeeps
    (creatorId, vaultId, keepId)
    VALUES
    (@CreatorId, @VaultId, @KeepId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, vKData);
    vKData.Id = id;
    int keepId = vKData.KeepId;
    string sql2 = @"
    UPDATE keeps
    SET
    kept = kept + 1
    WHERE keeps.id = @keepId;";
    _db.Execute(sql2, new { keepId });
    return vKData;
  }

  internal VaultKeep GetById(int id)
  {
    string sql = @"
    SELECT
    *
    FROM
    vaultkeeps
    WHERE id = @id;
    ";
    return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
  }



  internal void RemoveVK(int id, VaultKeep found)
  {
    int VKId = found.Id;
    int keepId = found.KeepId;
    string sql = @"
    DELETE
    FROM
    vaultkeeps
    WHERE id = @VKId;
    ";
    _db.Execute(sql, new { VKId });
    string sql2 = @"
    UPDATE keeps
    SET
    kept = kept - 1
    WHERE keeps.id = @keepId;";
    _db.Execute(sql2, new { keepId });

  }
}
