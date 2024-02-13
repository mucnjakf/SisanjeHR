using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IRegisteredUserRepository : IRepository<RegisteredUser>
    {
        IEnumerable<RegisteredUser> GetRegisteredUsersWithLocations();
        RegisteredUser GetRegisteredUserWithLocation(int id);
        bool Exists(string username, string email);
        RegisteredUser GetRegisteredUserWithLocation(string registeredUserUsername, string registeredUserEmail);
        RegisteredUser GetRegisteredUser(string username, string passwordHash);
    }
}
