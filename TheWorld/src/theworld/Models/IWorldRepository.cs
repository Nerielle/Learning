using System.Collections.Generic;

namespace theworld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
        void AddTrip(Trip newTrip);
        bool SaveAll();
        Trip GetTripByName(string tripName, string username);
        void AddStop(string tripName, Stop newStop, string name);
        IEnumerable<Trip> GetUserTripsWithStops(string name);
    }
}