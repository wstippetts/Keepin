namespace Keepin.Services;

public class KeepsService
{
  private readonly KeepsRepository _repo;
  public KeepsService(KeepsRepository repo)
  {
    _repo = repo;
  }
  internal Keep Create(Keep keepData)
  {
    Keep keep = _repo.Create(keepData);
    return keep;
  }

  internal void RemoveKeep(int id, string userId)
  {
    Keep keep = _repo.GetOneKeep(id);
    if (keep.CreatorId != userId) throw new Exception("You are not the creator of this keep");
    _repo.RemoveKeep(id);
  }

  internal Keep EditKeep(Keep keepData, string userId)
  {
    if (keepData.CreatorId != userId) throw new Exception("You are not the creator of this keep");
    Keep keep = _repo.GetOneKeep(keepData.Id);
    keep.Name = keepData.Name;
    keep.Description = keepData.Description;
    keep.Img = keepData.Img;
    keep.Views = keepData.Views;
    keep.Kept = keepData.Kept;
    keep.CreatorId = keepData.CreatorId;
    _repo.UpdateKeep(keep);
    return keep;
  }

  internal List<Keep> GetKeeps(string id)
  {
    List<Keep> allKeeps = _repo.GetKeeps();
    // List<Keep> filteredKeeps = allKeeps.FindAll(k => k.CreatorId == id);
    return allKeeps;
  }

  internal Keep GetOneKeep(int id, string userId)
  {
    Keep keep = _repo.GetOneKeep(id);
    if (keep == null) throw new Exception($"no keep with id: {id}");
    if (keep.CreatorId != userId) throw new Exception("You are not the creator of this keep");
    {
      if (keep.CreatorId != userId)
        keep.Views++;
      // _repo.UpdateKeep(keep);
    }
    return keep;
  }
}
