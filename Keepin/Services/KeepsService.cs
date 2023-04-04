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
    Keep keep = _repo.GetOneKeep(keepData.Id);
    if (keep.CreatorId != userId) throw new Exception("You are not the creator of this keep");
    keep.Name = keepData.Name != null ? keepData.Name : keep.Name;
    keep.Description = keepData.Description != null ? keepData.Description : keep.Description;
    keep.Img = keepData.Img != null ? keepData.Img : keep.Img;
    _repo.UpdateKeep(keep);
    return keep;
  }

  internal List<Keep> GetKeeps()
  {
    List<Keep> allKeeps = _repo.GetKeeps();
    // List<Keep> filteredKeeps = allKeeps.FindAll(k => k.CreatorId == id);
    return allKeeps;
  }

  internal Keep GetOneKeep(int id)
  {
    Keep keep = _repo.GetOneKeep(id);

    return keep;
  }
}
