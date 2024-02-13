using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace SisanjeHrApi.Controllers
{
    public class FavoriteHairSalonController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetFavoriteHairSalonsByRegisteredUser/{registeredUserId}")]
        public IEnumerable<FavoriteHairSalons> GetFavoriteHairSalonsByRegisteredUser(int registeredUserId)
        {
            return unitOfWork.FavoriteHairSalons.GetFavoriteHairSalonsByRegisteredUser(registeredUserId);
        }
    }
}
