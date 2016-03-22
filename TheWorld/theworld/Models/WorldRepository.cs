using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;

namespace theworld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext worldContext;
        private ILogger<WorldRepository> logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            worldContext = context;
            this.logger = logger;
        }

        public void AddStop(string tripName, string username, Stop newStop)
        {
            var theTrip = GetTripByName(tripName, username);
            newStop.Order = theTrip.Stops.Max(s => s.Order) + 1;
            theTrip.Stops.Add(newStop);
            worldContext.Stops.Add(newStop);
        }

        public void AddTrip(Trip newTrip)
        {
            worldContext.Add(newTrip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return worldContext.Trips.OrderBy(t => t.Name).ToList();
            }
            catch (Exception e)
            {
                logger.LogError("Could not get trips from database", e);
                return null;
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
                return null;
            }
        }

        public Trip GetTripByName(string tripName, string username)
        {
            return worldContext.Trips
                                 .Include(t => t.Stops)
                                 .FirstOrDefault(t => t.Name == tripName && t.UserName == username);
        }

        public IEnumerable<Trip> GetUserTripsWithStops(string name)
        {
            try
            {
                return worldContext.Trips
                .Include(x => x.Stops)
                .OrderBy(x => x.Name)
                .Where(x => x.UserName == name)
                .ToList();
            }
            catch (Exception e)
            {
                logger.LogError("Could not get trips with stops from database", e);
                return null;
            }
        }

        public bool SaveAll()
        {
            return worldContext.SaveChanges() > 0;
        }
    }
}
