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
    public class TouristPlaceImageDAL
    {
        public static async Task<int> CreateAsync(TouristPlaceImage touristImage)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                dbContext.Add(touristImage);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> UpdateAsync(TouristPlaceImage touristImage)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var touristImageDB = await dbContext.TouristPlacesImage.FirstOrDefaultAsync(s => s.Id == touristImage.Id);
                if (touristImageDB != null)
                {
                    touristImageDB.IdTouristPlaces = touristImage.IdTouristPlaces;
                    touristImageDB.Path = touristImage.Path;
                    dbContext.Update(touristImageDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<int> DeleteAsync(TouristPlaceImage touristImage)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var touristImageDB = await dbContext.TouristPlacesImage.FirstOrDefaultAsync(s => s.Id == touristImage.Id);
                if (touristImageDB != null)
                {
                    dbContext.TouristPlacesImage.Remove(touristImageDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<TouristPlaceImage> GetByIdAsync(TouristPlaceImage tourisImage)
        {
            var touristImageDB = new TouristPlaceImage();
            using (var dbContext = new ContextDB())
            {
                touristImageDB = await dbContext.TouristPlacesImage.FirstOrDefaultAsync(s => s.Id == tourisImage.Id);
            }
            return touristImageDB;
        }

        public static async Task<List<TouristPlaceImage>> GetAllAsync()
        {
            var images = new List<TouristPlaceImage>();
            using (var dbContext = new ContextDB())
            {
                images = await dbContext.TouristPlacesImage.ToListAsync();
            }
            return images;
        }

        internal static IQueryable<TouristPlaceImage> QuerySelect(IQueryable<TouristPlaceImage> query, TouristPlaceImage touristImage)
        {
            if (touristImage.Id > 0)
                query = query.Where(s => s.Id == touristImage.Id);
            if (touristImage.IdTouristPlaces > 0)
                query = query.Where(s => s.IdTouristPlaces == touristImage.IdTouristPlaces);
            if (!string.IsNullOrWhiteSpace(touristImage.Path))
                query = query.Where(s => s.Path.Contains(touristImage.Path));

            query = query.OrderByDescending(s => s.Id).AsQueryable();

            if (touristImage.Top_Aux > 0)
                query = query.Take(touristImage.Top_Aux).AsQueryable();
            return query;
        }

        public static async Task<List<TouristPlaceImage>> SearchAsync(TouristPlaceImage touristImage)
        {
            var images = new List<TouristPlaceImage>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.TouristPlacesImage.AsQueryable();
                select = QuerySelect(select, touristImage);
                images = await select.ToListAsync();
            }
            return images;
        }

        public static async Task<List<TouristPlaceImage>> SearchIncludeAdAsync(TouristPlaceImage touristImage)
        {
            var images = new List<TouristPlaceImage>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.TouristPlacesImage.AsQueryable();
                select = QuerySelect(select, touristImage).Include(s => s.TouristPlaces).AsQueryable();
                images = await select.ToListAsync();
            }
            return images;
        }
    }
}
