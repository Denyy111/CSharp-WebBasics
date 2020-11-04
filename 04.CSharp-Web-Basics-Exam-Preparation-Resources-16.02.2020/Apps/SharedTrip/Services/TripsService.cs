using System;
using SharedTrip.Data;
using SharedTrip.ViewModels.Trips;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;
        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool AddUserTotrip(string userId, string tripId)
        {
            var userInTrip = this.db.UserTrips.Any(x => x.UserId == userId 
            && x.TripId == tripId);

            // Ako usera veche e dobaven v trip, da ne se dobavq veche.
            if (userInTrip)
            {
                return false;
            }

            // Ako go nqma go syzdavame;
            var userTrip = new UserTrip
            {
                TripId = tripId,
                UserId = userId
            };

            this.db.UserTrips.Add(userTrip);
            this.db.SaveChanges();
            return true;
        }

        //Ima li svobodni mesta
        public bool HasAvailableSeats( string tripId)
        {
            var trip = this.db.Trips.Where(
                x => x.Id == tripId)
                .Select(x => new { x.Seats, TakenSeats = x.UserTrips.Count() })
                .FirstOrDefault();

            var availableSeats = trip.Seats - trip.TakenSeats;
            if (availableSeats <= 0)
            {
                return false;
            }

            return true;
        }

        public void Create(AddTripInputModel trip)
        {
            //Create user and inicialised property inside.
            var dbTrip = new Trip
            {
                DepartureTime = DateTime.ParseExact(trip.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                Description = trip.Description,
                EndPoint = trip.EndPoint,
                StartPoint = trip.StartPoint,
                ImagePath = trip.ImagePath,
                Seats = trip.Seats,
            };

            this.db.Trips.Add(dbTrip);
            this.db.SaveChanges();
        }

        public IEnumerable<TripViewModel> GetAll()
        {
            var trips = this.db.Trips.Select(x => new TripViewModel
            {
                DepartureTime = x.DepartureTime,
                EndPoint = x.EndPoint,
                StartPoint = x.StartPoint,
                Id = x.Id,
                AvailableSeats = x.Seats - x.UserTrips.Count(),
                //(obshto5 - 3ma zapisali = 5 swobodni)
                //(vzemame obshto mestata - kolkoto choveka sa se zapisali)
            }).ToList();

            return trips;
        }

        public TripDetailsViewModel GetDetails(string id)
        {
            var trip = this.db.Trips.Where(x => x.Id == id)
                 .Select(x => new TripDetailsViewModel
                 {
                     DepartureTime = x.DepartureTime,
                     Description = x.Description,
                     EndPoint = x.EndPoint,
                     StartPoint = x.StartPoint,
                     Id = x.Id,
                     ImagePath = x.ImagePath,
                     Seats = x.Seats,
                 })
                .FirstOrDefault();

            return trip;

        }
    }
}
