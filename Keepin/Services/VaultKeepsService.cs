namespace Keepin.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repo;
  private readonly VaultsService _vaultService;

  public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultService)
  {
    _repo = repo;
    _vaultService = vaultService;
  }

  internal VaultKeep CreateVK(VaultKeep vKData, Profile userInfo)
  {
    Vault vault = _vaultService.GetById(vKData.VaultId, userInfo.Id);
    if (vault.CreatorId != userInfo.Id) throw new Exception("You are not the creator of this vault");
    return _repo.CreateVK(vKData);
  }

  internal string RemoveVK(int id, string userId)
  {
    VaultKeep found = _repo.GetById(id);
    if (found.CreatorId != userId) throw new Exception("You are not the creator of this vault");
    _repo.RemoveVK(id, found);
    return $"Successfully deleted: {found.Id}";
  }
}
