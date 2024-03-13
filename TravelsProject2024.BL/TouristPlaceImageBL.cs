using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelsProject2024.DAL;
using TravelsProject2024.EN;

namespace TravelsProject2024.BL
{
    public class TouristPlaceImageBL
    {

        public async Task<int> CreateAsync(TouristPlaceImage touristImage)
        {
            return await TouristPlaceImageDAL.CreateAsync(touristImage);
        }

        public async Task<int> UpdateAsync(TouristPlaceImage touristImage)
        {
            return await TouristPlaceImageDAL.UpdateAsync(touristImage);
        }

        public  async Task<int> DeleteAsync(TouristPlaceImage touristImage)
        {
            return await TouristPlaceImageDAL.DeleteAsync(touristImage);
        }

        public async Task<TouristPlaceImage> GetByIdAsync(TouristPlaceImage tourisImage)
        {
            return await TouristPlaceImageDAL.GetByIdAsync(tourisImage);
        }

        public async Task<List<TouristPlaceImage>> GetAllAsync()
        {
            return await TouristPlaceImageDAL.GetAllAsync();
        }

        public async Task<List<TouristPlaceImage>> SearchAsync(TouristPlaceImage touristImage)
        {
            return await TouristPlaceImageDAL.SearchAsync(touristImage);
        }

        public async Task<List<TouristPlaceImage>> SearchIncludeAdAsync(TouristPlaceImage touristImage)
        {
            return await TouristPlaceImageDAL.SearchIncludeAdAsync(touristImage);
        }
    }
}
