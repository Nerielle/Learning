using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;

namespace theworld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private readonly WorldContext worldContext;
        private ILogger<WorldRepository> logger; 

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            worldContext = context;
            this.logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return worldContext
                    .Trips
                    .OrderBy(x => x.Name)
                    .ToList();
            }
            catch (Exception e)
            {
                logger.LogError("Could not get trips from database", e);
                throw;
            }
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return worldContext.Trips
                    .Include(x => x.Stops)
                    .OrderBy(x => x.Name)
                    .ToList();
            }
            catch (Exception e)
            {
                logger.LogError("Could not get trips with stops from database", e);
                throw;
            }
        }

        public void AddTrip(Trip newTrip)
        {
            worldContext.Trips.Add(newTrip);
        }

        public bool SaveAll()
        {
            return worldContext.SaveChanges() > 0;
        }

        public Trip GetTripByName(string tripName, string username)
        {
            return worldContext.Trips
                .Include(x => x.Stops)
                .FirstOrDefault(x => x.Name == tripName);
        }

        public void AddStop(string tripName, Stop newStop, string username)
        {
            var theTrip = GetTripByName(tripName, username);
            newStop.Order = theTrip.Stops.Max(x => x.Order) +1 ;
            theTrip.Stops.Add(newStop);
            worldContext.Stops.Add(newStop);
        }

        public IEnumerable<Trip> GetUserTripsWithStops(string name)
        {
            try
            {
                return worldContext.Trips
                    .Include(x => x.Stops)
                    .Where(x=> x.UserName == name)
                    .OrderBy(x => x.Name)
                    .ToList();
            }
            catch (Exception e)
            {
                logger.LogError("Could not get trips with stops from database", e);
                throw;
            }
        }
    }
}
