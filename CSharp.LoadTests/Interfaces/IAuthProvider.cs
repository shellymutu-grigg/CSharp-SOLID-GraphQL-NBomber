public interface IAuthProvider
{
    Task<string?> GetAuthToken();
}