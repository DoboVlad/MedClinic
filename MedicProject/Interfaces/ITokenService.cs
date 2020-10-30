using MedicProject.Models;

namespace MedicProject.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}