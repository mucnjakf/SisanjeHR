using DataAccessLayer.Models;
using System.Data.Entity;

namespace DataAccessLayer.DatabaseContext
{
    public class SisanjeHrDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }

        public DbSet<HairSalon> HairSalons { get; set; }
        public DbSet<HairSalonServices> HairSalonServices { get; set; }
        public DbSet<HairSalonMethodsOfPayment> HairSalonMethodsOfPayment { get; set; }
        public DbSet<HairSalonWorkingHours> HairSalonWorkingHours { get; set; }

        public DbSet<Service> Services { get; set; }
        public DbSet<MethodOfPayment> MethodsOfPayment { get; set; }
        public DbSet<WorkingHour> WorkingHours { get; set; }

        public DbSet<Location> Locations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        
        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentServices> AppointmentServices { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<FavoriteHairSalons> FavoriteHairSalons { get; set; }
    }
}
