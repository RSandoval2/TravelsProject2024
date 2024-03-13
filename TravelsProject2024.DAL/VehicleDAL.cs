using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelsProject2024.EN;

namespace TravelsProject2024.DAL
    {
        public class VehicleDAL
        {
            public static async Task<int> CreateAsync(Vehicle vehicle)
            {
                int result = 0;
                using (var dbContext = new ContextDB())
                {
                    dbContext.Add(vehicle);
                    result = await dbContext.SaveChangesAsync();
                }
                return result;
            }

            public static async Task<int> UpdateAsync(Vehicle vehicle)
            {
                int result = 0;
                using (var dbContext = new ContextDB())
                {
                    var vehicleDb = await dbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == vehicle.Id);
                if (vehicleDb != null)
                {
                    vehicleDb.Type = vehicle.Type;
                    vehicleDb.Capacity = vehicle.Capacity;
                    vehicleDb.Brand = vehicle.Brand;
                    vehicleDb.Model = vehicle.Model;
                    vehicleDb.Year = vehicle.Year;
                        dbContext.Update(vehicleDb);
                        result = await dbContext.SaveChangesAsync();
                    }
                }
                return result;

            }
            public static async Task<int> DeleteAsync(Vehicle vehicle)
            {
                int result = 0;
                using (var dbContext = new ContextDB())
                {
                    var vehicleDB = await dbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == vehicle.Id);
                    if (vehicleDB != null)
                    {
                        dbContext.Vehicles.Remove(vehicleDB);
                        result = await dbContext.SaveChangesAsync();

                    }
                }
                return result;
            }

            public static async Task<Vehicle> GetByIdAsync(Vehicle vehicle)
            {
                var vehicleDB = new Vehicle();
                using (var dbContext = new ContextDB())
                {
                    vehicleDB = await dbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == vehicle.Id);
                }
                return vehicleDB;
            }

            public static async Task<List<Vehicle>> GetAllAsync()
            {
                var vehicles = new List<Vehicle>();
                using (var dbContext = new ContextDB())
                {
                    vehicles = await dbContext.Vehicles.ToListAsync();
                }
                return vehicles;
            }

            internal static IQueryable<Vehicle> QuerySelect(IQueryable<Vehicle> query, Vehicle vehicle)
            {
                if (vehicle.Id > 0)
                    query = query.Where(v => v.Id == vehicle.Id);

                if (!string.IsNullOrWhiteSpace(vehicle.Type))
                    query = query.Where(v => v.Type.Contains(vehicle.Type));

                if (vehicle.Capacity > 0)
                query = query.Where(v => v.Capacity == vehicle.Capacity);

            if (!string.IsNullOrWhiteSpace(vehicle.Brand))
                query = query.Where(v => v.Brand.Contains(vehicle.Brand));

            if (!string.IsNullOrWhiteSpace(vehicle.Model))
                query = query.Where(v => v.Model.Contains(vehicle.Model));

            if (vehicle.Year > 0)
                query = query.Where(v => v.Year == vehicle.Year);

            query = query.OrderByDescending(v => vehicle.Id).AsQueryable();

                return query;
            }

            public static async Task<List<Vehicle>> SearchAsync(Vehicle vehicle)
            {
                var vehicles = new List<Vehicle>();
                using (var dbContext = new ContextDB())
                {
                    var select = dbContext.Vehicles.AsQueryable();
                    select = QuerySelect(select, vehicle);
                    vehicles = await select.ToListAsync();
                }
                return vehicles;
            }
        }
    }