using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelsProject2024.EN;
using static System.Net.Mime.MediaTypeNames;

namespace TravelsProject2024.DAL
{
    public class ContextDB : DbContext

    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TouristPlaces> TouristPlaces { get; set; }
        public DbSet<TouristPlaceImage> TouristPlacesImage { get; set; }

        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data source = TravelsProject.mssql.somee.com; Initial catalog = TravelsProject;
                           user id=Sandoval22_SQLLogin_1;pwd=Sandoval22; Encrypt = false;
                           TrustServerCertificate = True");
        }
    }
}
