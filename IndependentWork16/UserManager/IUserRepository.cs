public interface IUserRepository
{
    void SaveUser(string username, string password, string email);
}