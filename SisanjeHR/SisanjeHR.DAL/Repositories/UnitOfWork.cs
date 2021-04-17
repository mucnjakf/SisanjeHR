using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Repositories.Interfaces;
using DataAccessLayer.Repositories.ModelRepositories;

namespace DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SisanjeHrDbContext _context;

        public IAdminRepository Admins { get; private set; }
        public IOwnerRepository Owners { get; private set; }
        public ISubscriptionRepository Subscriptions { get; private set; }
        public ICountryRepository Countries { get; private set; }
        public ICityRepository Cities { get; private set; }
        public ILocationRepository Locations { get; private set; }
        public IRegisteredUserRepository RegisteredUsers { get; private set; }
        public IWorkerRepository Workers { get; private set; }
        public IHairSalonRepository HairSalons { get; private set; }
        public IHairSalonMethodsOfPaymentRepository HairSalonMethodsOfPayment { get; set; }
        public IMethodOfPaymentRepository MethodsOfPayment { get; set; }
        public IHairSalonWorkingHoursRepository HairSalonWorkingHours { get; set; }
        public IWorkingHourRepository WorkingHours { get; set; }
        public IHairSalonServicesRepository HairSalonServices { get; set; }
        public IServiceRepository Services { get; set; }
        public IAppointmentRepository Appointments { get; set; }
        public IAppointmentServicesRepository AppointmentServices { get; set; }
        public IReviewRepository Reviews { get; set; }
        public IFavoriteHairSalonRepository FavoriteHairSalons { get; set; }

        public UnitOfWork(SisanjeHrDbContext context)
        {
            _context = context;

            Admins = new AdminRepository(_context);
            Owners = new OwnerRepository(_context);
            Subscriptions = new SubscriptionRepository(_context);
            Countries = new CountryRepository(_context);
            Cities = new CityRepository(_context);
            Locations = new LocationRepository(_context);
            RegisteredUsers = new RegisteredUserRepository(_context);
            Workers = new WorkerRepository(_context);
            HairSalons = new HairSalonRepository(_context);
            HairSalonMethodsOfPayment = new HairSalonMethodsOfPaymentRepository(_context);
            MethodsOfPayment = new MethodOfPaymentRepository(_context);
            HairSalonWorkingHours = new HairSalonWorkingHoursRepository(_context);
            WorkingHours = new WorkingHourRepository(_context);
            HairSalonServices = new HairSalonServicesRepository(_context);
            Services = new ServiceRepository(_context);
            Appointments = new AppointmentRepository(_context);
            AppointmentServices = new AppointmentServicesRepository(_context);
            Reviews = new ReviewRepository(_context);
            FavoriteHairSalons = new FavoriteHairSalonRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
