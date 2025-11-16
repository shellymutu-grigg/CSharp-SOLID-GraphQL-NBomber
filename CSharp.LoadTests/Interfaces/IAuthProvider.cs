using System.Threading.Tasks;

namespace CSharp.LoadTests.Interfaces;

public interface IAuthProvider
{
    Task<string> GetTokenAsync();
}