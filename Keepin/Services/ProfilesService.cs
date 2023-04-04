namespace Keepin.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _repo;

  public ProfilesService(ProfilesRepository repo)
  {
    _repo = repo;
  }

  internal List<Keep> GetKeepsByProfileId(string id)
  {
    List<Keep> keeps = _repo.GetKeepsByProfileId(id);
    if (keeps == null) throw new Exception("Invalid Id");

    return keeps;
  }

  internal Profile GetProfileById(string id)
  {
    Profile profile = _repo.GetProfileById(id);
    if (profile == null)
    {
      throw new Exception("Invalid Id");
    }
    return profile;
  }

  internal List<Vault> GetVaultsByProfileId(string id, Profile userInfo)
  {
    List<Vault> vaults = _repo.GetVaultsByProfileId(id);
    if (vaults == null) throw new Exception("Invalid Id");
    List<Vault> vaultsToReturn = vaults.FindAll(v => v.IsPrivate == false || v.CreatorId == userInfo?.Id);
    return vaultsToReturn;
  }
}
