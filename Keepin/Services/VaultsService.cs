namespace Keepin.Services;

public class VaultsService
{
  private readonly VaultsRepository _repo;

  public VaultsService(VaultsRepository repo)
  {
    _repo = repo;
  }

  internal Vault CreateVault(Vault newVault)
  {
    Vault created = _repo.CreateVault(newVault);
    return created;
  }

  internal Vault Edit(Vault editVault)
  {
    Vault found = _repo.GetById(editVault.Id);
    if (found == null) throw new Exception("Invalid Id");
    if (found.CreatorId != editVault.CreatorId) throw new Exception("You are not the creator of this vault");
    found.Name = editVault.Name != null ? editVault.Name : found.Name;
    found.Description = editVault.Description != null ? editVault.Description : found.Description;
    found.Img = editVault.Img != null ? editVault.Img : found.Img;
    found.IsPrivate = editVault.IsPrivate;
    return _repo.Edit(found);
  }

  internal Vault GetById(int id, string userId)
  {
    Vault found = _repo.GetById(id);
    if (found == null)
    {
      throw new Exception("Invalid Id");
    }
    if (found.IsPrivate && found.CreatorId != userId)
    {
      throw new Exception("This is a private vault");
    }
    return found;
  }

  internal List<VKeep> GetKeepsByVaultId(int vaultId, Profile userInfo)
  {
    this.GetById(vaultId, userInfo?.Id);
    return _repo.GetKeepsByVaultId(vaultId);
  }

  internal List<Vault> GetVaultsByProfileId(Profile userInfo)
  {
    List<Vault> vaults = _repo.GetVaultsByProfileId(userInfo?.Id);
    if (vaults == null) throw new Exception("Invalid Id");
    List<Vault> vaultsToReturn = vaults.FindAll(v => v.IsPrivate == false || v.CreatorId == userInfo?.Id);
    return vaultsToReturn;
  }

  internal string RemoveVault(int id, string userId)
  {
    Vault found = this.GetById(id, userId);
    if (found.CreatorId != userId)
    {
      throw new Exception("You are not the creator of this vault");
    }
    _repo.RemoveVault(id);
    return $"Successfully deleted: {found.Id}";
  }
}
