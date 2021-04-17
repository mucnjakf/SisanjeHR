using System;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminRepository Admins { get; }
        IOwnerRepository Owners { get; }
        ISubscriptionRepository Subscriptions { get; }
        ICountryRepository Countries { get; }
        ICityRepository Cities { get; }
        ILocationRepository Locations { get; }
        IRegisteredUserRepository RegisteredUsers { get; }
        IWorkerRepository Workers { get; }
        IHairSalonRepository HairSalons { get; }
        IHairSalonMethodsOfPaymentRepository HairSalonMethodsOfPayment { get; }
        IMethodOfPaymentRepository MethodsOfPayment { get; }
        IHairSalonWorkingHoursRepository HairSalonWorkingHours { get; }
        IWorkingHourRepository WorkingHours { get; }
        IHairSalonServicesRepository HairSalonServices { get; }
        IServiceRepository Services { get; }
        IAppointmentRepository Appointments { get; }
        IAppointmentServicesRepository AppointmentServices { get; }
        IReviewRepository Reviews { get; }
        IFavoriteHairSalonRepository FavoriteHairSalons{ get; }

        int Complete();
    }
}
