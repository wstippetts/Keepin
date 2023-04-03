namespace Keepin.Repositories;

public class KeepsRepository
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }
  internal Keep Create(Keep keepData)
  {
    string sql = @"
        INSERT INTO keeps
        (name, description, img, creatorId)
        VALUES
        (@name, @description, @img, @creatorId)
        SELECT LAST_INSERT_ID();
        ";
    int id = _db.ExecuteScalar<int>(sql, keepData);
    return keepData;
  }

  internal Keep GetOneKeep(int id)
  {
    string sql = @"
  SELECT
  k.*,
  COUNTV(v.id) AS Views,
  COUNTK(ks.id) AS Kept,
  a.*
  FROM keeps k
  LEFT JOIN views v ON k.id = v.keepId
  LEFT JOIN keeps ks ON k.id = ks.keepId
  JOIN accounts a ON k.creatorId = a.id
  GROUP BY k.id, a.id
  ";
    Keep keep = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }).FirstOrDefault();
    return keep;
  }

  internal List<Keep> GetKeeps()
  {
    string sql = @"
  SELECT
  k.*,
  COUNTV(v.id) AS Views,
  COUNTK(ks.id) AS Kept,
  a.*
  FROM keeps k
  LEFT JOIN views v ON k.id = v.keepId
  LEFT JOIN keeps ks ON k.id = ks.keepId
  JOIN accounts a ON k.creatorId = a.id
  GROUP BY k.id, a.id
  ";
    List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }).ToList();
    return keeps;
  }

}