using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelsProject2024.DAL;
using TravelsProject2024.EN;


namespace TravelsProject2024.DAL
{
    public class TouristPlaceDAL
    {
        public static async Task<int> CreateAsync(TouristPlaces touristPlace)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                dbContext.Add(touristPlace);
                await dbContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> UpdateAsync(TouristPlaces touristPlace)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var placeDb = await dbContext.TouristPlaces.FirstOrDefaultAsync(tp => tp.Id == touristPlace.Id);
                if (placeDb != null)
                {
                    placeDb.Name = touristPlace.Name;
                    placeDb.Description = touristPlace.Description;
                    placeDb.Location = touristPlace.Location;

                    dbContext.Update(placeDb);
                    result = await dbContext.SaveChangesAsync();
                }
                return result;
            }
        }

        public static async Task<int> DeleteAsync(TouristPlaces touristPlace)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var placeDb = await dbContext.TouristPlaces.FirstOrDefaultAsync(tp => tp.Id == touristPlace.Id);
                if (placeDb != null)
                {
                    dbContext.TouristPlaces.Remove(placeDb);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<TouristPlaces> GetByIdAsync(int id)
        {
            var placeDB = new TouristPlaces();
            using (var dbContext = new ContextDB())
            {
                placeDB = await dbContext.TouristPlaces.FirstOrDefaultAsync(tp => tp.Id == id);
            }
            return placeDB;
        }

        public static async Task<List<TouristPlaces>> GetAllAsync()
        {
            var places = new List<TouristPlaces>();
            using (var dbContext = new ContextDB())
            {
                places = await dbContext.TouristPlaces.ToListAsync();
            }
            return places;
        }

        internal static IQueryable<TouristPlaces> QuerySelect(IQueryable<TouristPlaces> query, TouristPlaces place)
        {
            if (place.Id > 0)
                query = query.Where(tp => tp.Id == place.Id);

            if (!string.IsNullOrWhiteSpace(place.Name))
                query = query.Where(tp => tp.Name.Contains(place.Name));

            if (!string.IsNullOrWhiteSpace(place.Description))
                query = query.Where(tp => tp.Description.Contains(place.Description));

            if (!string.IsNullOrWhiteSpace(place.Location))
                query = query.Where(tp => tp.Location.Contains(place.Location));

            query = query.OrderByDescending(tp => tp.Id).AsQueryable();

            // Puedes agregar más criterios de búsqueda según tus necesidades

            return query;
        }

        public static async Task<List<TouristPlaces>> SearchAsync(TouristPlaces place)
        {
            var places = new List<TouristPlaces>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.TouristPlaces.AsQueryable();
                select = QuerySelect(select, place);
                places = await select.ToListAsync();
            }
            return places;
        }

        public static async Task<List<TouristPlaces>> SearchIncludeVehicleAsync(TouristPlaces touristPlace)
        {
            var tourists = new List<TouristPlaces>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.TouristPlaces.AsQueryable();
                tourists = await select.ToListAsync();
            }
            return tourists;
        }
    }
}