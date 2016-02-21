﻿using System;
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
    }
}
