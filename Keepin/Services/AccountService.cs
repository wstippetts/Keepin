namespace Keepin.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  internal Profile GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Profile GetOrCreateProfile(Profile userInfo)
  {
    Profile profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  internal Profile Edit(Profile editData, Profile original)
  {
    original.Name = editData.Name != null ? editData.Name : original.Name;
    original.Picture = editData.Picture != null ? editData.Picture : original.Picture;

    _repo.Edit(original);
    return original;
  }
}
