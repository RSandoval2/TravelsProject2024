using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelsProject2024.DAL;
using TravelsProject2024.EN;

namespace TravelsProject2024.BL
{
    public class VehicleBL
    {
        public async Task<int> CreateAsync(Vehicle vehicle)
        {
            return await VehicleDAL.CreateAsync(vehicle);
        }

        public async Task<int> UpdateAsync(Vehicle vehicle)
        {
            return await VehicleDAL.UpdateAsync(vehicle);
        }

        public async Task<int> DeleteAsync(Vehicle vehicle)
        {
            return await VehicleDAL.DeleteAsync(vehicle);
        }

        public async Task<Vehicle> GetByIdAsync(Vehicle vehicle)
        {
            return await VehicleDAL.GetByIdAsync(vehicle);
        }

        public async Task<List<Vehicle>> GetAllAsync()
        {
            return await VehicleDAL.GetAllAsync();
        }

        public async Task<List<Vehicle>> SearchAsync(Vehicle vehicle)
        {
            return await VehicleDAL.SearchAsync(vehicle);
        }


    }
}
