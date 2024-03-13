using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelsProject2024.DAL;
using TravelsProject2024.EN;

namespace TravelsProject2024.BL
{
    public class TouristPlaceBL
    {
        public async Task<int> CreateAsync(TouristPlaces touristPlace)
        {
            return await TouristPlaceDAL.CreateAsync(touristPlace);
        }

        public async Task<int> UpdateAsync(TouristPlaces touristPlace)
        {
            return await TouristPlaceDAL.UpdateAsync(touristPlace);
        }

        public async Task<int> DeleteAsync(TouristPlaces touristPlace)
        {
            return await TouristPlaceDAL.DeleteAsync(touristPlace);
        }

        public  async Task<TouristPlaces> GetByIdAsync(int id)
        {
            return await TouristPlaceDAL.GetByIdAsync(id);
        }

        public static async Task<List<TouristPlaces>> GetAllAsync()
        {
            return await TouristPlaceDAL.GetAllAsync();
        }

        public  async Task<List<TouristPlaces>> SearchAsync(TouristPlaces place)
        {
            return await TouristPlaceDAL.SearchAsync(place);
        }

   
      
    }
}
