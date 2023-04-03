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

  internal Profile Edit(Profile editData, string userEmail)
  {
    Profile original = GetProfileByEmail(userEmail);
    original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
    return _repo.Edit(original);
  }
}
