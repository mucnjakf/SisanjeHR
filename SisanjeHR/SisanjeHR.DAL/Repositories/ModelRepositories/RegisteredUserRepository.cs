using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories.ModelRepositories
{
    public class RegisteredUserRepository : Repository<RegisteredUser>, IRegisteredUserRepository
    {
        public SisanjeHrDbContext SisanjeHrDbContext { get { return Context as SisanjeHrDbContext; } }

        public RegisteredUserRepository(SisanjeHrDbContext context) : base(context)
        {
        }

        public IEnumerable<RegisteredUser> GetRegisteredUsersWithLocations()
        {
            return SisanjeHrDbContext.RegisteredUsers
                .Include("Location.City.Country")
                .ToList();
        }

        public RegisteredUser GetRegisteredUserWithLocation(int id)
        {
            return SisanjeHrDbContext.RegisteredUsers.Include("Location.City.Country").Single(ru => ru.Id == id);
        }

        public bool Exists(string username, string email)
        {
            return SisanjeHrDbContext.RegisteredUsers.Include("Location.City.Country").Any(ru => ru.Username == username && ru.Email == email);
        }

        public RegisteredUser GetRegisteredUserWithLocation(string registeredUserUsername, string registeredUserEmail)
        {
            return SisanjeHrDbContext.RegisteredUsers.Include("Location.City.Country").Single(ru => ru.Username == registeredUserUsername && ru.Email == registeredUserEmail);
        }

        public RegisteredUser GetRegisteredUser(string username, string passwordHash)
        {
            return SisanjeHrDbContext.RegisteredUsers.Include("Location.City.Country").Single(ru => ru.Username == username && ru.PasswordHash == passwordHash);
        }
    }
}
