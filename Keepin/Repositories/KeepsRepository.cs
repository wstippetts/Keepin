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
        (name, description, img, creatorId, kept, views)
        VALUES
        (@name, @description, @img, @creatorId, 0, 0);
        SELECT LAST_INSERT_ID();
        ";
    int id = _db.ExecuteScalar<int>(sql, keepData);
    keepData.Id = id;
    return keepData;
  }

  internal Keep GetOneKeep(int id)
  {
    string sql = @"
  SELECT
  k.*,
  a.*
  FROM keeps k
  JOIN accounts a ON k.creatorId = a.id
  WHERE k.id = @id;
  ";
    Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      keep.CreatorId = account.Id;
      return keep;
    }, new { id }).FirstOrDefault();
    if (keep == null) throw new Exception("Invalid Id");
    string sql2 = @"
    UPDATE keeps
    SET
    views = views + 1
    WHERE id = @id;
    ";
    _db.Execute(sql2, new { id });
    keep.Views++;
    return keep;
  }

  internal List<Keep> GetKeeps()
  {
    string sql = @"
  SELECT
  k.*,
  a.*
  FROM keeps k
  JOIN accounts a ON k.creatorId = a.id;
  ";
    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      keep.CreatorId = account.Id;
      return keep;
    }).ToList();
    return keeps;
  }

  internal int UpdateKeep(Keep keepData)
  {
    string sql = @"
    UPDATE keeps
    SET
    name = @Name,
    description = @Description,
    img = @Img
    WHERE id = @Id;
    ";
    int rows = _db.Execute(sql, keepData);
    return rows;
  }

  internal int RemoveKeep(int id)
  {
    string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";
    int rows = _db.Execute(sql, new { id });
    return rows;
  }
}